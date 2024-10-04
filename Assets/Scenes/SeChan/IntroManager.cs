using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        /*if (Input.GetKeyDown(KeyCode.PageDown))
        {
            StopAllCoroutines();
            SceneManager.LoadScene("StageSelect"); // 스테이지 씬 이동
        }*/
    }

    IEnumerator fadeOut()
    {
        IntroBGM();
        yield return new WaitForSeconds(34.0f);
        Debug.Log("페이드아웃");
        float f = 0;
        while (f < 1)
        {
            f += 0.01f;
            Color ColorAlhpa = MyRenderer.GetComponent<Image>().color;
            ColorAlhpa.a = f;
            MyRenderer.GetComponent<Image>().color = ColorAlhpa;
            yield return new WaitForSeconds(0.02f);
        }
       
        SceneManager.LoadScene("StageSelect"); // 스테이지 씬 이동
    }
    public void IntroBGM()
    {
        SoundManager.Instance.PlayBGM(SoundManager.Instance.UISound.IntroBGM);
    }
}
