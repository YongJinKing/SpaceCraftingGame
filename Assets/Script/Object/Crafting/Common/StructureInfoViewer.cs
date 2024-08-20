using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureInfoViewer : MonoBehaviour
{
    public LayerMask layerMask;
    public LayerMask structureMask;
    public LayerMask resourceMask;
    public Structure viewStructure;
    NaturalResources naturalResource;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HandleClick(layerMask, structureMask, resourceMask);
        }

        if (Input.GetMouseButtonDown(1))
        {
            HandleRightClick(structureMask);
        }
    }

    private void HandleClick(LayerMask layerMask, LayerMask structureMask, LayerMask resourceMask)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, layerMask);
        if (hit.collider != null)
        {
            Debug.Log(hit.collider.gameObject.name);
            Debug.Log(hit.collider.gameObject.layer);
            if (((1 << hit.collider.gameObject.layer) & structureMask) != 0)
            {
                viewStructure = hit.collider.GetComponent<Structure>();
                if (viewStructure != null)
                {
                    viewStructure.TakeDamage(1000f);
                }
            }
            else if (((1 << hit.collider.gameObject.layer) & resourceMask) != 0)
            {
                Debug.Log("자원 히트");
                naturalResource = hit.collider.GetComponent<NaturalResources>();
                if (naturalResource != null)
                {
                    naturalResource.TakeDamage(1f);
                }
            }
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
