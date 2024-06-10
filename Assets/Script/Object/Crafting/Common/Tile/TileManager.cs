using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    //public List<Vector3> availablePlaces;
    public Dictionary<Vector3Int, Tile> availablePlaces;
    ComponetsInfo componentsInfo;
    CraftBuildingManager craftmanager;
    CraftFactory factory;
    void Awake()
    {
        //craftmanager = FindObjectOfType<CraftBuildingManager>();
        factory = new CraftFactory();
        /*craftmanager.RemovePlaceEvent.AddListener(RemopvePlace);
        craftmanager.WritePlaceInfoEvent.AddListener(RemopvePlace);*/
        tileMap = transform.GetComponent<Tilemap>();
        //availablePlaces = new List<Vector3>();
        availablePlaces = new Dictionary<Vector3Int, Tile>();

        /*for (int x = tileMap.cellBounds.xMin; x < tileMap.cellBounds.xMax; x++)
        {
            for (int y = tileMap.cellBounds.yMin; y < tileMap.cellBounds.yMax; y++)
            {
                Vector3Int localPlace = (new Vector3Int(x, y, (int)tileMap.transform.position.z));
                Vector3 place = tileMap.CellToWorld(localPlace);
                if (tileMap.HasTile(localPlace))
                {
                    //Tile at "place"
                    availablePlaces.Add(place);
                }
                else
                {
                    //No tile at "place"
                }
            }
        }*/


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
                    /*availablePlaces[_place].available = true;
                    availablePlaces[_place].Object = null;*/
                }
                else
                {
                    //No tile at "place"
                }
            }
        }


        componentsInfo = ComponentSaveSystem.Instance.LoadTileInfo();
        if (componentsInfo == null) return;
        else
        {
            Debug.Log("읽어서 가져옴");
        }
        for (int i = 0; i < componentsInfo.components.Count; i++)
        {
            Vector3Int componetPlace = componentsInfo.components[i].coordinates;
            int index = componentsInfo.components[i].index;
            float Hp = componentsInfo.components[i].Hp;
            int size = componentsInfo.components[i].size;

            Debug.Log("읽어온 좌표 : " + componetPlace);
            Debug.Log("읽어온 인덱스 : " + index);
            Debug.Log("읽어온 사이즈 : " + size);
            Debug.Log("읽어온 Hp : " + Hp);
            Vector3 componentPos = new Vector3(componetPlace.x + (tileMap.tileAnchor.x * size), 
                componetPlace.y + (tileMap.tileAnchor.y * size), componetPlace.z);

            Tile isPlacedTile = null;
            if (size != 0) isPlacedTile = new Tile(false, factory.CraftBuilding(index, componentPos, Hp, size), size);
            else isPlacedTile = new Tile(false, null, 0);

            availablePlaces[componetPlace] = isPlacedTile;

        }


        Debug.Log(availablePlaces.Count);
    }

    public bool IsCraftable(Vector3Int coordinates)
    {
        //return availablePlaces.Contains(coordinates);
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
        /*int x = Mathf.RoundToInt(worldPos.x);
        int y = Mathf.RoundToInt(worldPos.y);
        Vector3Int coordinates = (new Vector3Int(x, y, 0));*/
        //Vector3Int coordinates = (new Vector3Int((int)worldPos.x, (int)worldPos.y, 0));
        //Vector3 place = tileMap.CellToWorld(coordinates);
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
        Vector3 centerPos = new Vector3(pos.x + tileMap.cellSize.x, pos.y + tileMap.cellSize.y, 0);

        return centerPos;
    }

    public void DestoryObjectOnTile(Vector3 pos) // 해당 위치에 있던 타일 제거
    {
        
        Vector3Int coordiantes = tileMap.WorldToCell(pos);
        if (tileMap.HasTile(coordiantes))
        {
            availablePlaces.Remove(coordiantes);
            availablePlaces[coordiantes] = new Tile(true, null, 0);
            Debug.Log(coordiantes + "위치 건물 삭제");
        }
    }

    public Dictionary<Vector3Int, Tile> GetTileMap()
    {
        return availablePlaces;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            foreach (KeyValuePair<Vector3Int, Tile> entry in availablePlaces)
            {
                Debug.Log(entry.Key + " : " + entry.Value);
            }
        }

        if (Input.GetKeyDown(KeyCode.F10))
        {
            ComponentSaveSystem.Instance.SaveTilesInfo();
        }
    }

}
