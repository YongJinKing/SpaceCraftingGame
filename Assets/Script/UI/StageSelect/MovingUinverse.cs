using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class MovingUinverse : MonoBehaviour
{
    public Sprite[] sprites;
    public Image Background;
    // Start is called before the first frame update
    void Start()
    {
       
        StartCoroutine(SpaceWorks());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator SpaceWorks()
    {
        while (true)
        {
            
            for (int i = 0; i < sprites.Length; i++)
            {
                Background.GetComponent<Image>().sprite = sprites[i];
                //Debug.Log(i);
                yield return new WaitForSeconds(0.1f);
            }
        }
        //yield return null;
    }
}
