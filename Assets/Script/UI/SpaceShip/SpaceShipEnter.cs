using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipEnter : MonoBehaviour
{
    public LayerMask spaceshipLayer;
    public GameObject spaceShipCanvas;
    public GameObject invenCanvas;
    public GameObject buildingCanvas;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (invenCanvas.activeSelf || buildingCanvas.activeSelf) return;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, spaceshipLayer);

            if (hit.collider != null)
            {
                if (((1 << hit.collider.gameObject.layer) & spaceshipLayer) != 0)
                {
                    spaceShipCanvas.SetActive(true);
                }
            }
        }
    }
}
