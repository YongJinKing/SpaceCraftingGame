using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPlacer : MonoBehaviour
{
    public GameObject resource1;
    public LayerMask Mineral;
    public LayerMask Gas;

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (CanPlaceObject(mousePosition))
            {
                Instantiate(resource1, mousePosition, Quaternion.identity);
            }
        }
    }

    bool CanPlaceObject(Vector2 position)
    {
        Collider2D hitCollider = Physics2D.OverlapPoint(position, Gas, Mineral);
        return hitCollider == null;
    }
}
