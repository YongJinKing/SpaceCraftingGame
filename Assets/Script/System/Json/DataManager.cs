using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using Spine;
using System;

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

    public string playerdataString;
    public UnitData(Unit unit)
    {
        this.moveSpeed = unit.GetRawStat(EStat.MoveSpeed);
        this.ATK = unit.GetRawStat(EStat.ATK);
        this.ATKSpeed = unit.GetRawStat(EStat.ATKSpeed);
        this.MaxHP = unit.GetRawStat(EStat.MaxHP);
    }

    public void PlayerDataSerialize()
    {
        playerdataString = $"{MaxHP},{moveSpeed},{ATK},{ATKSpeed}";
    }

    public void PlayerDataDeserialize()
    {
        var state = playerdataString.Split(',');
    }
}

[Serializable]
public class PlayerDataMansgerInfo
{
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
    public LayerMask playerLayerMask;
    public LayerMask EnemyLayerMask;

    private void Awake()
    {
        
    }

    void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            SavePlayerInfo();
        }
    }

    List<Unit> unitsList = new List<Unit>();
    public void SavePlayerInfo()
    {
        Debug.Log("SavePlayerInfo called");

        Unit[] units = FindObjectsOfType<Unit>();
        Dictionary<int, Player> keyValuePairs = new Dictionary<int, Player>();

        List<UnitData> playerdataList = new List<UnitData>();
        foreach (var player in keyValuePairs)
        {
            UnitData unitData = gameObject.AddComponent<UnitData>();
            unitData.PlayerDataDeserialize();
            if (player.Value.gameObject == null)
            {
                if ((playerLayerMask & 1 << player.Value.layer)!=0)
                {
                    unitData.MaxHP = player.Value.gameObject.GetComponent<Player>().MaxHP;
                    unitData.moveSpeed = player.Value.gameObject.GetComponent<Player>().moveSpeed;
                    unitData.ATK = player.Value.gameObject.GetComponent<Player>().ATK;
                    unitData.ATKSpeed = player.Value.gameObject.GetComponent<Player>().ATKSpeed;
                }
                else if((EnemyLayerMask & 1 << player.Value.layer) !=0)
                {
                    unitData.MaxHP = player.Value.gameObject.GetComponent<Monster>().MaxHP;
                    unitData.moveSpeed = player.Value.gameObject.GetComponent<Monster>().moveSpeed;
                    unitData.ATK = player.Value.gameObject.GetComponent<Monster>().ATK;
                    unitData.ATKSpeed = player.Value.gameObject.GetComponent<Monster>().ATKSpeed;
                }
            }
            else
            {
                unitData.MaxHP = 0;
                unitData.moveSpeed = 0;
                unitData.ATK = 0;
                unitData.ATKSpeed = 0;
            }
            playerdataList.Add(unitData);
        }

        unitsList.Clear();
        unitsList.AddRange(units);

        playerDataMansgerInfo = new PlayerDataMansgerInfo(unitsList);

        //직렬화 코드
        var json = JsonConvert.SerializeObject(playerDataMansgerInfo, Formatting.Indented);

        savePath = "PlayerDataStruct.json";
        File.WriteAllText(savePath, json);

        Debug.Log($"Data saved to: {savePath}");
        Debug.Log($"Saved JSON: {json}");
    }

    private string LoadJson()
    {
        Debug.Log("LoadJson called");

        var jsonPath = "PlayerDataStruct.json";
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

        foreach (var item in playerDataManagersList.units)
        {
            item.PlayerDataDeserialize();
        }

        return playerDataManagersList;

    }
}
