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
    TileManager tileManage;
    int size;
    // Start is called before the first frame update
    void Start()
    {
        tileManage = FindObjectOfType<TileManager>();
        size = tileManage.GetTileLength();
    }

    // Update is called once per frame
    void Update()
    {
        
        Debug.Log(tileManage.GetTileLength());
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, layerMask);
        if (hit.collider != null)
        {
            ground = hit.collider.gameObject.GetComponent<Tilemap>();
            Debug.Log(ground.cellSize.x + " " + ground.cellSize.y);
            Vector3Int cellPosition = ground.LocalToCell(hit.point);
            /* if (tileManage.IsCraftable(cellPosition))
                 DrawRectangle(new Vector3(cellPosition.x + ground.tileAnchor.x, cellPosition.y + ground.tileAnchor.y, 0), Color.green);
             else
             {
                 DrawRectangle(new Vector3(cellPosition.x + ground.tileAnchor.x, cellPosition.y + ground.tileAnchor.y, 0), Color.red);
             }
 */
            Draw_nSizeRectangle(cellPosition, 2);
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log(cellPosition);
                if (tileManage.IsCraftable(cellPosition))
                {
                    Debug.Log(tileManage.GetPlaceIdx(cellPosition));
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
        /*if(!rectangle.gameObject.activeSelf)rectangle.gameObject.SetActive(true);
        rectangle.transform.position = pos;
        Color tmpColor = color;
        tmpColor.a = 0.5f;
        rectangle.transform.GetComponent<SpriteRenderer>().color = tmpColor;*/
    }

    void Draw_nSizeRectangle(Vector3 pos, int n)
    {
        int idx = tileManage.GetPlaceIdx(pos);
        Vector3 tmpPos = pos;
        int recIdx = 0;
        for(int i = idx, cnt = 0; cnt < n; i += size, cnt++)
        {
            int mul = 0;
            for (int j = i; j < i+n; j++)
            {
                tmpPos = tmpPos + new Vector3(tmpPos.x + +(ground.cellSize.x * mul), tmpPos.y + (ground.cellSize.y * mul),0);
                if (!rectangles[recIdx].gameObject.activeSelf) rectangles[recIdx].gameObject.SetActive(true);
                rectangles[recIdx].transform.position =
                    new Vector3(tmpPos.x + ground.tileAnchor.x, tmpPos.y + ground.tileAnchor.y, 0);
                Color tmpColor = Color.white;
                if (tileManage.IsCraftable(tmpPos))
                {
                    tmpColor = Color.green;
                    tmpColor.a = 0.5f;
                }
                else
                {
                    tmpColor = Color.red;
                    tmpColor.a = 0.5f;
                }
                rectangles[recIdx].transform.GetComponent<SpriteRenderer>().color = tmpColor;
                recIdx++;
                mul++;
            }
            
        }
    }

    void StopDrawingRectangle()
    {
        for(int i = 0; i < rectangles.Length; i++)
        {
            if (rectangles[i].gameObject.activeSelf) rectangles[i].gameObject.SetActive(false);
            else break;
        }
    }

}
