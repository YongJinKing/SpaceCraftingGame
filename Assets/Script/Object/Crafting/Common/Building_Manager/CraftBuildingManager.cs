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
        //tileManage = FindObjectOfType<TileManager>(); // tilemanager singleton화 시키는중
        factory = new CraftFactory();
        buildingIndex = 110000; // 빌딩 인덱스, 추후 건축 모드에서 Ui를 통해 이 인덱스를 원하는 건물의 인덱스로 변경할 수 있어야함  <<<<<<<<<<<<
        size = factory.GetBuildingSize(buildingIndex);
    }

    // Update is called once per frame
    void Update()
    {
        // 지금은 단순히 1번,2번키를 눌러서 지을 건물을 바꾸지만 아래에 else if 까지의 코드는 UI 개발시 버튼을 클릭해서 바꾸는 식으로 바꿔야함
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            buildingIndex = 110000;
            size = factory.GetBuildingSize(buildingIndex);
            Debug.Log("현재 건설 선택된 인덱스 : " + buildingIndex);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            buildingIndex = 100000;
            size = factory.GetBuildingSize(buildingIndex);
            Debug.Log("현재 건설 선택된 인덱스 : " + buildingIndex);
        }
        //===========================위의 코드들은 수정해야함==================================================

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
                //if (tileManage.IsCraftable(cellPosition))  // 타일에 건축이 가능하다면<< 이것만 체크하는데 이제 필요한 자원까지 생각해서 건축할 수 있는지 검사해야함
                if (TileManager.Instance.IsCraftable(cellPosition))
                {

                    MakeFalseCoordinates(cellPosition, buildingIndex, size);
                }
                else
                {
                    Debug.Log("클릭한 위치에 건물이 있어 지을 수 없어요");
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

    // NxN 사이즈의 박스를 그리기 위해 NxN번 반복하며 그린다.
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
                Debug.Log("클릭한 위치 범위 내에 건물이 있어 지을 수 없어요");
                break;
            }
        }

        if (canBuild) // 해당 위치에 지을 수 있지만, 내가 가지고 있는 자원량과도 비교해야함 << 이거 추가해야함
        {
            Debug.Log("여기엔 지을 수 있어요");

            // 자원이 부족하면 못지어야함

            // 아래는 건물을 짓는 코드들, 이것도 바로 짓는게 아니라 건물이 지어지는 느낌을 연출해야 하니 아래 코드들은 코루틴으로 이동?
            // 건물 건설 연출 코루틴 -> 코루틴이 끝날때 아래 건설 코드들 실행
            //Inventory.instance.UseItem(10000, 5); // <<<<<<<< 인벤토리에서 10000번 인덱스의 자원을 5개 만큼 사용한다. 그런데 이제 10000번이나 5개 모두 json에서 읽어와서 적용해야함, 건물마다 다르니깐
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
