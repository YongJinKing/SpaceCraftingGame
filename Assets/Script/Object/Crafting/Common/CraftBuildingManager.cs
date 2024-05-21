using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

public class CraftBuildingManager : MonoBehaviour
{
    public LayerMask layerMask;
    public Tilemap ground;
    public UnityEvent<Vector3> RemovePlaceEvent;
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
            Debug.Log(ground.cellSize.x + " " + ground.cellSize.y);
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
                    Instantiate(turret, new Vector3(cellPosition.x + ground.tileAnchor.x, cellPosition.y+ground.tileAnchor.y, 0), Quaternion.identity, TurretParent);
                    RemovePlaceEvent?.Invoke(cellPosition);
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
        Vector3 tmpPos;
        Vector3 cellPos;
        int rectIdx = 0;
        for(int i = 0; i < n; i++)
        {
            for(int j = 0; j < n; j++)
            {
                tmpPos = pos + new Vector3(ground.cellSize.x * j, ground.cellSize.y * i,0);
                cellPos = tmpPos + new Vector3(ground.tileAnchor.x, ground.tileAnchor.y, 0);
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

    void StopDrawingRectangle()
    {
        for(int i = 0; i < rectangles.Length; i++)
        {
            if (rectangles[i].gameObject.activeSelf) rectangles[i].gameObject.SetActive(false);
        }
    }

}
