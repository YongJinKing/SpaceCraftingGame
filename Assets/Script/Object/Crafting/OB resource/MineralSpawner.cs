using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineralSpawner : MonoBehaviour
{
    public GameObject mineral;
    public int numberOfminerals = 5;
    public int mapWidth = 128;
    public int mapHeight = 128;

    public float minDistance = 1f; // �ڿ� ���� �ּ� �Ÿ�
    public LayerMask mineralLayer;

    void Start()
    {
        PlaceMinerals();
    }

    public void PlaceMinerals()
    {
        int mineralsPlaced = 0;
        while (mineralsPlaced < numberOfminerals)
        {
            Vector2 randomPosition = GetRandomPosition();
            if (CanPlaceMineral(randomPosition))
            {
                Instantiate(mineral, randomPosition, Quaternion.identity);
                mineralsPlaced++;
            }
            
        }
    }

     Vector2 GetRandomPosition()
    {
        float x = Random.Range(0, mapWidth);
        float y = Random.Range(0, mapHeight);
        return new Vector2(x, y);
    }

    bool CanPlaceMineral(Vector2 pos)
    {
        Collider2D[] hitCollider = Physics2D.OverlapCircleAll(pos, minDistance ,mineralLayer);
        return hitCollider.Length == 0;
    }
    
}
