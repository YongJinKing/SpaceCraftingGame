using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class StageSelectManager : MonoBehaviour
{
    public string savePos;

    void Start()
    {
        savePos = Path.Combine(Application.persistentDataPath, "PlayerPos.json");
        // 게임 시작 시 파일 존재 여부를 확인하고 해당 씬으로 이동
        if (File.Exists(savePos))
        {
            //파일이 존재하면 메인 씬으로 이동
            LoadMainScene();
        }
        else
        {
            // 파일이 존재하지 않으면 랜딩 씬으로 이동 (최초)
            LoadLandingScene();
        }
    }

    void LoadMainScene()
    {
        SceneManager.LoadScene("MainStage1");
    }

    void LoadLandingScene()
    {
        SceneManager.LoadScene("StageLanding");
    }
}
