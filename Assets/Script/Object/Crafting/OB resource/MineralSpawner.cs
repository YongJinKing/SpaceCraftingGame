using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MineralSpawner : MonoBehaviour
{
    public GameObject mineralPrefab; // �̳׶� �������� �����ϱ� ���� ����
    public Tilemap tilemap; // Ÿ�ϸ��� �����ϱ� ���� ����
    public int mapWidth = 128; // Ÿ�ϸ��� �ʺ�
    public int mapHeight = 128; // Ÿ�ϸ��� ����
    public int numberOfMinerals = 16; // ������ �̳׶��� ��
    public LayerMask mineralLayer; // �̳׶� ���̾ �����ϱ� ���� ����

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

        // �̳׶��� ������ ��� �߰��� ���� (���� ó��)
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
