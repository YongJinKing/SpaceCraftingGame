using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureInfoViewer : MonoBehaviour
{
    // 
    public LayerMask layerMask;
    public LayerMask structureMask;
    public LayerMask resourceMask;
    public Structure viewStructure;
    NaturalResources naturalResource;
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            HandleRightClick(structureMask);
        }
    }

    private void HandleRightClick(LayerMask layerMask)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, layerMask);
        if (hit.collider != null)
        {
            viewStructure = hit.collider.GetComponent<Structure>();
            if (viewStructure != null)
            {
                Debug.Log(hit.collider.name);
                Debug.Log(viewStructure.mComponentName);
                Debug.Log(viewStructure[EStat.HP]);
                var factoryProduce = viewStructure.GetComponent<FactoryBuilding>();
                if (factoryProduce != null)
                {
                    factoryProduce.FactoryClickEvent();
                }
            }
        }
    }
}
