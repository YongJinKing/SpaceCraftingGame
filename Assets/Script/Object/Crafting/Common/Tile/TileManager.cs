using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class Tile
{
    public bool available;
    public GameObject Object;

    public Tile(bool available, GameObject Object)
    {
        this.available = available;
        this.Object = Object;
    }
}

public class TileManager : MonoBehaviour
{
    public Tilemap tileMap = null;

    //public List<Vector3> availablePlaces;
    public Dictionary<Vector3Int, Tile> availablePlaces;
    ComponetsInfo componentsInfo;
    CraftBuildingManager craftmanager;
    CraftFactory factory;
    void Awake()
    {
        craftmanager = FindObjectOfType<CraftBuildingManager>();
        factory = new CraftFactory();
        craftmanager.RemovePlaceEvent.AddListener(RemopvePlace);
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
                    availablePlaces[_place] = new Tile(true, null);
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
            Debug.Log("읽어온 좌표 : " + componetPlace);
            Debug.Log("읽어온 인덱스 : " + componentsInfo.components[i].index);
            Vector3 componentPos = new Vector3(componetPlace.x + tileMap.tileAnchor.x, componetPlace.y + tileMap.tileAnchor.y, componetPlace.z); // 추후 수정해야함 << size에 따라 바뀌어야함
            Tile isPlacedTile = new Tile(false, factory.CraftBuilding(componentsInfo.components[i].index, componentPos, componentsInfo.components[i].Hp));
            availablePlaces[componetPlace] = isPlacedTile;

        }


        Debug.Log(availablePlaces.Count);
    }

    public bool IsCraftable(Vector3Int coordinates)
    {
        //return availablePlaces.Contains(coordinates);
        return availablePlaces[coordinates].available;
    }

    public void RemopvePlace(Vector3Int coordinates, GameObject obj)
    {
        availablePlaces[coordinates].available = false;
        availablePlaces[coordinates].Object = obj;
    }

    public int GetTileLength()
    {
        return (int)Mathf.Sqrt(availablePlaces.Count);
    }

    public bool HasTile(Vector3Int coordinates)
    {
        return availablePlaces.ContainsKey(coordinates);
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
