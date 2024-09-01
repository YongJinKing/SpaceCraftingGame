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
    public UnityEvent ActiveCantBuildThere;
    public Transform turret;
    public Transform[] rectangles;
    public Transform TurretParent;

    public int size;
    public bool craftReady;
    [SerializeField] int buildingIndex;
    // Start is called before the first frame update
    void Start()
    {
        buildingIndex = 110000;
        size = CraftFactory.Instance.GetBuildingSize(buildingIndex);
        craftReady = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!craftReady)
        {
            StopDrawingRectangle();
            return; // 건축 준비가 완료 되어야 아래 코드들을 진행
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            craftReady = false;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, layerMask);
        if (hit.collider != null)
        {
            ground = hit.collider.gameObject.GetComponent<Tilemap>();
            Vector3Int cellPosition = ground.WorldToCell(hit.point);

            Draw_nSizeRectangle(cellPosition, size);
            if (Input.GetMouseButtonDown(0))
            {
                if (TileManager.Instance.IsCraftable(cellPosition))
                {
                    MakeFalseCoordinates(cellPosition, buildingIndex, size);
                }
            }
        }
        else
        {
            StopDrawingRectangle();
        }

    }

    public void BuildingSetting(int index)
    {
        craftReady = true;
        buildingIndex = index;
        size = CraftFactory.Instance.GetBuildingSize(buildingIndex);
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

                if (!TileManager.Instance.HasTile(tmpPos)) break;
                if (!rectangles[rectIdx].gameObject.activeSelf) rectangles[rectIdx].gameObject.SetActive(true);
                rectangles[rectIdx].transform.position = cellPos;
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
                if (!TileManager.Instance.HasTile(cellPos))
                {
                    canBuild = false;
                    break;
                }

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
                break;
            }
        }

        if (canBuild) // 해당 위치에 지을 수 있지만, 내가 가지고 있는 자원량과도 비교해야함 << 이거 추가해야함
        {
            Vector3 craftPos = new Vector3((pos.x + ground.tileAnchor.x), (pos.y + ground.tileAnchor.y), 0);

            GameObject craft = CraftFactory.Instance.ReadyToCraftBuilding(index, craftPos, 0, size);
            if (craft == null)
            {
                craftReady = false;
                StopDrawingRectangle();
                return;
            }
            craft.transform.localScale = Vector3.one * size;
            craft.transform.SetParent(TurretParent);

            cellPos = Vector3Int.zero;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    cellPos = tmpPos + new Vector3Int((int)ground.cellSize.x * j, (int)ground.cellSize.y * i, 0);
                    RemovePlaceEvent?.Invoke(cellPos);
                }
            }
        }
        else
        {
            ActiveCantBuildThere?.Invoke();
        }
        craftReady = false;
        StopDrawingRectangle();
    }

    void StopDrawingRectangle()
    {
        for (int i = 0; i < rectangles.Length; i++)
        {
            if (rectangles[i].gameObject.activeSelf) rectangles[i].gameObject.SetActive(false);
        }
        
    }

}
