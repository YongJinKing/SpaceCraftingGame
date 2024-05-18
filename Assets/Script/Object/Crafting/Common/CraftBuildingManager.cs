using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CraftBuildingManager : MonoBehaviour
{
    public Tilemap ground;
    public Camera cam;
    public LayerMask layerMask;
    public Transform Tower;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Input.mousePosition;
            // 어떤게 클릭 되었는가? -> layermask비교해서 맞으면 아래 진행
            mousePosition = cam.ScreenToWorldPoint(mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero, Mathf.Infinity, layerMask);
            if (hit.collider != null)
            {
                Debug.Log("hit");
                var worldPos = cam.ScreenToWorldPoint(Input.mousePosition);
                Vector3Int tilemapPos = ground.WorldToCell(worldPos);
                Debug.Log("tile pos : " + tilemapPos);
                Vector3 cellToworldPos = ground.CellToWorld(tilemapPos);
                Debug.Log("cellTowrold pos : " + cellToworldPos);

                Instantiate(Tower, new Vector3(cellToworldPos.x + 0.5f, cellToworldPos.y + 0.5f, 0), Quaternion.identity);
            }
            else
            {
                Debug.Log("No hit");
            }
        }*/
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition = cam.ScreenToWorldPoint(mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero, Mathf.Infinity, layerMask);
            if (hit.collider != null)
            {
                Debug.Log("hit");

                var worldPos = mousePosition;
                Vector3Int tilemapPos = ground.WorldToCell(worldPos);
                Debug.Log("tile pos : " + tilemapPos);

                Vector3 cellToworldPos = ground.CellToWorld(tilemapPos);
                Debug.Log("cellToWorld pos : " + cellToworldPos);

                Instantiate(Tower, new Vector3(cellToworldPos.x + 0.5f, cellToworldPos.y + 0.5f, 0), Quaternion.identity);
            }
            else
            {
                Debug.Log("No hit");
            }
        }
    }

}
