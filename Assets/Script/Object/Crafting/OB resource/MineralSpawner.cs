using UnityEngine;
using UnityEngine.Tilemaps;

public class MineralGasSpawner : MonoBehaviour
{
    public GameObject mineralPrefab; // �̳׶� �������� �����ϱ� ���� ����
    public GameObject gasPrefab; // ���� �������� �����ϱ� ���� ����
    public Tilemap tilemap; // Ÿ�ϸ��� �����ϱ� ���� ����
    public int mapWidth = 128; // Ÿ�ϸ��� �ʺ�
    public int mapHeight = 128; // Ÿ�ϸ��� ����
    public int totalResources = 8; // ������ �� �ڿ��� ��
    public LayerMask mineralLayer; // �̳׶� ���̾ �����ϱ� ���� ����
    public LayerMask gasLayer; // ���� ���̾ �����ϱ� ���� ����

    private CollectionResource collectionResource;

    private void OnEnable()
    {
        CollectionResource.OnMineralHarvested += (pos) => SpawnResourceNearPosition(pos, mineralPrefab, mineralLayer);
        CollectionResource.OnGasHarvested += (pos) => SpawnResourceNearPosition(pos, gasPrefab, gasLayer);
    }

    private void OnDisable()
    {
        CollectionResource.OnMineralHarvested -= (pos) => SpawnResourceNearPosition(pos, mineralPrefab, mineralLayer);
        CollectionResource.OnGasHarvested -= (pos) => SpawnResourceNearPosition(pos, gasPrefab, gasLayer);
    }

    void Start()
    {
        collectionResource = GetComponent<CollectionResource>();
        SpawnResources();
    }

    void SpawnResources()
    {
        int mineralsPlaced = 0;
        int gasPlaced = 0;
        int totalMinerals = (int)(totalResources * 0.7f);
        int totalGas = totalResources - totalMinerals;

        float mineralDensity = (float)totalMinerals / (mapWidth * mapHeight);
        float gasDensity = (float)totalGas / (mapWidth * mapHeight);

        for (int y = tilemap.cellBounds.yMin; y < tilemap.cellBounds.yMax; y++)
        {
            for (int x = tilemap.cellBounds.xMin; x < tilemap.cellBounds.xMax; x++)
            {
                if (mineralsPlaced >= totalMinerals && gasPlaced >= totalGas)
                {
                    return;
                }

                Vector3Int cellPosition = new Vector3Int(x, y, 0);
                Vector3 worldPosition = tilemap.CellToWorld(cellPosition);

                if (tilemap.GetTile(cellPosition) != null)
                {
                    if (mineralsPlaced < totalMinerals && !IsResourceAtPosition(worldPosition, mineralLayer) && Random.value < mineralDensity)
                    {
                        Instantiate(mineralPrefab, worldPosition, Quaternion.identity);
                        mineralsPlaced++;
                    }
                    else if (gasPlaced < totalGas && !IsResourceAtPosition(worldPosition, gasLayer) && Random.value < gasDensity)
                    {
                        Instantiate(gasPrefab, worldPosition, Quaternion.identity);
                        gasPlaced++;
                    }
                }
            }
        }

        // �ڿ��� ������ ��� �߰��� ���� (���� ó��)
        /*while (mineralsPlaced < totalMinerals || gasPlaced < totalGas)
        {
            int x = Random.Range(0, mapWidth);
            int y = Random.Range(0, mapHeight);
            Vector3Int cellPosition = new Vector3Int(x, y, 0);
            Vector3 worldPosition = tilemap.CellToWorld(cellPosition);

            if (tilemap.GetTile(cellPosition) != null)
            {
                if (mineralsPlaced < totalMinerals && !IsResourceAtPosition(worldPosition, mineralLayer))
                {
                    Instantiate(mineralPrefab, worldPosition, Quaternion.identity);
                    mineralsPlaced++;
                }
                else if (gasPlaced < totalGas && !IsResourceAtPosition(worldPosition, gasLayer))
                {
                    Instantiate(gasPrefab, worldPosition, Quaternion.identity);
                    gasPlaced++;
                }
            }
        }*/
    }

    void SpawnResourceNearPosition(Vector3Int position, GameObject prefab, LayerMask layer)
    {
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if (i == 0 && j == 0) continue;

                Vector3Int newPos = position + new Vector3Int(i, j, 0);
                Vector3 worldPosition = tilemap.CellToWorld(newPos);

                if (tilemap.GetTile(newPos) != null && !IsResourceAtPosition(worldPosition, layer))
                {
                    Instantiate(prefab, worldPosition, Quaternion.identity);
                    return;
                }
            }
        }
    }

    bool IsResourceAtPosition(Vector3 position, LayerMask layer)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, 0.1f, layer);
        return colliders.Length > 0;
    }
}

