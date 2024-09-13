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

        savePos = Path.Combine(filePath, "PlayerPos_" + DataManager.Instance.nowSlot + ".json");
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
        if (DataManager.Instance.nowSlot == -1) return;
        base.Save();
        Vector3 playerPosition = player.transform.position;
        string json = JsonUtility.ToJson(playerPosition);
        File.WriteAllText(savePos, json);
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
