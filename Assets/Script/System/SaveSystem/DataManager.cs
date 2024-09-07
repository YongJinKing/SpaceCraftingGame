using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using Spine;
using System;
using System.Linq;

[Serializable]
public class DataManager : BaseSaveSystem
{
    #region variable
    public string savePath = "PlayerData_";
    public int nowSlot;
    public LayerMask playerLayerMask ;
    /// <summary>
    /// 플레이어 데이터 저장 배열
    /// </summary>
    public PlayerDataStruct[] pd = new PlayerDataStruct[1];
    public Player NowPlayer;

    public Player LoadedPlayer;

    public static DataManager Instance;
    public string tempSavePath;
    #endregion

    #region start
    private void Awake()
    {
        Instance = this;
    }
    protected override void Start()
    {
        base.Start();
        tempSavePath = Path.Combine(filePath, savePath + nowSlot.ToString() + ".json");

        //LoadJson(tempSavePath);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            //LoadJson("PlayerData" + nowSlot.ToString() + ".json");
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            for (int i = 0; i < pd.Length; ++i)
            {
                //Debug.Log($"{pd[i].Index}, {pd[i].MaxHP}, {pd[i].moveSpeed}, {pd[i].ATK} {pd[i].ATKSpeed}");
            }
        }
    }
    #endregion

    #region player_data_save
    public override void Save()
    {
        //1. 유닛 찾기
        //2. 유닛이 플레이어 레이어 인지 확인
        //3. 플레이어의 데이터를 가져와 pd[0]에 저장
        //4. 직렬화
        //5. 파일로 저장
        //6. 디버그 로그

        string currentTime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm");
        Debug.Log(currentTime);

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
                    pd[0].HP = tempPl.GetRawStat(EStat.HP);
                    pd[0].MoveSpeed = tempPl.GetRawStat(EStat.MoveSpeed);
                    pd[0].ATK = tempPl.GetRawStat(EStat.ATK);
                    pd[0].ATKSpeed = tempPl.GetRawStat(EStat.ATKSpeed);
                    pd[0].Priority = tempPl.GetRawStat(EStat.Priority);
                    pd[0].saveTime = currentTime;

                }
            }
        }

        //4.
        //직렬화 코드
        var json = JsonConvert.SerializeObject(pd, Formatting.Indented);


        //5.
        File.WriteAllText(tempSavePath, json);

        //6.
        Debug.Log($"Data saved to: {savePath}");
        Debug.Log($"Saved JSON: {json}");
    }
    #endregion

    #region player_data_load
    public PlayerDataStruct LoadJson(string path) // LoadJson(string path)
    {
        Debug.Log("LoadJson called");

        var jsonPath = path;
        if (File.Exists(jsonPath))
        {
            string JsonString = File.ReadAllText(jsonPath);
            pd = JsonConvert.DeserializeObject<PlayerDataStruct[]>(JsonString);

            Dictionary<int, PlayerDataStruct> playerdataDic = new Dictionary<int, PlayerDataStruct>();
            playerdataDic = pd.ToDictionary(x => x.Index);

            return playerdataDic[0];
            /*LoadedPlayer[EStat.MaxHP] = playerdataDic[0].MaxHP;
            LoadedPlayer[EStat.HP] = playerdataDic[0].HP;
            LoadedPlayer[EStat.MoveSpeed] = playerdataDic[0].MoveSpeed;
            LoadedPlayer[EStat.ATK] = playerdataDic[0].ATK;
            LoadedPlayer[EStat.ATKSpeed] = playerdataDic[0].ATKSpeed;
            LoadedPlayer[EStat.Priority] = playerdataDic[0].Priority;*/
        }
        else
        {
            Debug.Log("없음");
            PlayerDataStruct pd = new PlayerDataStruct();

            pd.Index = -1;
            return pd;
        }
        //NowPlayer = JsonUtility.FromJson<Player>(data);
    }
    #endregion

    #region
    public void DataClear()
    {
        nowSlot = -1;
        NowPlayer = new Player();
    }
    #endregion
}
