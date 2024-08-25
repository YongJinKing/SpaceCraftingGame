using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapTest : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Monster1;
    public GameObject Monster2;
    public GameObject Monster3;
    public GameObject Monster4;
    public GameObject Monster5;
    public GameObject Turret1;
    public GameObject Turret2;
    public GameObject Turret3;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F2)) 
        {
            StartCoroutine(testing());
        
        }
        if (Input.GetKeyDown(KeyCode.F3))
        {
            StartCoroutine(testingTurret());

        }
    }

    public IEnumerator testing()
    {
        Monster1.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        Monster2.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        Monster3.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        Monster4.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        Monster5.SetActive(true);
        yield return null;
    }
    public IEnumerator testingTurret()
    {
        Turret1.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        Turret2.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        Turret3.SetActive(true);       
        yield return null;
    }
}
