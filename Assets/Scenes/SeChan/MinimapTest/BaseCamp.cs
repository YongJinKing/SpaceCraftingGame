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
        //Debug.Log("������ �Ÿ���" + dist);
        if(dist < NavDistance)
        {
            //���� dist���� �ȿ� ���� �� - �÷��̾� �þ߿� ���� ���� ��
            this.GetComponent<SpriteRenderer>().enabled = false;    
        }
        else
        {
            this.GetComponent<SpriteRenderer>().enabled = true;
        }

      
    }

 
       
    
}
