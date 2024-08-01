using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DataViewUI : MonoBehaviour
{
    public TextMeshProUGUI tmp1;
    public TextMeshProUGUI tmp2;
    public TextMeshProUGUI tmp3;
    public TextMeshProUGUI tmp4;
    public TextMeshProUGUI tmp5;

    //public TextMeshProUGUI name;

    public void Start()
    {
        //name.text += DataManager.Instance.NowPlayer.name;
    }

    void Update()
    {
     if(Input.GetKeyDown(KeyCode.Z))
        {
            ShowData();
        }

    }

    void ShowData()
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
}
