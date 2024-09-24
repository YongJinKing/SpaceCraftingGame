using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class ResourcesSpawner : Singleton<ResourcesSpawner>
{
    public GameObject mineralPrefab; // 1x1 ũ���� �̳׶� ������
    public GameObject gasPrefab; // 1x1 ũ���� ���� ������
    public GameObject largeResourcePrefab; // 2x2 ũ���� ū �̳׶� ������

    public Transform minerals;
    public Transform gases;
    public Transform largeMinerals;

    public Tilemap tileMap; // Ÿ�ϸ�
    public LayerMask layerMask; // �ǹ��� �ڿ��� �ִ��� ������ �˻��ϱ� ���� layerMask

    public int placementInterval = 4; // �ڿ��� ��ġ�� ���� (NxN ũ��)
    private float mineralRatio = 0.6f; // �̳׶��� ����
    private float gasRatio = 0.3f; // ������ ����

    GameObject obj;
    int size = 0;

    void Start()
    {
        StartCoroutine(RespawnCoroutine());
    }
    
    IEnumerator RespawnCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(120f);
            RespawnResources();
        }
        
    }

    public void RespawnResources()
    {
        // Ÿ�ϸ��� ��� ���� ���� ũ��� ����
        for (int y = tileMap.cellBounds.yMin + placementInterval / 2; y <= tileMap.cellBounds.yMax - placementInterval / 2; y += placementInterval)
        {
            for (int x = tileMap.cellBounds.xMin + placementInterval / 2; x <= tileMap.cellBounds.xMax - placementInterval / 2; x += placementInterval)
            {
                size = 0;
                obj = null;
                Vector3Int cellPosition = new Vector3Int(x, y, 0);
                Vector3 worldPos = tileMap.WorldToCell(cellPosition);

                if (!tileMap.HasTile(cellPosition)) continue;
                if (!TileManager.Instance.IsCraftable(cellPosition)) continue;
                if (CheckObjectsInArea(cellPosition, placementInterval, layerMask)) continue;

                // ���� ������ ���� ��ġ ���
                Vector3 randomWorldPosition = GetRandomPositionInCell(worldPos);
                Vector3Int convertPos = ConvertMinusPos(randomWorldPosition);

                if (!TileManager.Instance.IsCraftable(convertPos)) continue;


                SpawnResource(randomWorldPosition, cellPosition, false);
            }
        }
    }
    public void StartSpawnResources()
    {
        // Ÿ�ϸ��� ��� ���� ���� ũ��� ����
        for (int y = tileMap.cellBounds.yMin + placementInterval / 2; y <= tileMap.cellBounds.yMax - placementInterval / 2; y += placementInterval)
        {
            for (int x = tileMap.cellBounds.xMin + placementInterval / 2; x <= tileMap.cellBounds.xMax - placementInterval / 2; x += placementInterval)
            {
                size = 0;
                obj = null;
                Vector3Int cellPosition = new Vector3Int(x, y, 0);
                Vector3 worldPos = tileMap.WorldToCell(cellPosition);

                if (!tileMap.HasTile(cellPosition)) continue;
                if (!TileManager.Instance.IsCraftable(cellPosition)) continue;
                // ���� ������ ���� ��ġ ���
                Vector3 randomWorldPosition = GetRandomPositionInCell(worldPos);
                Vector3Int convertPos = ConvertMinusPos(randomWorldPosition);

                if (!TileManager.Instance.IsCraftable(convertPos)) continue;
                if (!CanPlaceAt(randomWorldPosition, placementInterval)) continue;

                SpawnResource(randomWorldPosition, cellPosition, true);
            }
        }
    }

    void SpawnResource(Vector3 randomWorldPosition, Vector3Int cellPosition, bool randomable)
    {
        if (randomable)
        {
            // �����ϰ� �ڿ� ������ �����Ͽ� ��ġ
            float randomValue = Random.value;
            if (randomValue < mineralRatio)
            {
                size = 1;
                // 1x1 ũ���� �ڿ�1 ��ġ
                obj = Instantiate(mineralPrefab, randomWorldPosition, Quaternion.identity);
                obj.transform.SetParent(minerals);
                RemovePlaceForResource(randomWorldPosition);

            }
            else if (randomValue < mineralRatio + gasRatio)
            {
                size = 1;
                // 1x1 ũ���� �ڿ�2 ��ġ
                obj = Instantiate(gasPrefab, randomWorldPosition, Quaternion.identity);
                obj.transform.SetParent(gases);
                RemovePlaceForResource(randomWorldPosition);
            }
            else
            {
                // 2x2 ũ���� �ڿ� ��ġ
                if (CanPlaceLargeResource(cellPosition))
                {
                    size = 2;
                    randomWorldPosition = GetRandomPositionInCell(cellPosition);
                    Vector3 spawnPos = new Vector3(randomWorldPosition.x + 0.25f, randomWorldPosition.y + 0.25f, randomWorldPosition.z);
                    obj = Instantiate(largeResourcePrefab, spawnPos, Quaternion.identity);
                    obj.transform.SetParent(largeMinerals);
                    RemovePlaceForResource(randomWorldPosition);
                }
            }
        }
        else
        {
            int randomValue = Random.Range(0, 3);
            if (randomValue == 0)
            {
                size = 1;
                // 1x1 ũ���� �ڿ�1 ��ġ
                obj = Instantiate(mineralPrefab, randomWorldPosition, Quaternion.identity);
                obj.transform.SetParent(minerals);
                RemovePlaceForResource(randomWorldPosition);
            }
            else if (randomValue == 1)
            {
                size = 1;
                // 1x1 ũ���� �ڿ�2 ��ġ
                obj = Instantiate(gasPrefab, randomWorldPosition, Quaternion.identity);
                obj.transform.SetParent(gases);
                RemovePlaceForResource(randomWorldPosition);
            }
            else
            {
                // 2x2 ũ���� �ڿ� ��ġ
                if (CanPlaceLargeResource(cellPosition))
                {
                    size = 2;
                    randomWorldPosition = GetRandomPositionInCell(cellPosition);
                    Vector3Int rndPos = new Vector3Int((int)randomWorldPosition.x, (int)randomWorldPosition.y, (int)randomWorldPosition.z);
                    if (!CanPlaceLargeResource(rndPos)) return;
                    obj = Instantiate(largeResourcePrefab, randomWorldPosition, Quaternion.identity);
                    obj.transform.SetParent(largeMinerals);
                    RemovePlaceForResource(randomWorldPosition);
                }
            }
        }
    }

    bool checkCraftable(Vector3Int cellPosition)
    {
        if (!tileMap.HasTile(cellPosition)) return false;
        if (!TileManager.Instance.IsCraftable(cellPosition)) return false;

        Vector3 worldPos = tileMap.WorldToCell(cellPosition);

        Vector3 randomWorldPosition = GetRandomPositionInCell(worldPos);
        Vector3Int convertPos = ConvertMinusPos(randomWorldPosition);

        if (!TileManager.Instance.IsCraftable(convertPos)) return false;

        return true;
    }
    bool CheckObjectsInArea(Vector3Int startPosition, float size, LayerMask layerMask)
    {
        // �ڽ� �߽� ��ġ ���
        Vector3 centerPosition = startPosition + new Vector3(size / 2, size / 2, 0);

        // NxN ũ���� �ڽ� ���� ���� �ִ� ������Ʈ�� �˻�
        Collider2D[] colliders = Physics2D.OverlapBoxAll(centerPosition, new Vector2(size, size), 0f, layerMask);

        // ������Ʈ�� �ִ��� Ȯ��
        return colliders.Length > 0;
    }

    Vector3Int ConvertMinusPos(Vector3 pos)
    {
        Vector3 tmpPos = pos;
        if (tmpPos.x < 0)
        {
            tmpPos.x--;
        }
        if (tmpPos.y < 0)
        {
            tmpPos.y--;
        }
        return new Vector3Int((int)tmpPos.x, (int)tmpPos.y, 0);
    }
    void RemovePlaceForResource(Vector3 pos)
    {
        if (pos.x < 0)
        {
            pos.x--;
        }
        if (pos.y < 0)
        {
            pos.y--;
        }

        Vector3Int resourcePos = new Vector3Int((int)pos.x, (int)pos.y, 0);
        TileManager.Instance.RemopvePlace(resourcePos, obj, size);
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                Vector3Int cellPos = resourcePos + new Vector3Int((int)tileMap.cellSize.x * j, (int)tileMap.cellSize.y * i, 0);
                TileManager.Instance.RemopvePlace(cellPos);
            }
        }
    }

    Vector3 GetRandomPositionInCell(Vector3 cellPosition)
    {
        int randomOffsetX = (int)Random.Range(0f, (placementInterval * tileMap.cellSize.x) / 2);
        int randomOffsetY = (int)Random.Range(0f, (placementInterval * tileMap.cellSize.y) / 2);
        return cellPosition + new Vector3(randomOffsetX + 0.5f, randomOffsetY + 0.5f, 0);
    }

    bool CanPlaceLargeResource(Vector3Int position)
    {
        Vector3Int[] offsets = new Vector3Int[placementInterval * placementInterval];
        int idx = 0;
        for (int i = 0; i < placementInterval; i++)
        {
            for (int j = 0; j < placementInterval; j++)
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

    bool CanPlaceAt(Vector3 pos, int interval)
    {
        /*
         ���ڷ� ���� pos�� �������� �������� interval�� ������ OverlapSphere�� �׷� �� �ȿ� ���� ������ ���̾� ����ũ�� �ɸ���
        � ��ü�� �ִٸ� false�� �����ϰ� ���ٸ� true�� �����Ѵ�.
         */

        // �������� interval�� �������� ����
        float radius = interval / 2f;

        // OverlapCircle�� ����Ͽ� �־��� ��ġ(pos)�� �������� ������ ���̾� ����ũ�� �ش��ϴ� ������Ʈ�� �ִ��� Ȯ��
        Collider2D hitCollider = Physics2D.OverlapCircle(pos, radius, layerMask);

        // �浹�� ��ü�� �ִٸ� false�� ����, ������ true�� ����
        return hitCollider == null;

    }


}
