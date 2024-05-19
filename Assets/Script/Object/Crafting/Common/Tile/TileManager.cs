using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
    public Tilemap tileMap = null;

    public List<Vector3> availablePlaces;
    public List<Vector3> objectPlaces;

    CraftBuildingManager craftmanager;
    void Awake()
    {
        craftmanager = FindObjectOfType<CraftBuildingManager>();
        craftmanager.RemovePlaceEvent.AddListener(RemopvePlace);
        tileMap = transform.GetComponent<Tilemap>();
        availablePlaces = new List<Vector3>();

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
                    availablePlaces.Add(place);
                }
                else
                {
                    //No tile at "place"
                }
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            CoutList();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            RandomObject();
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            CoutRndList();
        }
    }

    public bool IsCraftable(Vector3 coordinates)
    {
        return availablePlaces.Contains(coordinates);
    }

    public void RemopvePlace(Vector3 coordinates)
    {
        availablePlaces.Remove(coordinates);
    }

    void CoutList()
    {
        for (int i = 0; i < availablePlaces.Count; i++)
        {
            Debug.Log(availablePlaces[i]);
        }
    }

    void CoutRndList()
    {
        for (int i = 0; i < objectPlaces.Count; i++)
        {
            Debug.Log(objectPlaces[i]);
        }
    }

    void RandomObject()
    {
        int idx = -1;
        int prevIdx = idx;
        for (int i = 0; i < 10; i++)
        {
            idx = Random.Range(0, availablePlaces.Count);
            if (prevIdx == idx)
            {
                continue;
            }
            else
            {
                objectPlaces.Add(availablePlaces[idx]);
                availablePlaces.RemoveAt(idx);
                prevIdx = idx;
            }

        }


    }
}
