using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ResourcesSpawner : MonoBehaviour
{
    public GameObject mineralPrefab; // 1x1 ũ���� �ڿ� ������
    public GameObject gasPrefab; // 1x1 ũ���� �ڿ� ������
    public GameObject largeResourcePrefab; // 2x2 ũ���� �ڿ� ������
    public Tilemap tileMap; // Ÿ�ϸ�

    public int placementInterval = 4; // �ڿ��� ��ġ�� ���� (NxN ũ��)
    private float mineralRatio = 0.4f; // �ڿ�1�� ����
    private float gasRatio = 0.5f; // �ڿ�2�� ����

    void Start()
    {
        SpawnResources();
    }

    public void SpawnResources()
    {
        // Ÿ�ϸ��� ��� ���� ���� ũ��� ����
        for (int y = tileMap.cellBounds.yMin + placementInterval / 2; y <= tileMap.cellBounds.yMax - placementInterval / 2; y += placementInterval)
        {
            for (int x = tileMap.cellBounds.xMin + placementInterval / 2; x <= tileMap.cellBounds.xMax - placementInterval / 2; x += placementInterval)
            {
                Vector3Int cellPosition = new Vector3Int(x, y, 0);
                Vector3 worldPos = tileMap.GetCellCenterLocal(cellPosition);
                if (!tileMap.HasTile(cellPosition)) continue;

                // ���� ������ ���� ��ġ ���
                Vector3 randomWorldPosition = GetRandomPositionInCell(worldPos);

                // �����ϰ� �ڿ� ������ �����Ͽ� ��ġ
                float randomValue = Random.value;
                if (randomValue < mineralRatio)
                {
                    // 1x1 ũ���� �ڿ�1 ��ġ
                    Instantiate(mineralPrefab, randomWorldPosition, Quaternion.identity);
                    
                }
                else if (randomValue < mineralRatio + gasRatio)
                {
                    // 1x1 ũ���� �ڿ�2 ��ġ
                    Instantiate(gasPrefab, randomWorldPosition, Quaternion.identity);
                }
                else
                {
                    // 2x2 ũ���� �ڿ� ��ġ
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
        /*// 2x2 �ڿ��� ��ġ�� 4���� Ÿ���� �˻�
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
