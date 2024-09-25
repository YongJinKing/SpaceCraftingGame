using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    [SerializeField] Button goBackToStartScene;

    void OnEnable()
    {
        Time.timeScale = 0f;
        Debug.Log(goBackToStartScene);
        goBackToStartScene.onClick.AddListener(GoBackToStartScene);
    }

    void GoBackToStartScene()
    {
        SceneManager.LoadSceneAsync(0);
        Time.timeScale = 1f;
    }
}
