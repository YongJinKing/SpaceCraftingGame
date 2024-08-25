using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LoadingSceneController : MonoBehaviour
{
    public Slider progressbar;
    public TextMeshProUGUI loadtext;
    private void Start()
    {
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        yield return null;
        AsyncOperation op = SceneManager.LoadSceneAsync("PlayerDataManager");
        op.allowSceneActivation = false;

        while(!op.isDone)
        {
            yield return null;

            if(progressbar.value < 0.9f)
            {
                progressbar.value = Mathf.MoveTowards(progressbar.value, 0.9f, Time.deltaTime);
            }else if(op.progress >= 0.9f)
            {
                progressbar.value = Mathf.MoveTowards(progressbar.value, 1f, Time.deltaTime);
            }

            if(progressbar.value >= 1f)
            {
                loadtext.text = "스페이스바를 눌러 시작하세요!";
            }

            if (Input.GetKeyDown(KeyCode.Space) && progressbar.value >= 1f && op.progress >= 0.9f)
            {
                op.allowSceneActivation = true;
            }
        }
    }
}
