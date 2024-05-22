using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MineralSpawner : MonoBehaviour
{
    public GameObject mineralPrefab;
    public int numberOfMinerals = 10;
    public Tilemap tilemap; // Ÿ�ϸ��� ����
    public LayerMask mineralLayer;
    public float minDistanceBetweenMinerals = 30f; // �ּ� �Ÿ� ����
    public int maxAttempts = 100; // �ִ� �õ� Ƚ��

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
            Debug.LogWarning("�ִ� �õ��� ���� �߽��ϴ�.");
        }
    }

    Vector2 GetRandomPositionInTilemap()
    {
        // Ÿ�ϸ��� �ٿ�� �ڽ��� �����ɴϴ�.
        BoundsInt bounds = tilemap.cellBounds;

        // Ÿ�ϸ��� �ٿ�� �ڽ� ������ ������ �� ��ǥ�� �����մϴ�.
        int x = Random.Range(bounds.xMin, bounds.xMax);
        int y = Random.Range(bounds.yMin, bounds.yMax);

        // ���õ� ���� �߽� ���� ��ǥ�� �����ɴϴ�.
        Vector3Int cellPosition = new Vector3Int(x, y, 0);
        Vector3 worldPosition = tilemap.GetCellCenterWorld(cellPosition);

        // 2D ���ͷ� ��ȯ�մϴ�.
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

