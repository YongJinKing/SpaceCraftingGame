using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasSpawner : MonoBehaviour
{
    public GameObject Gas;
    public int numberOfGas = 5;
    public int mapWidth = 128;
    public int mapHeight = 128;

    void Start()
    {
        PlaceGas();
    }

    public void PlaceGas()
    {
        for (int i = 0; i< numberOfGas; i++)
        {
            Vector2 randomPosition = GetRandomPosition();
            Instantiate(Gas, randomPosition, Quaternion.identity);
        }
    }

    Vector2 GetRandomPosition()
    {
        float x = Random.Range(0, mapWidth);
        float y = Random.Range(0, mapHeight);
        return new Vector2(x, y);
    }
   
}
