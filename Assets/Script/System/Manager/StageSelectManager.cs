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
        savePos = "PlayerPos" + DataManager.Instance.nowSlot;
        // ���� ���� �� ���� ���� ���θ� Ȯ���ϰ� �ش� ������ �̵�
        if (File.Exists(savePos))
        {
            //������ �����ϸ� ���� ������ �̵�
            LoadMainScene();
            Debug.Log("���� �ε�");
        }
        else
        {
            // ������ �������� ������ ���� ������ �̵� (����)
            LoadLandingScene();
            Debug.Log("���� �ε�");
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
