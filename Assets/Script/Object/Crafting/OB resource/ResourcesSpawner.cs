using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ResourcesSpawner : MonoBehaviour
{
    public GameObject mineralPrefab; // 1x1 크기의 자원 프리팹
    public GameObject gasPrefab; // 1x1 크기의 자원 프리팹
    public GameObject largeResourcePrefab; // 2x2 크기의 자원 프리팹
    public Tilemap tileMap; // 타일맵

    public int placementInterval = 4; // 자원을 배치할 간격 (NxN 크기)
    private float mineralRatio = 0.4f; // 자원1의 비율
    private float gasRatio = 0.5f; // 자원2의 비율

    void Start()
    {
        SpawnResources();
    }

    public void SpawnResources()
    {
        // 타일맵의 모든 셀을 일정 크기로 나눔
        for (int y = tileMap.cellBounds.yMin + placementInterval / 2; y <= tileMap.cellBounds.yMax - placementInterval / 2; y += placementInterval)
        {
            for (int x = tileMap.cellBounds.xMin + placementInterval / 2; x <= tileMap.cellBounds.xMax - placementInterval / 2; x += placementInterval)
            {
                Vector3Int cellPosition = new Vector3Int(x, y, 0);
                Vector3 worldPos = tileMap.GetCellCenterLocal(cellPosition);
                if (!tileMap.HasTile(cellPosition)) continue;

                // 구역 내에서 랜덤 위치 계산
                Vector3 randomWorldPosition = GetRandomPositionInCell(worldPos);

                // 랜덤하게 자원 종류를 선택하여 배치
                float randomValue = Random.value;
                if (randomValue < mineralRatio)
                {
                    // 1x1 크기의 자원1 배치
                    Instantiate(mineralPrefab, randomWorldPosition, Quaternion.identity);
                    
                }
                else if (randomValue < mineralRatio + gasRatio)
                {
                    // 1x1 크기의 자원2 배치
                    Instantiate(gasPrefab, randomWorldPosition, Quaternion.identity);
                }
                else
                {
                    // 2x2 크기의 자원 배치
                    if (CanPlaceLargeResource(cellPosition))
                    {
                        Instantiate(largeResourcePrefab, randomWorldPosition, Quaternion.identity);
                    }
                }
            }
        }
    }

    Vector3 GetRandomPositionInCell(Vector3 cellPosition)
    {
        //Vector3 cellWorldPosition = tileMap.CellToWorld(cellPosition);
        int randomOffsetX = (int)Random.Range(0f, (placementInterval * tileMap.cellSize.x) / 2);
        int randomOffsetY = (int)Random.Range(0f, (placementInterval * tileMap.cellSize.y) / 2);
        //return cellWorldPosition + new Vector3(randomOffsetX, randomOffsetY, 0);
        return cellPosition + new Vector3(randomOffsetX, randomOffsetY, 0);
    }

    bool CanPlaceLargeResource(Vector3Int position)
    {
        /*// 2x2 자원이 배치될 4개의 타일을 검사
        Vector3Int[] offsets = new Vector3Int[]
        {
            new Vector3Int(0, 0, 0),
            new Vector3Int(1, 0, 0),
            new Vector3Int(0, 1, 0),
            new Vector3Int(1, 1, 0)
        };*/
        Vector3Int[] offsets = new Vector3Int[placementInterval * placementInterval];
        int idx = 0;
        for(int i = 0; i < placementInterval; i++)
        {
            for(int j = 0; j < placementInterval; j++)
            {
                offsets[idx++] = new Vector3Int(i, j, 0);
            }
        }

        foreach (var offset in offsets)
        {
            Vector3Int checkPosition = position + offset;
            if (!tileMap.HasTile(checkPosition))
            {
                return false;
            }
        }

        return true;
    }
}
