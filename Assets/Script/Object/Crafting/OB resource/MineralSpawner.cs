using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MineralGasSpawner : MonoBehaviour
{
    public GameObject mineralPrefab; // �̳׶� �������� �����ϱ� ���� ����
    public GameObject gasPrefab; // ���� �������� �����ϱ� ���� ����
    public Tilemap tilemap; // Ÿ�ϸ��� �����ϱ� ���� ����
    public int mapWidth = 128; // Ÿ�ϸ��� �ʺ�
    public int mapHeight = 128; // Ÿ�ϸ��� ����
    public int totalResources = 32; // ������ �� �ڿ��� ��
    public LayerMask mineralLayer; // �̳׶� ���̾ �����ϱ� ���� ����
    public LayerMask gasLayer; // ���� ���̾ �����ϱ� ���� ����;

    private CollectionResource collectionResource;
    private bool isplusResourceSpawned;

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
        int totalMinerals = (int)(totalResources * 0.8f);
        int totalGas = totalResources - totalMinerals;

        float mineralDensity = (float)totalMinerals / (mapWidth * mapHeight);
        float gasDensity = (float)totalGas / (mapWidth * mapHeight);

        int boundaryOffset = 2;// ��輱�� �ڿ��� �����Ǵ°��� ����

        for (int y = tilemap.cellBounds.yMin + boundaryOffset ; y < tilemap.cellBounds.yMax; y += 2)
        {
            for (int x = tilemap.cellBounds.xMin + boundaryOffset; x < tilemap.cellBounds.xMax; x += 2)
            {
   
               Vector3Int cellPosition = new Vector3Int(x, y, 0);
               Vector3 worldPosition = tilemap.CellToWorld(cellPosition);

               if (tilemap.GetTile(cellPosition) != null)
               {
                  if (mineralsPlaced < totalMinerals && !IsResourceAtPosition(worldPosition) && Random.value < mineralDensity)
                  {
                     Instantiate(mineralPrefab, worldPosition, Quaternion.identity);
                      mineralsPlaced++;
                  }
                  else if (gasPlaced < totalGas && !IsResourceAtPosition(worldPosition) && Random.value < gasDensity)
                  {
                     Instantiate(gasPrefab, worldPosition, Quaternion.identity);
                     gasPlaced++;
                  }
                  if(mineralsPlaced >= totalMinerals && gasPlaced >= totalGas)
                  {
                     return;
                  }
               }

            }
        }
    }

    void SpawnResourceNearPosition(Vector3Int position, GameObject prefab, LayerMask layer)
    {
        int boundaryOffset = 2;

        for(int attemp =0; attemp < 10; attemp++) // 10�� �õ�
        {
            int randomX = Random.Range(-2, 3); // -2~2���� ����
            int randomY = Random.Range(-2, 3);

            Vector3Int newPos = position + new Vector3Int(randomX, randomY, 0);
            Vector3 worldPosition = tilemap.CellToWorld(newPos);

            //Ÿ���� �����ϰ�, ��輱 �����̸�, �ڿ��� ���� ��ġ���� ����
            if(tilemap.GetTile(newPos) != null &&
                newPos.x >= tilemap.cellBounds.xMin + boundaryOffset &&
                newPos.x <= tilemap.cellBounds.xMax - boundaryOffset &&
                newPos.y >= tilemap.cellBounds.yMin + boundaryOffset &&
                newPos.y <= tilemap.cellBounds.yMax - boundaryOffset &&
                !IsResourceAtPosition(worldPosition))
            {
                Instantiate(prefab, worldPosition, Quaternion.identity);
                return;
            }
        }
    }

    bool IsResourceAtPosition(Vector3 position)
    {
        Collider2D[] mineralColliders = Physics2D.OverlapCircleAll(position, 1.5f, mineralLayer);
        Collider2D[] gasColliders = Physics2D.OverlapCircleAll(position, 1.5f, gasLayer);
        return mineralColliders.Length > 0 || gasColliders.Length > 0;
    }
}
