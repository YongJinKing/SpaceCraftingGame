using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MineralSpawner : MonoBehaviour
{
    public GameObject mineralPrefab; // 미네랄 프리팹을 연결하기 위한 변수
    public Tilemap tilemap; // 타일맵을 연결하기 위한 변수
    public int mapWidth = 128; // 타일맵의 너비
    public int mapHeight = 128; // 타일맵의 높이
    public int numberOfMinerals = 16; // 생성할 미네랄의 수
    public LayerMask mineralLayer; // 미네랄 레이어를 설정하기 위한 변수

    private void OnEnable()
    {
        CollectionResource.OnMineralHarvested += SpawnMineralNearPosition;
    }

    private void OnDisable()
    {
        CollectionResource.OnMineralHarvested -= SpawnMineralNearPosition;
    }

    void Start()
    {
        SpawnMinerals();
    }

    void SpawnMinerals()
    {
        int mineralsPlaced = 0;
        float mineralDensity = (float)numberOfMinerals / (mapWidth * mapHeight);

        for (int y = tilemap.cellBounds.yMin; y < tilemap.cellBounds.yMax; y++)
        {
            for (int x = tilemap.cellBounds.xMin; x < tilemap.cellBounds.xMax; x++)
            {
                if (mineralsPlaced >= numberOfMinerals)
                {
                    return;
                }

                Vector3Int cellPosition = new Vector3Int(x, y, 0);
                Vector3 worldPosition = tilemap.CellToWorld(cellPosition);

                if (tilemap.GetTile(cellPosition) != null && !IsMineralAtPosition(worldPosition) && Random.value < mineralDensity)
                {
                    Instantiate(mineralPrefab, worldPosition, Quaternion.identity);
                    mineralsPlaced++;
                }
            }
        }

        // 미네랄이 부족한 경우 추가로 생성 (예외 처리)
        /*while (mineralsPlaced < numberOfMinerals)
        {
            int x = Random.Range(0, mapWidth);
            int y = Random.Range(0, mapHeight);
            Vector3Int cellPosition = new Vector3Int(x, y, 0);
            Vector3 worldPosition = tilemap.CellToWorld(cellPosition);

            if (tilemap.GetTile(cellPosition) != null && !IsMineralAtPosition(worldPosition))
            {
                Instantiate(mineralPrefab, worldPosition, Quaternion.identity);
                mineralsPlaced++;
            }
        }*/
    }

    void SpawnMineralNearPosition(Vector3Int position)
    {
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if (i == 0 && j == 0) continue;

                Vector3Int newPos = position + new Vector3Int(i, j, 0);
                Vector3 worldPosition = tilemap.CellToWorld(newPos);

                if (tilemap.GetTile(newPos) != null && !IsMineralAtPosition(worldPosition))
                {
                    Instantiate(mineralPrefab, worldPosition, Quaternion.identity);
                    return;
                }
            }
        }
    }

    bool IsMineralAtPosition(Vector3 position)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, 0.1f, mineralLayer);
        return colliders.Length > 0;
    }
}
