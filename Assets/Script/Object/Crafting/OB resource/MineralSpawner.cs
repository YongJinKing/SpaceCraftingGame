using Newtonsoft.Json;
using JetBrains.Annotations;
using System.IO;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;


public class MineralGasSpawner : MonoBehaviour
{
    public GameObject mineralPrefab; // �̳׶� �������� �����ϱ� ���� ����
    public GameObject gasPrefab; // ���� �������� �����ϱ� ���� ����
    public GameObject plusResource; // 2x2 �ڿ�
    public Tilemap tilemap; // Ÿ�ϸ��� �����ϱ� ���� ����
    public int totalResources = 64; // ������ �� �ڿ��� ��
    public LayerMask mineralLayer; // �̳׶� ���̾ �����ϱ� ���� ����
    public LayerMask gasLayer; // ���� ���̾ �����ϱ� ���� ����
    public LayerMask plusResourceLayer;

    private CollectionResource collectionResource;
    private bool isPlusResourceSpawned;
    private int resourceIndex = 500000; // ���� �ε���

    private List<Resource_JsonData> resourceDataList = new List<Resource_JsonData>();

    //�Ʒ� �ּ� ó�� �� �κ��� �ڿ� ä�� ���� ��������Ʈ... ���� �ʿ���
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

        float totalCells = (tilemap.cellBounds.size.x - 98) * (tilemap.cellBounds.size.y - 80); // ��� ������ ���
        float mineralDensity = totalMinerals / totalCells;
        float gasDensity = totalGas / totalCells;

        int boundaryOffset = 2; // ��輱�� �ڿ��� �����Ǵ� ���� ����

        //�� �� ���� 4ĭ �˻� �� �ڿ� ���� 
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


    //�ڿ� �� ���� �Լ� 
   /* void SpawnResourceNearPosition(Vector3Int position, GameObject prefab, LayerMask layer)
    {
        int boundaryOffset = 2;

        for (int attempt = 0; attempt < 10; attempt++) // 10�� �õ�
        {
            int randomX = Random.Range(-2, 3); // -2~2���� ����
            int randomY = Random.Range(-2, 3);

            Vector3Int newPos = position + new Vector3Int(randomX, randomY, 0);
            Vector3 worldPosition = tilemap.CellToWorld(newPos);

            //Ÿ���� �����ϰ�, ��輱 �����̸�, �ڿ��� ���� ��ġ���� ����
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
        //json ������ 
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


    void SaveResourceDataToJson() // json ����
    {
        GameResourceList dataList = new GameResourceList { resources = resourceDataList };
        string json = JsonUtility.ToJson(dataList, true);
        string folderpath = Path.Combine(Application.dataPath,"Resources/Component/GameResources");
        string filepath = Path.Combine(folderpath, "Pexplorer_GameResources_Ability.json");
        File.WriteAllText(filepath, json);
        Debug.Log($"Resource data saved to {"���� ��ġ"}");

    }


    bool IsResourceAtPosition(Vector3 position)
    {
        Collider2D[] mineralColliders = Physics2D.OverlapCircleAll(position, 1.5f, mineralLayer);
        Collider2D[] gasColliders = Physics2D.OverlapCircleAll(position, 1.5f, gasLayer);
        Collider2D[] plusResourceColliders = Physics2D.OverlapCircleAll(position, 1.5f, plusResourceLayer);
        return mineralColliders.Length > 0 || gasColliders.Length > 0 || plusResourceColliders.Length > 0;
    }
}
