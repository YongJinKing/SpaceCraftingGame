using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MineralGasSpawner : MonoBehaviour
{
    public GameObject mineralPrefab; // 미네랄 프리팹을 연결하기 위한 변수
    public GameObject gasPrefab; // 가스 프리팹을 연결하기 위한 변수
    public Tilemap tilemap; // 타일맵을 연결하기 위한 변수
    public int mapWidth = 128; // 타일맵의 너비
    public int mapHeight = 128; // 타일맵의 높이
    public int totalResources = 32; // 생성할 총 자원의 수
    public LayerMask mineralLayer; // 미네랄 레이어를 설정하기 위한 변수
    public LayerMask gasLayer; // 가스 레이어를 설정하기 위한 변수;

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

        int boundaryOffset = 2;// 경계선에 자원이 생성되는것을 방지

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

        for(int attemp =0; attemp < 10; attemp++) // 10번 시도
        {
            int randomX = Random.Range(-2, 3); // -2~2사이 랜덤
            int randomY = Random.Range(-2, 3);

            Vector3Int newPos = position + new Vector3Int(randomX, randomY, 0);
            Vector3 worldPosition = tilemap.CellToWorld(newPos);

            //타일이 존재하고, 경계선 안쪽이며, 자원이 없는 위치에만 생성
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
