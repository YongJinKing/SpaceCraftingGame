using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ResourcesSpawner : Singleton<ResourcesSpawner>
{
    public GameObject mineralPrefab; // 1x1 크기의 미네랄 프리팹
    public GameObject gasPrefab; // 1x1 크기의 가스 프리팹
    public GameObject largeResourcePrefab; // 2x2 크기의 큰 미네랄 프리팹

    public Transform minerals;
    public Transform gases;
    public Transform largeMinerals;

    public Tilemap tileMap; // 타일맵
    public LayerMask layerMask; // 건물과 자원이 있는지 없는지 검사하기 위한 layerMask

    public int placementInterval = 4; // 자원을 배치할 간격 (NxN 크기)
    private float mineralRatio = 0.6f; // 미네랄의 비율
    private float gasRatio = 0.3f; // 가스의 비율

    GameObject obj;
    int size = 0;

    void Start()
    {
        StartSpawnResources();
    }

    public void RespawnResources()
    {
        // 타일맵의 모든 셀을 일정 크기로 나눔
        for (int y = tileMap.cellBounds.yMin + placementInterval / 2; y <= tileMap.cellBounds.yMax - placementInterval / 2; y += placementInterval)
        {
            for (int x = tileMap.cellBounds.xMin + placementInterval / 2; x <= tileMap.cellBounds.xMax - placementInterval / 2; x += placementInterval)
            {
                size = 0;
                obj = null;
                Vector3Int cellPosition = new Vector3Int(x, y, 0);
                //Vector3 worldPos = tileMap.GetCellCenterLocal(cellPosition);
                Vector3 worldPos = tileMap.WorldToCell(cellPosition);

                if (!tileMap.HasTile(cellPosition)) continue;
                if (!TileManager.Instance.IsCraftable(cellPosition)) continue;
                // 여기에 조건 추가
                if (CheckObjectsInArea(cellPosition, placementInterval, layerMask)) continue;

                // 구역 내에서 랜덤 위치 계산
                Vector3 randomWorldPosition = GetRandomPositionInCell(worldPos);
                Vector3Int convertPos = ConvertMinusPos(randomWorldPosition);

                if (!TileManager.Instance.IsCraftable(convertPos)) continue;


                SpawnResource(randomWorldPosition, cellPosition, false);
            }
        }
    }
    public void StartSpawnResources()
    {
        // 타일맵의 모든 셀을 일정 크기로 나눔
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
                // 구역 내에서 랜덤 위치 계산
                Vector3 randomWorldPosition = GetRandomPositionInCell(worldPos);
                Vector3Int convertPos = ConvertMinusPos(randomWorldPosition);

                if (!TileManager.Instance.IsCraftable(convertPos)) continue;

                SpawnResource(randomWorldPosition, cellPosition, true);
                /*// 랜덤하게 자원 종류를 선택하여 배치
                float randomValue = Random.value;
                if (randomValue < mineralRatio)
                {
                    size = 1;
                    // 1x1 크기의 자원1 배치
                    obj = Instantiate(mineralPrefab, randomWorldPosition, Quaternion.identity);
                    obj.transform.SetParent(minerals);
                    RemovePlaceForResource(randomWorldPosition);

                }
                else if (randomValue < mineralRatio + gasRatio)
                {
                    size = 1;
                    // 1x1 크기의 자원2 배치
                    obj = Instantiate(gasPrefab, randomWorldPosition, Quaternion.identity);
                    obj.transform.SetParent(gases);
                    RemovePlaceForResource(randomWorldPosition);
                }
                else
                {
                    // 2x2 크기의 자원 배치
                    if (CanPlaceLargeResource(cellPosition))
                    {
                        size = 2;
                        randomWorldPosition = GetRandomPositionInCell(cellPosition);
                        obj = Instantiate(largeResourcePrefab, randomWorldPosition, Quaternion.identity);
                        obj.transform.SetParent(largeMinerals);
                        RemovePlaceForResource(randomWorldPosition);
                    }
                }*/

            }
        }
    }

    void SpawnResource(Vector3 randomWorldPosition, Vector3Int cellPosition, bool randomable)
    {
        if (randomable)
        {
            // 랜덤하게 자원 종류를 선택하여 배치
            float randomValue = Random.value;
            if (randomValue < mineralRatio)
            {
                size = 1;
                // 1x1 크기의 자원1 배치
                obj = Instantiate(mineralPrefab, randomWorldPosition, Quaternion.identity);
                obj.transform.SetParent(minerals);
                RemovePlaceForResource(randomWorldPosition);

            }
            else if (randomValue < mineralRatio + gasRatio)
            {
                size = 1;
                // 1x1 크기의 자원2 배치
                obj = Instantiate(gasPrefab, randomWorldPosition, Quaternion.identity);
                obj.transform.SetParent(gases);
                RemovePlaceForResource(randomWorldPosition);
            }
            else
            {
                // 2x2 크기의 자원 배치
                if (CanPlaceLargeResource(cellPosition))
                {
                    size = 2;
                    randomWorldPosition = GetRandomPositionInCell(cellPosition);
                    obj = Instantiate(largeResourcePrefab, randomWorldPosition, Quaternion.identity);
                    obj.transform.SetParent(largeMinerals);
                    RemovePlaceForResource(randomWorldPosition);
                }
            }
        }
        else
        {
            int randomValue = Random.Range(0, 3);
            if(randomValue == 0)
            {
                size = 1;
                // 1x1 크기의 자원1 배치
                obj = Instantiate(mineralPrefab, randomWorldPosition, Quaternion.identity);
                obj.transform.SetParent(minerals);
                RemovePlaceForResource(randomWorldPosition);
            }
            else if (randomValue == 1)
            {
                size = 1;
                // 1x1 크기의 자원2 배치
                obj = Instantiate(gasPrefab, randomWorldPosition, Quaternion.identity);
                obj.transform.SetParent(gases);
                RemovePlaceForResource(randomWorldPosition);
            }
            else
            {
                // 2x2 크기의 자원 배치
                if (CanPlaceLargeResource(cellPosition))
                {
                    size = 2;
                    randomWorldPosition = GetRandomPositionInCell(cellPosition);
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
        // 박스 중심 위치 계산
        Vector3 centerPosition = startPosition + new Vector3(size / 2, size / 2, 0);

        // NxN 크기의 박스 영역 내에 있는 오브젝트를 검사
        Collider2D[] colliders = Physics2D.OverlapBoxAll(centerPosition, new Vector2(size, size), 0f, layerMask);

        // 오브젝트가 있는지 확인
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
        //Vector3 cellWorldPosition = tileMap.CellToWorld(cellPosition);
        int randomOffsetX = (int)Random.Range(0f, (placementInterval * tileMap.cellSize.x) / 2);
        int randomOffsetY = (int)Random.Range(0f, (placementInterval * tileMap.cellSize.y) / 2);
        //return cellWorldPosition + new Vector3(randomOffsetX, randomOffsetY, 0);
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            RespawnResources();
        }
    }
}
