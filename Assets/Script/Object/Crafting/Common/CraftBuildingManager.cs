using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CraftBuildingManager : MonoBehaviour
{
    public Tilemap ground;
    public LayerMask layerMask;
    public Transform Tower;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        // 어떤게 클릭 되었는가? -> layermask비교해서 맞으면 아래 진행
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        Debug.Log("mouse Position : " + mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector3.forward, layerMask);
        if (hit.collider != null)
        {
            Debug.Log("hit");
            var worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int tilemapPos = ground.WorldToCell(worldPos);
            Debug.Log("tile pos : " + tilemapPos);
            Vector3 cellToworldPos = ground.CellToWorld(tilemapPos);
            Debug.Log("cellTowrold pos : " + cellToworldPos);

            if (Input.GetMouseButtonDown(0))
                Instantiate(Tower, new Vector3(cellToworldPos.x + 0.5f, cellToworldPos.y + 0.5f, 0), Quaternion.identity);
        }
    }

}
