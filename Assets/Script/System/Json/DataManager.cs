using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using Spine;
using System;

/*[Serializable]
public class PlayerDataManager:MonoBehaviour
{
    public int index;
    public int ModelPreFabIndex;
    public float MaxHP;
    public float Priority;
    public float moveSpeed;
    public float ATK;
    public float ATKSpeed;

}*/ //���� ����
[System.Serializable]
public class UnitData : MonoBehaviour
{
    public int index;
    public int ModelPreFabIndex;
    public float MaxHP;
    public float Priority;
    public float moveSpeed;
    public float ATK;
    public float ATKSpeed;

    public UnitData(Unit unit)
    {
        this.moveSpeed = unit.GetRawStat(EStat.MoveSpeed);
        this.ATK = unit.GetRawStat(EStat.ATK);
        this.ATKSpeed = unit.GetRawStat(EStat.ATKSpeed);
        this.MaxHP = unit.GetRawStat(EStat.MaxHP);
    }
}

[Serializable]
public class PlayerDataMansgerInfo
{
    /*public List<PlayerDataManager> playerDataManagers;

    public PlayerDataMansgerInfo(List<PlayerDataManager> playerDataManagers)
    {
        this.playerDataManagers = playerDataManagers;
    }*/ // ��������

    public List<UnitData> units;

    public PlayerDataMansgerInfo(List<Unit> units)
    {
        this.units = new List<UnitData>();
        foreach (var unit in units)
        {
            this.units.Add(new UnitData(unit));
        }
    }
}

public class PlayerDataManagerSaveSystem : Singleton<PlayerDataManagerSaveSystem>
{
    PlayerDataMansgerInfo playerDataMansgerInfo;
    string savePath;

    List<Unit> unitsList = new List<Unit>();
    public void SavePlayerInfo()
    {
        Debug.Log("SavePlayerInfo called");

        Unit[] units = FindObjectsOfType<Unit>();
        Debug.Log($"Found {units.Length} units");

        unitsList.Clear();
        unitsList.AddRange(units);

        playerDataMansgerInfo = new PlayerDataMansgerInfo(unitsList);

        //����ȭ �ڵ�
        var json = JsonConvert.SerializeObject(playerDataMansgerInfo, Formatting.Indented);

        savePath = "SaveTestJson.json";
        File.WriteAllText(savePath, json);

        Debug.Log($"Data saved to: {savePath}");
        Debug.Log($"Saved JSON: {json}");
    }

    private string LoadJson()
    {
        Debug.Log("LoadJson called");

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
        Debug.Log("LoadPlayerInfo called");

        var jsonString = LoadJson();
        if (string.IsNullOrEmpty(jsonString))
        {
            Debug.LogError("JSON �����Ͱ� ��� �ֽ��ϴ�.");
            return null;
        }

        //PlayerDataMansgerInfo playerDataManagersList = JsonUtility.FromJson<PlayerDataMansgerInfo>(jsonString);
        PlayerDataMansgerInfo playerDataManagersList = JsonConvert.DeserializeObject<PlayerDataMansgerInfo>(jsonString);
        if (playerDataManagersList == null || playerDataManagersList.units == null)
        {
            Debug.LogError("JSON �����͸� ������ȭ�ϴµ� �����߽��ϴ�.");
        }

        Debug.Log($"Loaded JSON: {jsonString}");

        return playerDataManagersList;

    }
}
