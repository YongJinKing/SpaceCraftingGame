using Newtonsoft.Json;
using JetBrains.Annotations;
using System.IO;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;


public class MineralGasSpawner : MonoBehaviour
{
    public GameObject mineralPrefab; // 미네랄 프리팹을 연결하기 위한 변수
    public GameObject gasPrefab; // 가스 프리팹을 연결하기 위한 변수
    public GameObject plusResource; // 2x2 자원
    public Tilemap tilemap; // 타일맵을 연결하기 위한 변수
    public int totalResources = 64; // 생성할 총 자원의 수
    public LayerMask mineralLayer; // 미네랄 레이어를 설정하기 위한 변수
    public LayerMask gasLayer; // 가스 레이어를 설정하기 위한 변수
    public LayerMask plusResourceLayer;

    private CollectionResource collectionResource;
    private bool isPlusResourceSpawned;
    private int resourceIndex = 500000; // 시작 인덱스

    private List<Resource_JsonData> resourceDataList = new List<Resource_JsonData>();

    //아래 주석 처리 된 부분은 자원 채취 관련 델리게이트... 수정 필요함
   /* private void OnEnable()
    {
        CollectionResource.OnMineralHarvested += (pos) => SpawnResourceNearPosition(pos, mineralPrefab, mineralLayer);
        CollectionResource.OnGasHarvested += (pos) => SpawnResourceNearPosition(pos, gasPrefab, gasLayer);
    }

    private void OnDisable()
    {
        CollectionResource.OnMineralHarvested -= (pos) => SpawnResourceNearPosition(pos, mineralPrefab, mineralLayer);
        CollectionResource.OnGasHarvested -= (pos) => SpawnResourceNearPosition(pos, gasPrefab, gasLayer);
    }*/

    void Start()
    {
        collectionResource = GetComponent<CollectionResource>();
        SpawnResources();
        SpawnPlusResource();
        SaveResourceDataToJson();
    }

    void SpawnPlusResource()
    {
        if (isPlusResourceSpawned)
        {
            return; 
        }
        int boundaryOffset = 2;
        for (int attempt = 0; attempt < 100; attempt++)
        {
            int x = Random.Range(tilemap.cellBounds.xMin + boundaryOffset, tilemap.cellBounds.xMax - boundaryOffset);
            int y = Random.Range(tilemap.cellBounds.yMin + boundaryOffset, tilemap.cellBounds.yMax - boundaryOffset);
            Vector3Int cellPosition = new Vector3Int(x, y, 0);
            Vector3 worldPosition = tilemap.CellToWorld(cellPosition);

            if (tilemap.GetTile(cellPosition) != null && !IsResourceAtPosition(worldPosition))
            {
                Instantiate(plusResource, worldPosition, Quaternion.identity);
                isPlusResourceSpawned = true;
                AddResourceData(2, worldPosition, "[100000]", "[10]", 1, 5);
                break;
            }
        }
    }

    void SpawnResources()
    {
        int mineralsPlaced = 0;
        int gasPlaced = 0;
        int totalMinerals = (int)(totalResources * 0.8f);
        int totalGas = totalResources - totalMinerals;

        float totalCells = (tilemap.cellBounds.size.x - 98) * (tilemap.cellBounds.size.y - 80); // 경계 오프셋 고려
        float mineralDensity = totalMinerals / totalCells;
        float gasDensity = totalGas / totalCells;

        int boundaryOffset = 2; // 경계선에 자원이 생성되는 것을 방지

        //각 축 별로 4칸 검사 후 자원 생성 
        for (int y = tilemap.cellBounds.yMin + boundaryOffset; y < tilemap.cellBounds.yMax - boundaryOffset; y+=4)
        {
            for (int x = tilemap.cellBounds.xMin + boundaryOffset; x < tilemap.cellBounds.xMax - boundaryOffset; x+=4)
            {
                if (mineralsPlaced >= totalMinerals && gasPlaced >= totalGas)
                {
                    return;
                }

                Vector3Int cellPosition = new Vector3Int(x, y, 0);
                Vector3 worldPosition = tilemap.CellToWorld(cellPosition);

                if (tilemap.GetTile(cellPosition) != null && !IsResourceAtPosition(worldPosition))
                {
                    if (mineralsPlaced < totalMinerals && Random.value < mineralDensity)
                    {
                        Instantiate(mineralPrefab, worldPosition, Quaternion.identity);
                        mineralsPlaced++;
                        AddResourceData(0, worldPosition, "[100000]", "[10]", 1, 5);
                    }
                    else if (gasPlaced < totalGas && Random.value < gasDensity)
                    {
                        Instantiate(gasPrefab, worldPosition, Quaternion.identity);
                        gasPlaced++;
                        AddResourceData(1, worldPosition, "[100001]", "[3]", 1, 1);
                    }
                }
            }
        }
    }


    //자원 재 생성 함수 
   /* void SpawnResourceNearPosition(Vector3Int position, GameObject prefab, LayerMask layer)
    {
        int boundaryOffset = 2;

        for (int attempt = 0; attempt < 10; attempt++) // 10번 시도
        {
            int randomX = Random.Range(-2, 3); // -2~2사이 랜덤
            int randomY = Random.Range(-2, 3);

            Vector3Int newPos = position + new Vector3Int(randomX, randomY, 0);
            Vector3 worldPosition = tilemap.CellToWorld(newPos);

            //타일이 존재하고, 경계선 안쪽이며, 자원이 없는 위치에만 생성
            if (tilemap.GetTile(newPos) != null &&
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
   */
   
    void AddResourceData(int type, Vector3 position, string sponItem, string itemCount, int hasPercentage, int percentageMinimum)
    {
        //json 데이터 
        Resource_JsonData data = new Resource_JsonData
        {
            GameResources_Index = resourceIndex++,
            GameResources_Type = type,
            GameResources_SponItem = sponItem,
            GameResources_ItemCount = itemCount,
            GameResources_HasRercentage = hasPercentage,
            GameResources_PercentageMinimum = percentageMinimum
        };
        resourceDataList.Add(data);
    }


    void SaveResourceDataToJson() // json 저장
    {
        GameResourceList dataList = new GameResourceList { resources = resourceDataList };
        string json = JsonUtility.ToJson(dataList, true);
        string folderpath = Path.Combine(Application.dataPath,"Resources/Component/GameResources");
        string filepath = Path.Combine(folderpath, "Pexplorer_GameResources_Ability.json");
        File.WriteAllText(filepath, json);
        Debug.Log($"Resource data saved to {"파일 패치"}");

    }


    bool IsResourceAtPosition(Vector3 position)
    {
        Collider2D[] mineralColliders = Physics2D.OverlapCircleAll(position, 1.5f, mineralLayer);
        Collider2D[] gasColliders = Physics2D.OverlapCircleAll(position, 1.5f, gasLayer);
        Collider2D[] plusResourceColliders = Physics2D.OverlapCircleAll(position, 1.5f, plusResourceLayer);
        return mineralColliders.Length > 0 || gasColliders.Length > 0 || plusResourceColliders.Length > 0;
    }
}
