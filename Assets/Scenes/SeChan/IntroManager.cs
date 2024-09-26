using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroManager : MonoBehaviour
{

    public GameObject MyRenderer;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(fadeOut());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator fadeOut()
    {
        yield return new WaitForSeconds(34.0f);
        Debug.Log("ÆäÀÌµå¾Æ¿ô");
        float f = 0;
        while (f < 1)
        {
            f += 0.01f;
            Color ColorAlhpa = MyRenderer.GetComponent<Image>().color;
            ColorAlhpa.a = f;
            MyRenderer.GetComponent<Image>().color = ColorAlhpa;
            yield return new WaitForSeconds(0.02f);
        }
    }
}
