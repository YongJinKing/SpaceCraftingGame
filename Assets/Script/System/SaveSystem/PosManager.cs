using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class PosManager : BaseSaveSystem
{
    public string savePos;
    public Player player;

    protected override void Start()
    {
        base.Start();
        savePos = filePath + "PlayerPos" + DataManager.Instance.nowSlot + ".json";
        MakeDir(savePos);
        player = FindObjectOfType<Player>();
        //����� ��ġ�� ������ �ҷ��ͼ� �÷��̾� ��ġ ����
        if (File.Exists(savePos))
        {
            LoadPosition();
        }
    }

    public void Update()
    {
        
    }

    public override void Save()
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
