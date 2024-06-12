using System.IO;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;


public class MineralGasSpawner : MonoBehaviour
{
    public GameObject mineralPrefab; // 미네랄 프리팹을 연결하기 위한 변수
    public GameObject gasPrefab; // 가스 프리팹을 연결하기 위한 변수
    public GameObject plusResource; // 2x2 자원
    public TileManager tileManager; // 타일 매니저를 연결하기 위한 변수
    public LayerMask mineralLayer; // 미네랄 레이어를 설정하기 위한 변수
    public LayerMask gasLayer; // 가스 레이어를 설정하기 위한 변수
    public LayerMask plusResourceLayer;

    private const int totalResources = 224;
    private const float mineralRatio = 0.8f;
    private const float plusMineralRatio = 0.01f;

 
    public int resourceIndex = 500000; // 시작 인덱스 

    public List<Resource_JsonData> resourceDataList = new List<Resource_JsonData>();


    void Start()
    {
        SpawnResources();
        LoadResourceDataFromJson();
        SaveResourceDataToJson();
    }

    public void SpawnResources()
    {
        int totalMinerals = Mathf.RoundToInt(totalResources * mineralRatio);//5376 ,16384
        int totalGas = totalResources - totalMinerals;
        int plusMinerals = Mathf.RoundToInt(totalMinerals * plusMineralRatio);

        int mineralsPlaced = 0;
        int gasPlaced = 0;

        float totalCells = (TileManager.Instance.tileMap.cellBounds.size.x - 64) * (TileManager.Instance.tileMap.cellBounds.size.y - 80); // 경계 오프셋 고려
        float mineralDensity = totalMinerals / totalCells;
        float gasDensity = totalGas / totalCells;

        int boundaryOffset = 1; // 경계선에 자원이 생성되는 것을 방지
        HashSet<Vector2Int> placedResources = new HashSet<Vector2Int>();
        SpawnPlusMinerals(plusMinerals);


        //각 축 별로 4칸 검사 후 자원 생성 
        for (int y = TileManager.Instance.tileMap.cellBounds.yMin + boundaryOffset; y < TileManager.Instance.tileMap.cellBounds.yMax - boundaryOffset; y += 4)
        {
            for (int x = TileManager.Instance.tileMap.cellBounds.xMin + boundaryOffset; x < TileManager.Instance.tileMap.cellBounds.xMax + boundaryOffset; x += 4)
            {
                if (mineralsPlaced >= totalMinerals && gasPlaced >= totalGas)
                {
                    return;
                }

                //Vector2Int cellPosition = new Vector2Int(x + Random.Range(0, 4), y + Random.Range(0, 4));
                Vector2Int cellPosition = new Vector2Int(x, y);
                Vector3 worldPosition = TileManager.Instance.GetWorldPosCenterOfCell(cellPosition);
                Vector3Int gridPosition = new Vector3Int((int)worldPosition.x, (int)worldPosition.y, 0);

                if (placedResources.Contains(cellPosition) || TileManager.Instance.tileMap.GetTile(gridPosition) == null || IsResourceAtPosition(worldPosition))
                {
                    continue;
                }

                if (TileManager.Instance.tileMap.GetTile(gridPosition) != null && !IsResourceAtPosition(worldPosition))
                {
                    if (TileManager.Instance.HasTile(gridPosition))
                    {
                        TileManager.Instance.RemopvePlace(gridPosition);
                    }
                    if (mineralsPlaced < totalMinerals && Random.value < mineralDensity)
                    {
                        Instantiate(mineralPrefab, worldPosition, Quaternion.identity);
                        mineralsPlaced++;
                        AddResourceData(0, worldPosition, new int[] { 100000 }, new int[] { 5 }, new int[] { 10 }, 0.8f);
                    }
                    else if (gasPlaced < totalGas && Random.value < gasDensity)
                    {
                        Instantiate(gasPrefab, worldPosition, Quaternion.identity);
                        gasPlaced++;
                        AddResourceData(1, worldPosition, new int[] { 100001 }, new int[] { 1 }, new int[] { 5 }, 0.2f);
                    }
                }
            }
        }
        void SpawnPlusMinerals(int plusMinerals)
        {
            int boundaryOffset = 2;
            for (int i = 0; i < plusMinerals; i++)
            {
                for (int attempt = 0; attempt < 100; attempt++)
                {
                    int x = Random.Range(TileManager.Instance.tileMap.cellBounds.xMin + boundaryOffset, TileManager.Instance.tileMap.cellBounds.xMax - boundaryOffset);
                    int y = Random.Range(TileManager.Instance.tileMap.cellBounds.yMin + boundaryOffset, TileManager.Instance.tileMap.cellBounds.yMax - boundaryOffset);
                    Vector2Int cellPosition = new Vector2Int(x, y);
                    Vector3 worldPosition = TileManager.Instance.GetWorldPosCenterOfCell(cellPosition);
                    Vector3Int gridPosition = new Vector3Int((int)worldPosition.x, (int)worldPosition.y, 0);
                    Vector3 place = new Vector3(worldPosition.x + (TileManager.Instance.tileMap.cellSize.x * 0.5f), worldPosition.y + (TileManager.Instance.tileMap.cellSize.y * 0.5f), 0);

                    if (placedResources.Contains(cellPosition) || TileManager.Instance.tileMap.GetTile(gridPosition) == null || IsResourceAtPosition(worldPosition))
                    {
                        continue;
                    }

                    if (TileManager.Instance.tileMap.GetTile(gridPosition) != null && !IsResourceAtPosition(worldPosition))
                    {
                        Instantiate(plusResource, worldPosition, Quaternion.identity);
                        AddResourceData(2, worldPosition, new int[] { 100000 }, new int[] { 5 }, new int[] { 10 }, 0.01f);
                        break;
                    }
                }
            }
        }
    }
   
    public void AddResourceData(int type, Vector3 position, int[] sponItem, int[] itemCount, int[] hasPercentage, float percentageMinimum)
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


    [ContextMenu("Save Resource Data To JSON")]
    public void SaveResourceDataToJson() // json 저장
    {
        GameResourceList dataList = new GameResourceList { resources = resourceDataList };
        string json = JsonUtility.ToJson(dataList, true);
        string folderpath = Path.Combine(Application.dataPath, "Resources/Component/GameResources");
        string filepath = Path.Combine(folderpath, "Pexplorer_GameResources_Ability.json");
        File.WriteAllText(filepath, json);
        Debug.Log($"Resource data saved to {filepath}");
    }

    [ContextMenu("Load Resource Data From JSON")]
    public void LoadResourceDataFromJson() // json 불러오기
    {
        string folderpath = Path.Combine(Application.dataPath, "Resources/Component/GameResources");
        string filepath = Path.Combine(folderpath, "Pexplorer_GameResources_Ability.json");
        if (File.Exists(filepath))
        {
            string json = File.ReadAllText(filepath);
            GameResourceList dataList = JsonUtility.FromJson<GameResourceList>(json);
            resourceDataList = dataList.resources;
            Debug.Log($"Resource data loaded from {filepath}");
        }
    }

    bool IsResourceAtPosition(Vector3 position)
    {
        Collider2D[] mineralColliders = Physics2D.OverlapCircleAll(position, 1.5f, mineralLayer);
        Collider2D[] gasColliders = Physics2D.OverlapCircleAll(position, 1.5f, gasLayer);
        Collider2D[] plusResourceColliders = Physics2D.OverlapCircleAll(position, 3.5f, plusResourceLayer);
        return mineralColliders.Length > 0 || gasColliders.Length > 0 || plusResourceColliders.Length > 0;
    }
}
