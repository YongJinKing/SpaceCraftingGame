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
    CraftBuildingManager craftmanager;
    void Awake()
    {
        craftmanager = FindObjectOfType<CraftBuildingManager>();
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            foreach (KeyValuePair<Vector3Int, Tile> entry in availablePlaces)
            {
                Debug.Log(entry.Key + " : " + entry.Value);
            }
        }
    }

}
