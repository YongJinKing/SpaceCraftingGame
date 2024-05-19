using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineralSpawner : MonoBehaviour
{
    public GameObject mineral;
    public int numberOfminerals = 5;
    public int mapWidth = 128;
    public int mapHeight = 128;

    void Start()
    {
        PlaceMinerals();
    }

    public void PlaceMinerals()
    {
        for(int i= 0; i < numberOfminerals; i++)
        {
            Vector2 randomPosition = GetRandomPosition();
            Instantiate(mineral, randomPosition, Quaternion.identity);
        }
    }

     Vector2 GetRandomPosition()
    {
        float x = Random.Range(0, mapWidth);
        float y = Random.Range(0, mapHeight);
        return new Vector2(x, y);
    }
    
}
