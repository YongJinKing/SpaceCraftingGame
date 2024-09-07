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
        //저장된 위치가 있으면 불러와서 플레이어 위치 설정
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
        File.WriteAllText(savePos, json);
        Debug.Log("현재 위치 저장: " + savePos);
    }

    void LoadPosition()
    {
        try
        {
            string json = File.ReadAllText(savePos);
            Vector3 savedPos = JsonUtility.FromJson<Vector3>(json);
            player.transform.position = savedPos;
            Debug.Log("위치 로드 완료: " + savedPos);
        }
        catch (Exception e)
        {
            Debug.LogError("위치 로드 실패" + e.Message);
        }
    }
}
