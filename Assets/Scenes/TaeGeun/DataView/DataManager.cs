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
    #region variable
    public int layerNumber = 21;
    public int layerNumber2 = 22;
    public string savePath = "PlayerData";
    public string savePath2 = "WeaponLevelData";
    public int nowSlot;
    public LayerMask playerLayerMask ;
    public LayerMask EnemyLayerMask;
    /// <summary>
    /// �÷��̾� ������ ���� �迭
    /// </summary>
    public PlayerDataStruct[] pd = new PlayerDataStruct[1];
    public EquipmentLevelStruct el = new EquipmentLevelStruct();
    public Player NowPlayer;
    #endregion

    #region start
    private void Awake()
    {
        Initialize();
    }
    void Start()
    {
        // ���̾� ��ȣ�� ����Ͽ� ���̾� ����ũ ����
        playerLayerMask = (1 << layerNumber) | (1 << layerNumber2);

        // ���̾� ����ũ Ȯ��
        Debug.Log("Layer Mask: " + playerLayerMask.value);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            SavePlayerInfo();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            LoadJson("PlayerData" + DataManager.Instance.nowSlot.ToString() + ".json");
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
    public void SavePlayerInfo()
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
        string tempSavePath = savePath + nowSlot.ToString() + ".json";
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
    #region weapon_level_save
    public void EquipmentLevelSave()
    {
        //1. ���� ������
        //2. ���� �����͸� ������ el ����
        //3. ����ȭ
        //4. ���Ϸ� ����

        //1
        Weapon weapon = FindObjectOfType<Weapon>();

        //3
        var json = JsonConvert.SerializeObject(el, Formatting.Indented);

        //4
        string SavePath = savePath2 + nowSlot.ToString() + ".json";
        File.WriteAllText(SavePath, json);

    }
    #endregion

    public void EquipmentLevelLoad(string path)
    {
        var jsonPath = path;
        if(File.Exists(jsonPath))
        {
            string JsonString = File.ReadAllText(jsonPath);
            el = JsonConvert.DeserializeObject<EquipmentLevelStruct>(JsonString);

            Dictionary<int, EquipmentLevelStruct> weaponlevelDic = new Dictionary<int, EquipmentLevelStruct>();
        }
    }
    public void DataClear()
    {
        nowSlot = -1;
        NowPlayer = new Player();
    }
}
