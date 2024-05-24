using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

public class CraftBuildingManager : MonoBehaviour
{
    public LayerMask layerMask;
    public Tilemap ground;
    public UnityEvent<Vector3Int> RemovePlaceEvent;
    public Transform turret;
    public Transform[] rectangles;
    public Transform TurretParent;

    public int size;
    TileManager tileManage;
    // Start is called before the first frame update
    void Start()
    {
        tileManage = FindObjectOfType<TileManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(tileManage.GetTileLength());
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, layerMask);
        if (hit.collider != null)
        {
            ground = hit.collider.gameObject.GetComponent<Tilemap>();
            //Debug.Log(ground.cellSize.x + " " + ground.cellSize.y);
            Vector3Int cellPosition = ground.LocalToCell(hit.point);
            /*if (tileManage.IsCraftable(cellPosition))
                DrawRectangle(new Vector3(cellPosition.x + ground.tileAnchor.x, cellPosition.y + ground.tileAnchor.y, 0), Color.green);
            else
            {
                DrawRectangle(new Vector3(cellPosition.x + ground.tileAnchor.x, cellPosition.y + ground.tileAnchor.y, 0), Color.red);
            }*/

            Draw_nSizeRectangle(cellPosition, size);
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log(cellPosition);
                if (tileManage.IsCraftable(cellPosition))
                {
                    /*Transform obj = Instantiate(turret, new Vector3(cellPosition.x + (ground.tileAnchor.x * size), cellPosition.y + (ground.tileAnchor.y * size), 0), Quaternion.identity, TurretParent);
                    obj.transform.localScale = Vector3.one * size;
                    RemovePlaceEvent?.Invoke(cellPosition);
                    */
                    MakeFalseCoordinates(cellPosition, size);
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
        for(int i = 0; i < n; i++)
        {
            for(int j = 0; j < n; j++)
            {
                drawPos = pos + new Vector3(ground.cellSize.x * j, ground.cellSize.y * i, 0);
                tmpPos = intPos + new Vector3Int((int)ground.cellSize.x * j, (int)ground.cellSize.y * i,0);
                cellPos = drawPos + new Vector3(ground.tileAnchor.x, ground.tileAnchor.y, 0);
                
                if (!tileManage.HasTile(tmpPos)) break;
                if (!rectangles[rectIdx].gameObject.activeSelf) rectangles[rectIdx].gameObject.SetActive(true);
                rectangles[rectIdx].transform.position = cellPos;
                if (tileManage.IsCraftable(tmpPos))
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

    void MakeFalseCoordinates(Vector3 pos, int size)
    {
        Vector3Int tmpPos = new Vector3Int((int)pos.x, (int)pos.y, (int)pos.z);
        Vector3Int cellPos;
        bool canBuild = true;
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                cellPos = tmpPos + new Vector3Int((int)ground.cellSize.x * j, (int)ground.cellSize.y * i, 0);
                if (!tileManage.HasTile(cellPos))
                {
                    canBuild = false;
                    break;
                }
                
                if (tileManage.IsCraftable(cellPos))
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

        if (canBuild)
        {
            Debug.Log("여기엔 지을 수 있어요");
            Transform obj = Instantiate(turret, new Vector3(pos.x + (ground.tileAnchor.x * size), pos.y + (ground.tileAnchor.y * size), 0), Quaternion.identity, TurretParent);
            obj.transform.localScale = Vector3.one * size;
            
            cellPos = Vector3Int.zero;
            for (int i = 0; i < size; i++)
            {
                for(int j = 0; j < size; j++)
                {
                    cellPos = tmpPos + new Vector3Int((int)ground.cellSize.x * j, (int)ground.cellSize.y * i, 0);
                    RemovePlaceEvent?.Invoke(cellPos);
                }
            }
        }
        
       
    }

    void StopDrawingRectangle()
    {
        for(int i = 0; i < rectangles.Length; i++)
        {
            if (rectangles[i].gameObject.activeSelf) rectangles[i].gameObject.SetActive(false);
        }
    }

}
