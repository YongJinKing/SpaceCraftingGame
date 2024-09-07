using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class PosManager : MonoBehaviour
{
    public string savePos;
    public GameObject player;

    void Start()
    {
        savePos = "PlayerPos" + DataManager.Instance.nowSlot;
        //����� ��ġ�� ������ �ҷ��ͼ� �÷��̾� ��ġ ����
        if (File.Exists(savePos))
        {
            LoadPosition();
        }
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.N))
        {
            SavePos();
        }
    }

    public void SavePos()
    {
        Vector3 playerPosition = player.transform.position;
        string json = JsonUtility.ToJson(playerPosition);
        File.WriteAllText(savePos + DataManager.Instance.nowSlot, json);
        Debug.Log("���� ��ġ ����: " + savePos);
    }

    void LoadPosition()
    {
        try
        {
            string json = File.ReadAllText(savePos);
            Vector3 savedPos = JsonUtility.FromJson<Vector3>(json);
            player.transform.position = savedPos;
            Debug.Log("��ġ �ε� �Ϸ�: " + savedPos);
        }
        catch (Exception e)
        {
            Debug.LogError("��ġ �ε� ����" + e.Message);
        }
    }
}
