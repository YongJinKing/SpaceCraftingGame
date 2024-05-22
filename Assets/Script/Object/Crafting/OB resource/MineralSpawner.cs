using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MineralSpawner : MonoBehaviour
{
    public GameObject mineralPrefab;
    public int numberOfMinerals = 10;
    public Tilemap tilemap; // 타일맵을 참조
    public LayerMask mineralLayer;
    public float minDistanceBetweenMinerals = 30f; // 최소 거리 설정
    public int maxAttempts = 100; // 최대 시도 횟수

    void Start()
    {
        PlaceMinerals();
        PlaceInitialMinerals();
    }

    void PlaceInitialMinerals()
    {
        for (int i = 0; i < numberOfMinerals; i++)
        {
            PlaceMinerals();
        }
    }
    public void PlaceMinerals()
    {
        int attempts = 0;

        while (attempts < maxAttempts)
        {
            Vector2 randomPosition = GetRandomPositionInTilemap();
            if (CanPlaceMineral(randomPosition))
            {
                GameObject mineral = Instantiate(mineralPrefab, randomPosition, Quaternion.identity);
                mineral.GetComponent<CollectionResource>().Initialize(this);
                return;
            }
            attempts++;
        }

        if (attempts >= maxAttempts)
        {
            Debug.LogWarning("최대 시도에 도달 했습니다.");
        }
    }

    Vector2 GetRandomPositionInTilemap()
    {
        // 타일맵의 바운딩 박스를 가져옵니다.
        BoundsInt bounds = tilemap.cellBounds;

        // 타일맵의 바운딩 박스 내에서 랜덤한 셀 좌표를 선택합니다.
        int x = Random.Range(bounds.xMin, bounds.xMax);
        int y = Random.Range(bounds.yMin, bounds.yMax);

        // 선택된 셀의 중심 월드 좌표를 가져옵니다.
        Vector3Int cellPosition = new Vector3Int(x, y, 0);
        Vector3 worldPosition = tilemap.GetCellCenterWorld(cellPosition);

        // 2D 벡터로 반환합니다.
        return new Vector2(worldPosition.x, worldPosition.y);
    }

    bool CanPlaceMineral(Vector2 position)
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(position, minDistanceBetweenMinerals, mineralLayer);
        bool canPlace = hitColliders.Length == 0;
        Debug.Log($"Can place at {position}: {canPlace}");
        return canPlace;
    }
}

