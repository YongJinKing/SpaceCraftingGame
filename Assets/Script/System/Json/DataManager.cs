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

}*/ //삭제 예정
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
    }*/ // 삭제예정

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

        //직렬화 코드
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
            Debug.Log("읽기");
            return JsonString;
        }
        else
        {
            Debug.Log("없음");
            return string.Empty;
        }
    }

    public PlayerDataMansgerInfo LoadPlayerInfo()
    {
        Debug.Log("LoadPlayerInfo called");

        var jsonString = LoadJson();
        if (string.IsNullOrEmpty(jsonString))
        {
            Debug.LogError("JSON 데이터가 비어 있습니다.");
            return null;
        }

        //PlayerDataMansgerInfo playerDataManagersList = JsonUtility.FromJson<PlayerDataMansgerInfo>(jsonString);
        PlayerDataMansgerInfo playerDataManagersList = JsonConvert.DeserializeObject<PlayerDataMansgerInfo>(jsonString);
        if (playerDataManagersList == null || playerDataManagersList.units == null)
        {
            Debug.LogError("JSON 데이터를 역직렬화하는데 실패했습니다.");
        }

        Debug.Log($"Loaded JSON: {jsonString}");

        return playerDataManagersList;

    }
}
