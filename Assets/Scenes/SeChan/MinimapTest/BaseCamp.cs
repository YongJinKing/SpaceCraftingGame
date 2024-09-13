using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class BaseCamp : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject OrgPos;
    public GameObject Player;
    public GameObject Arrows;
    
    [SerializeField] public float NavDistance = 5.0f;
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
     
        Vector3 directionToTarget = OrgPos.gameObject.transform.position - Player.gameObject.transform.position;
        float angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;

        Arrows.transform.rotation = Quaternion.Euler(0, 0, -90 + angle);
        float dist = Vector3.Distance(OrgPos.transform.position, Player.transform.position);
        //Debug.Log("집까지 거리는" + dist);
        if(dist < NavDistance)
        {
            //집이 dist범위 안에 있을 때 - 플레이어 시야에 집이 보일 때
            this.GetComponent<SpriteRenderer>().enabled = false;    
        }
        else
        {
            this.GetComponent<SpriteRenderer>().enabled = true;
        }

      
    }

 
       
    
}
