using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using Spine;
using System;

[Serializable]
public class PlayerDataManager:MonoBehaviour
{
    public int index;
    public int HP;
    public float moveSpeed;
    public float ATK;
    public float ATKSpeed;

}

[Serializable]
public class PlayerDataMansgerInfo
{
    public List<PlayerDataManager> playerDataManagers;

    public PlayerDataMansgerInfo(List<PlayerDataManager> playerDataManagers)
    {
        this.playerDataManagers = playerDataManagers;
    }
}

public class PlayerDataManagerSaveSystem : Singleton<PlayerDataManagerSaveSystem>
{
    PlayerDataMansgerInfo playerDataMansgerInfo;
    string savePath;
    public void SavePlayerInfo()
    {
        Unit unit = FindAnyObjectByType<Unit>();
        List<PlayerDataManager> playerDataManagersList = new List<PlayerDataManager>();

        playerDataMansgerInfo = new PlayerDataMansgerInfo(playerDataManagersList);
        var json = JsonConvert.SerializeObject(playerDataMansgerInfo);
        savePath = "SaveTestJson.json";
        File.WriteAllText(savePath, json);
    }

    string LoadJson()
    {
        var jsonPath = "SaveTestJson.json";
        if (File.Exists(jsonPath))
        {
            string JsonString = File.ReadAllText(jsonPath);
            Debug.Log("�б�");
            return JsonString;
        }
        else
        {
            Debug.Log("����");
            return string.Empty;
        }
    }

    public PlayerDataMansgerInfo LoadPlayerInfo()
    {
        var jsonString = LoadJson();
        if (string.IsNullOrEmpty(jsonString))
        {
            Debug.LogError("JSON �����Ͱ� ��� �ֽ��ϴ�.");
            return null;
        }

        PlayerDataMansgerInfo playerDataManagersList = JsonUtility.FromJson<PlayerDataMansgerInfo>(jsonString);
        if(playerDataManagersList == null || playerDataManagersList.playerDataManagers == null)
        {
            Debug.LogError("JSON �����͸� ������ȭ�ϴµ� �����߽��ϴ�.");
        }

        return playerDataManagersList;

    }
}
