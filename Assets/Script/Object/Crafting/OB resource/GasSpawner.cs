using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasSpawner : MonoBehaviour
{
    public GameObject Gas;
    public int numberOfGas = 5;
    public int mapWidth = 128;
    public int mapHeight = 128;
    public float minDistance = 1; //磊盔 积己 弥家 芭府
    public LayerMask GasMask;
    void Start()
    {
        PlaceGas();
    }

    public void PlaceGas()
    {
        int GasPlaced = 0;
        while(GasPlaced < numberOfGas)
        {
            Vector2 randomPosition = GetRandomPosition();
            if(CanPlaceGas(randomPosition))
            {
                Instantiate(Gas, randomPosition, Quaternion.identity);
                GasPlaced++;
            }
        }
    }

    Vector2 GetRandomPosition()
    {
        float x = Random.Range(0, mapWidth);
        float y = Random.Range(0, mapHeight);
        return new Vector2(x, y);
    }
   
    bool CanPlaceGas(Vector2 pos)
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(pos, minDistance, GasMask);
        return hitColliders.Length == 0;
    }

}
