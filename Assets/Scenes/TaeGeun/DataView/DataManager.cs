using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using Spine;
using System;
using System.Linq;

[Serializable]
public class DataManager : Singleton<DataManager>
{
    public string savePath;
    public int nowSlot;
    public LayerMask playerLayerMask;
    public LayerMask EnemyLayerMask;
    /// <summary>
    /// 플레이어 데이터 저장 배열
    /// </summary>
    public PlayerDataStruct[] pd = new PlayerDataStruct[1];
    public Player NowPlayer = new Player();
    private void Awake()
    {
        Initialize();
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

        if (Input.GetKeyDown(KeyCode.P))
        {
            //LoadJson();
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            for (int i = 0; i < pd.Length; ++i)
            {
                Debug.Log($"{pd[i].Index}, {pd[i].MaxHP}, {pd[i].Priority}, {pd[i].moveSpeed}, {pd[i].ATK} {pd[i].ATKSpeed}");
            }
        }
    }

    public void SavePlayerInfo()
    {
        //1. 유닛 찾기
        //2. 유닛이 플레이어 레이어 인지 확인
        //3. 플레이어의 데이터를 가져와 pd[0]에 저장
        //4. 직렬화
        //5. 파일로 저장
        //6. 디버그 로그

        //1.
        Unit[] units = FindObjectsOfType<Unit>();


        foreach (var player in units)
        {
            if (player.gameObject != null)
            {
                //2.
                if ((playerLayerMask & 1 << player.gameObject.layer) != 0)
                {
                    //3.
                    Player tempPl = player.GetComponent<Player>();

                    pd[0].MaxHP = tempPl.GetRawStat(EStat.MaxHP);
                    pd[0].moveSpeed = tempPl.GetRawStat(EStat.MoveSpeed);
                    pd[0].ATK = tempPl.GetRawStat(EStat.ATK);
                    pd[0].ATKSpeed = tempPl.GetRawStat(EStat.ATKSpeed);
                    pd[0].Priority = tempPl.GetRawStat(EStat.Priority);
                }
            }
        }

        //4.
        //직렬화 코드
        var json = JsonConvert.SerializeObject(pd, Formatting.Indented);


        //5.
        savePath = "PlayerData" + nowSlot.ToString() + ".json";
        string data = JsonUtility.ToJson(NowPlayer);
        File.WriteAllText(savePath,json);
        File.WriteAllText(nowSlot.ToString(), data);

        //6.
        Debug.Log($"Data saved to: {savePath}");
        Debug.Log($"Saved JSON: {json}");
    }

    public void LoadJson(string path) // LoadJson(string path)
    {
        Debug.Log("LoadJson called");

        var jsonPath = path;
        if (File.Exists(jsonPath))
        {
            string JsonString = File.ReadAllText(jsonPath);
            pd = JsonConvert.DeserializeObject<PlayerDataStruct[]>(JsonString);

            Dictionary<int, PlayerDataStruct> playerdataDic = new Dictionary<int, PlayerDataStruct>();
            playerdataDic = pd.ToDictionary(x => x.Index);
        }
        else
        {
            Debug.Log("없음");
        }
        string data = File.ReadAllText(nowSlot.ToString());
        //NowPlayer = JsonUtility.FromJson<Player>(data);
    }

    public void DataClear()
    {
        nowSlot = -1;
        NowPlayer = new Player();
    }
}
