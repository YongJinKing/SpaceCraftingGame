using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureInfoViewer : MonoBehaviour
{
    public LayerMask layerMask;
    public Structure viewStructure;

    [SerializeField] float mouseOnTime = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, layerMask);
        if (hit.collider != null)
        {
            mouseOnTime += Time.deltaTime;
            if(mouseOnTime >= 1.5f)
            {
                viewStructure = hit.collider.GetComponent<Structure>();

                if (viewStructure != null)
                {
                    Debug.Log(hit.collider.name);
                    Debug.Log(viewStructure.mComponentName);

                }
            }
        }
        else
        {
            mouseOnTime = 0f;
        }
    }
}
