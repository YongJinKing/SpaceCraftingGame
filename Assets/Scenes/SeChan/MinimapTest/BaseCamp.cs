using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BaseCamp : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject OrgPos;
    public Camera minimapCamera;
    void Start()
    {
        this.transform.position = OrgPos.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = OrgPos.transform.position;
        Vector3 viewportPosition = minimapCamera.WorldToViewportPoint(transform.position);
        if (viewportPosition.x < 0 || viewportPosition.x > 1 || viewportPosition.y < 0 || viewportPosition.y > 1)
        {
            // 화면을 벗어날 경우, 화면 가장자리에 위치시키기
            this.GetComponent<SpriteRenderer>().enabled = true;
            viewportPosition.x = Mathf.Clamp(viewportPosition.x, 0.03f, 0.97f);
            viewportPosition.y = Mathf.Clamp(viewportPosition.y, 0.03f, 0.97f);
           
        }
        else
        {
            this.GetComponent<SpriteRenderer>().enabled = false;
        }
    
        Vector3 worldPosition = minimapCamera.ViewportToWorldPoint(viewportPosition);
        this.transform.position = worldPosition;


      
    }

 
       
    
}
