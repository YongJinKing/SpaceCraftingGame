using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingInteraction : MonoBehaviour
{
    public LayerMask structureMask;
    public Structure targetStructure;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //HandleLeftClick(structureMask);
        }
        if (Input.GetMouseButtonDown(1))
        {
            HandleRightClick(structureMask);
        }
    }

    private void HandleLeftClick(LayerMask layerMask) // 테스트용
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, layerMask);
        if (hit.collider != null)
        {
            targetStructure = hit.collider.GetComponent<Structure>();
            if (targetStructure != null)
            {
                var factoryProduce = targetStructure.GetComponent<FactoryBuilding>();
                if (factoryProduce != null)
                {
                    factoryProduce.TakeDamage(1);
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
            targetStructure = hit.collider.GetComponent<Structure>();
            if (targetStructure != null)
            {
                var factoryProduce = targetStructure.GetComponent<FactoryBuilding>();
                if (factoryProduce != null)
                {
                    factoryProduce.FactoryClickEvent();
                }
            }
        }
    }
}
