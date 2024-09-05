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
        // ���� ���� �� ���� ���� ���θ� Ȯ���ϰ� �ش� ������ �̵�
        if (File.Exists(savePos))
        {
            //������ �����ϸ� ���� ������ �̵�
            LoadMainScene();
        }
        else
        {
            // ������ �������� ������ ���� ������ �̵� (����)
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
