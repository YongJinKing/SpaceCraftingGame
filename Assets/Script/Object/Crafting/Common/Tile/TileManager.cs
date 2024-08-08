using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;


public class Tile
{
    public bool available;
    public GameObject Object;
    public int size;

    public Tile(bool available, GameObject Object, int size)
    {
        this.available = available;
        this.Object = Object;
        this.size = size;
    }
}

public class TileManager : Singleton<TileManager>
{
    public Tilemap tileMap = null;
    public Dictionary<Vector3Int, Tile> availablePlaces;
    ComponetsInfo componentsInfo;
    CraftBuildingManager craftmanager;
    string path;
    void Awake()
    {
        craftmanager = FindObjectOfType<CraftBuildingManager>();
        craftmanager.RemovePlaceEvent.AddListener(RemopvePlace);
        craftmanager.WritePlaceInfoEvent.AddListener(RemopvePlace);
        tileMap = transform.GetComponent<Tilemap>();
        availablePlaces = new Dictionary<Vector3Int, Tile>();
        path = "TileSaveData" + DataManager.Instance.nowSlot + ".json";
        for (int y = tileMap.cellBounds.yMin; y < tileMap.cellBounds.yMax; y++)
        {
            for (int x = tileMap.cellBounds.xMin; x < tileMap.cellBounds.xMax; x++)
            {
                Vector3Int localPlace = (new Vector3Int(x, y, (int)tileMap.transform.position.z));
                Vector3 place = tileMap.CellToWorld(localPlace);
                if (tileMap.HasTile(localPlace))
                {
                    Vector3Int _place = new Vector3Int((int)place.x, (int)place.y, (int)place.z);
                    //Tile at "place"
                    availablePlaces[_place] = new Tile(true, null, 0);
                }
                else
                {
                    //No tile at "place"
                }
            }
        }

        SpaceShipInitiallize();

        componentsInfo = ComponentSaveSystem.Instance.LoadTileInfo(path);
        if (componentsInfo == null)
        {
            // 여기서 자원 최초 생성
            ResourcesSpawner.Instance.StartSpawnResources();
            return;
        }
        else
        {
            
        }
        for (int i = 0; i < componentsInfo.components.Count; i++)
        {
            Vector3Int componetPlace = componentsInfo.components[i].coordinates;
            int index = componentsInfo.components[i].index;
            float Hp = componentsInfo.components[i].Hp;
            int size = componentsInfo.components[i].size;

            
            Vector3 componentPos = new Vector3(componetPlace.x + (tileMap.tileAnchor.x * size), 
                componetPlace.y + (tileMap.tileAnchor.y * size), componetPlace.z);

            Tile isPlacedTile = null;
            if (size != 0) isPlacedTile = new Tile(false, CraftFactory.Instance.CraftBuilding(index, componentPos, Hp, size), size);
            else isPlacedTile = new Tile(false, null, 0);

            availablePlaces[componetPlace] = isPlacedTile;

        }


        
    }

    public bool IsCraftable(Vector3Int coordinates)
    {
        return availablePlaces[coordinates].available;
    }

    public void RemopvePlace(Vector3Int coordinates, GameObject obj, int size)
    {
        availablePlaces[coordinates].available = false;
        availablePlaces[coordinates].Object = obj;
        availablePlaces[coordinates].size = size;
        
    }

    public void RemopvePlace(Vector3Int coordinates)
    {
        availablePlaces[coordinates].available = false;
    }

    public void RevokePlace(Vector3Int coordinates)
    {
        Vector3Int cellPos = Vector3Int.zero;
        Vector3Int tmpPos = coordinates;
        int size = availablePlaces[coordinates].size;
        
        
        availablePlaces[coordinates].available = true;
        availablePlaces.Remove(coordinates);
        availablePlaces[coordinates] = new Tile(true, null, 0);
        
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                cellPos = tmpPos + new Vector3Int((int)tileMap.cellSize.x * j, (int)tileMap.cellSize.y * i, 0);
                
                availablePlaces[cellPos].available = true;
                availablePlaces.Remove(cellPos);
                availablePlaces[cellPos] = new Tile(true, null, 0);
            }
        }
        
    }

    public int GetTileLength()
    {
        return (int)Mathf.Sqrt(availablePlaces.Count);
    }

    public bool HasTile(Vector3Int coordinates)
    {
        return availablePlaces.ContainsKey(coordinates);
    }

    public Vector3Int GetTileCoordinates(Vector2 worldPos)
    {
        Vector3Int coordinates = tileMap.WorldToCell(new Vector3(worldPos.x, worldPos.y, 0));
        if (HasTile(coordinates))
        {
            return coordinates;
        }
        else
        {
            Vector3Int falseCoordinates = new Vector3Int(coordinates.x, coordinates.y, -1);
            return falseCoordinates;
        }
    }

    public Vector3 GetWorldPosCenterOfCell(Vector2Int pos)
    {
        Vector3 centerPos = new Vector3(pos.x + tileMap.cellSize.x / 2.0f, pos.y + tileMap.cellSize.y / 2.0f, 0);

        return centerPos;
    }

    public void DestoryObjectOnTile(Vector3 pos) // 해당 위치에 있던 타일 제거
    {
        Vector3Int coordiantes = tileMap.WorldToCell(pos);
        if (tileMap.HasTile(coordiantes))
        {
            RevokePlace(coordiantes);
        }
    }

    void SpaceShipInitiallize()
    {
        int centerY = (tileMap.cellBounds.yMin + tileMap.cellBounds.yMax) / 2;
        int centerX = (tileMap.cellBounds.xMin + tileMap.cellBounds.xMax) / 2;
        for (int y = centerY; y > centerY - 2; y--)
        {
            for (int x = centerX; x > centerX - 2; x--)
            {
                Vector3Int localPlace = (new Vector3Int(x, y, (int)tileMap.transform.position.z));
                
                RemopvePlace(localPlace);
            }
        }

    }

    public Dictionary<Vector3Int, Tile> GetTileMap()
    {
        return availablePlaces;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F10))
        {
            ComponentSaveSystem.Instance.SaveTilesInfo();
        }
    }

}
