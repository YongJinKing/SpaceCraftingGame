using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class DataViewUI : MonoBehaviour
{
    public TextMeshProUGUI tmp1;
    public TextMeshProUGUI tmp2;
    public TextMeshProUGUI tmp3;
    public TextMeshProUGUI tmp4;
    public TextMeshProUGUI tmp5;


    public void Start()
    {
        //name.text += DataManager.Instance.playerName;
   
        if (File.Exists("PlayerData" + DataManager.Instance.nowSlot.ToString() + ".json"))
        {
            DataManager.Instance.LoadJson("PlayerData" + DataManager.Instance.nowSlot.ToString() + ".json");
            UpdateStatUI();
        }
       
 
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            //ShowData();
            UpdateStatUI();
        }
    }

    /*void ShowData()
    {
        tmp1.text = "MaxHP: " + DataManager.Instance.pd[0].MaxHP.ToString();
        tmp2.text = "MoveSpeed: " + DataManager.Instance.pd[0].moveSpeed.ToString();
        tmp3.text = "ATK: " + DataManager.Instance.pd[0].ATK.ToString();
        tmp4.text = "ATKSpeed: " + DataManager.Instance.pd[0].ATKSpeed.ToString();
        tmp5.text = "Priority: " + DataManager.Instance.pd[0].Priority.ToString();
    }*/

    void UpdateStatUI()
    {
        tmp1.text = "MaxHP: " + DataManager.Instance.pd[0].MaxHP.ToString();
        tmp2.text = "MoveSpeed: " + DataManager.Instance.pd[0].moveSpeed.ToString();
        tmp3.text = "ATK: " + DataManager.Instance.pd[0].ATK.ToString();
        tmp4.text = "ATKSpeed: " + DataManager.Instance.pd[0].ATKSpeed.ToString();
        tmp5.text = "Priority: " + DataManager.Instance.pd[0].Priority.ToString();
    }

    public void Save()
    {
        DataManager.Instance.SavePlayerInfo();
    }

    public void OnIncreaseStatsButtonClicked()
    {
        // StatController를 사용하여 스탯 증가
        StatController statController = FindObjectOfType<StatController>();
        statController.IncreasePlayerStats();

        // UI 업데이트
        UpdateStatUI();
    }
}
