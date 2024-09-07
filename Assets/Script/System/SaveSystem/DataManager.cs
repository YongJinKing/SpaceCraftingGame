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
    public string savePath = "PlayerData";
    public int nowSlot;
    public LayerMask playerLayerMask ;
    /// <summary>
    /// �÷��̾� ������ ���� �迭
    /// </summary>
    public PlayerDataStruct[] pd = new PlayerDataStruct[1];
    public Player NowPlayer;

    public static DataManager Instance;
    string tempSavePath;
    #endregion

    #region start
    private void Awake()
    {
        Instance = this;
    }
    protected override void Start()
    {
        base.Start();
        tempSavePath = filePath + savePath + nowSlot.ToString() + ".json";

        MakeDir(tempSavePath);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            LoadJson("PlayerData" + nowSlot.ToString() + ".json");
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            for (int i = 0; i < pd.Length; ++i)
            {
                Debug.Log($"{pd[i].Index}, {pd[i].MaxHP}, {pd[i].moveSpeed}, {pd[i].ATK} {pd[i].ATKSpeed}");
            }
        }
    }
    #endregion

    #region player_data_save
    public override void Save()
    {
        //1. ���� ã��
        //2. ������ �÷��̾� ���̾� ���� Ȯ��
        //3. �÷��̾��� �����͸� ������ pd[0]�� ����
        //4. ����ȭ
        //5. ���Ϸ� ����
        //6. ����� �α�

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
                    pd[0].moveSpeed = tempPl.GetRawStat(EStat.MoveSpeed);
                    pd[0].ATK = tempPl.GetRawStat(EStat.ATK);
                    pd[0].ATKSpeed = tempPl.GetRawStat(EStat.ATKSpeed);
                    pd[0].Priority = tempPl.GetRawStat(EStat.Priority);
                    pd[0].saveTime = currentTime;

                }
            }
        }

        //4.
        //����ȭ �ڵ�
        var json = JsonConvert.SerializeObject(pd, Formatting.Indented);


        //5.
        File.WriteAllText(tempSavePath, json);

        //6.
        Debug.Log($"Data saved to: {savePath}");
        Debug.Log($"Saved JSON: {json}");
    }
    #endregion

    #region player_data_load
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
            Debug.Log("����");
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
