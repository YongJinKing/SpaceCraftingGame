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
    public Transform rectangle;
    TileManager tileManage;
    // Start is called before the first frame update
    void Start()
    {
        tileManage = FindObjectOfType<TileManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, layerMask);
        if (hit.collider != null)
        {
            ground = hit.collider.gameObject.GetComponent<Tilemap>();
            Vector3Int cellPosition = ground.LocalToCell(hit.point);
            DrawRectangle(new Vector3(cellPosition.x + ground.tileAnchor.x, cellPosition.y + ground.tileAnchor.y, 0));

            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log(cellPosition);
                if (tileManage.IsCraftable(cellPosition))
                {
                    Instantiate(turret, new Vector3(cellPosition.x + ground.tileAnchor.x, cellPosition.y+ground.tileAnchor.y, 0), Quaternion.identity, null);
                    RemovePlaceEvent?.Invoke(cellPosition);
                }
            }
        }
        else
        {
            StopDrawingRectangle();
        }

    }

    void DrawRectangle(Vector3 pos)
    {
        if(!rectangle.gameObject.activeSelf)rectangle.gameObject.SetActive(true);
        rectangle.transform.position = pos;
    }

    void StopDrawingRectangle()
    {
        rectangle.gameObject.SetActive(false);
    }

}
