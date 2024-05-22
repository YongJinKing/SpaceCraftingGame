using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
    public Tilemap tileMap = null;

    //public List<Vector3> availablePlaces;
    public Dictionary<Vector3, bool> availablePlaces;
    CraftBuildingManager craftmanager;
    void Awake()
    {
        craftmanager = FindObjectOfType<CraftBuildingManager>();
        craftmanager.RemovePlaceEvent.AddListener(RemopvePlace);
        tileMap = transform.GetComponent<Tilemap>();
        //availablePlaces = new List<Vector3>();
        availablePlaces = new Dictionary<Vector3, bool>();

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
                    //Tile at "place"
                    availablePlaces[place] = true;
                }
                else
                {
                    //No tile at "place"
                }
            }
        }

        Debug.Log(availablePlaces.Count);
    }

    public bool IsCraftable(Vector3 coordinates)
    {
        //return availablePlaces.Contains(coordinates);
        return availablePlaces[coordinates];
    }

    public void RemopvePlace(Vector3 coordinates)
    {
        availablePlaces[coordinates] = false;
    }

    public int GetTileLength()
    {
        return (int)Mathf.Sqrt(availablePlaces.Count);
    }
    
    public bool HasTile(Vector3 coordinates)
    {
        return availablePlaces.ContainsKey(coordinates);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            foreach (KeyValuePair<Vector3, bool> entry in availablePlaces)
            {
                Debug.Log(entry.Key + " : " + entry.Value);
            }
        }
    }

}
