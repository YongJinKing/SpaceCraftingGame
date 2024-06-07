using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

public class CraftBuildingManager : MonoBehaviour
{
    public LayerMask layerMask;
    public Tilemap ground;
    public UnityEvent<Vector3Int, GameObject, int> WritePlaceInfoEvent;
    public UnityEvent<Vector3Int> RemovePlaceEvent;
    public Transform turret;
    public Transform[] rectangles;
    public Transform TurretParent;

    public int size;
    [SerializeField] int buildingIndex;
    //TileManager tileManage;
    CraftFactory factory;
    // Start is called before the first frame update
    void Start()
    {
        //tileManage = FindObjectOfType<TileManager>(); // tilemanager singletonȭ ��Ű����
        factory = new CraftFactory();
        buildingIndex = 110000; // ���� �ε���, ���� ���� ��忡�� Ui�� ���� �� �ε����� ���ϴ� �ǹ��� �ε����� ������ �� �־����  <<<<<<<<<<<<
        size = factory.GetBuildingSize(buildingIndex);
    }

    // Update is called once per frame
    void Update()
    {
        // ������ �ܼ��� 1��,2��Ű�� ������ ���� �ǹ��� �ٲ����� �Ʒ��� else if ������ �ڵ�� UI ���߽� ��ư�� Ŭ���ؼ� �ٲٴ� ������ �ٲ����
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            buildingIndex = 110000;
            size = factory.GetBuildingSize(buildingIndex);
            Debug.Log("���� �Ǽ� ���õ� �ε��� : " + buildingIndex);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            buildingIndex = 100000;
            size = factory.GetBuildingSize(buildingIndex);
            Debug.Log("���� �Ǽ� ���õ� �ε��� : " + buildingIndex);
        }
        //===========================���� �ڵ���� �����ؾ���==================================================

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, layerMask);
        if (hit.collider != null)
        {
            ground = hit.collider.gameObject.GetComponent<Tilemap>();
            Vector3Int cellPosition = ground.LocalToCell(hit.point);

            Draw_nSizeRectangle(cellPosition, size);
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log(cellPosition);
                //if (tileManage.IsCraftable(cellPosition))  // Ÿ�Ͽ� ������ �����ϴٸ�<< �̰͸� üũ�ϴµ� ���� �ʿ��� �ڿ����� �����ؼ� ������ �� �ִ��� �˻��ؾ���
                if (TileManager.Instance.IsCraftable(cellPosition))
                {

                    MakeFalseCoordinates(cellPosition, buildingIndex, size);
                }
                else
                {
                    Debug.Log("Ŭ���� ��ġ�� �ǹ��� �־� ���� �� �����");
                }
            }
        }
        else
        {
            StopDrawingRectangle();
        }

    }

    void DrawRectangle(Vector3 pos, Color color)
    {
        if (!rectangles[0].gameObject.activeSelf) rectangles[0].gameObject.SetActive(true);
        rectangles[0].transform.position = pos;
        Color tmpColor = color;
        tmpColor.a = 0.5f;
        rectangles[0].transform.GetComponent<SpriteRenderer>().color = tmpColor;
    }

    // NxN �������� �ڽ��� �׸��� ���� NxN�� �ݺ��ϸ� �׸���.
    void Draw_nSizeRectangle(Vector3 pos, int n)
    {
        StopDrawingRectangle();
        Vector3Int intPos = new Vector3Int((int)pos.x, (int)pos.y, (int)pos.z);
        Vector3Int tmpPos;
        Vector3 cellPos;
        Vector3 drawPos = pos;
        int rectIdx = 0;
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                drawPos = pos + new Vector3(ground.cellSize.x * j, ground.cellSize.y * i, 0);
                tmpPos = intPos + new Vector3Int((int)ground.cellSize.x * j, (int)ground.cellSize.y * i, 0);
                cellPos = drawPos + new Vector3(ground.tileAnchor.x, ground.tileAnchor.y, 0);

                //if (!tileManage.HasTile(tmpPos)) break;
                if (!TileManager.Instance.HasTile(tmpPos)) break;
                if (!rectangles[rectIdx].gameObject.activeSelf) rectangles[rectIdx].gameObject.SetActive(true);
                rectangles[rectIdx].transform.position = cellPos;
                //if (tileManage.IsCraftable(tmpPos))
                if (TileManager.Instance.IsCraftable(tmpPos))
                {
                    Color tmpColor = Color.green;
                    tmpColor.a = 0.5f;
                    rectangles[rectIdx].transform.GetComponent<SpriteRenderer>().color = tmpColor;
                }
                else
                {
                    Color tmpColor = Color.red;
                    tmpColor.a = 0.5f;
                    rectangles[rectIdx].transform.GetComponent<SpriteRenderer>().color = tmpColor;
                }
                rectIdx++;
            }
        }
    }

    void MakeFalseCoordinates(Vector3 pos, int index, int size)
    {
        Vector3Int tmpPos = new Vector3Int((int)pos.x, (int)pos.y, (int)pos.z);
        Vector3Int cellPos;
        bool canBuild = true;
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                cellPos = tmpPos + new Vector3Int((int)ground.cellSize.x * j, (int)ground.cellSize.y * i, 0);
                //if (!tileManage.HasTile(cellPos))
                if (!TileManager.Instance.HasTile(cellPos))
                {
                    canBuild = false;
                    break;
                }

                //if (tileManage.IsCraftable(cellPos))
                if (TileManager.Instance.IsCraftable(cellPos))
                {
                    continue;
                }
                else
                {
                    canBuild = false;
                    break;
                }
            }
            if (!canBuild)
            {
                Debug.Log("Ŭ���� ��ġ ���� ���� �ǹ��� �־� ���� �� �����");
                break;
            }
        }

        if (canBuild) // �ش� ��ġ�� ���� �� ������, ���� ������ �ִ� �ڿ������� ���ؾ��� << �̰� �߰��ؾ���
        {
            Debug.Log("���⿣ ���� �� �־��");

            // �ڿ��� �����ϸ� ���������

            // �Ʒ��� �ǹ��� ���� �ڵ��, �̰͵� �ٷ� ���°� �ƴ϶� �ǹ��� �������� ������ �����ؾ� �ϴ� �Ʒ� �ڵ���� �ڷ�ƾ���� �̵�?
            // �ǹ� �Ǽ� ���� �ڷ�ƾ -> �ڷ�ƾ�� ������ �Ʒ� �Ǽ� �ڵ�� ����
            //Inventory.instance.UseItem(10000, 5); // <<<<<<<< �κ��丮���� 10000�� �ε����� �ڿ��� 5�� ��ŭ ����Ѵ�. �׷��� ���� 10000���̳� 5�� ��� json���� �о�ͼ� �����ؾ���, �ǹ����� �ٸ��ϱ�
            Vector3 craftPos = new Vector3((pos.x + (ground.tileAnchor.x * size)), (pos.y + (ground.tileAnchor.y * size)), 0);
            GameObject craft = factory.CraftBuilding(index, craftPos);
            craft.transform.localScale = Vector3.one * size;
            craft.transform.SetParent(TurretParent);

            cellPos = Vector3Int.zero;
            WritePlaceInfoEvent?.Invoke(tmpPos, craft, size);
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    cellPos = tmpPos + new Vector3Int((int)ground.cellSize.x * j, (int)ground.cellSize.y * i, 0);
                    RemovePlaceEvent?.Invoke(cellPos);
                }
            }
        }


    }

    void StopDrawingRectangle()
    {
        for (int i = 0; i < rectangles.Length; i++)
        {
            if (rectangles[i].gameObject.activeSelf) rectangles[i].gameObject.SetActive(false);
        }
    }

}
