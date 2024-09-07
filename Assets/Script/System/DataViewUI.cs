using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DataViewUI : MonoBehaviour
{
    public Stat myStat;

    public TextMeshProUGUI tmp1;
    public TextMeshProUGUI tmp2;
    public TextMeshProUGUI tmp3;
    public TextMeshProUGUI tmp4;
    public TextMeshProUGUI tmp5;


    public void Start()
    {
        

        if (File.Exists("PlayerData" + DataManager.Instance.nowSlot.ToString() + ".json"))
        {
            DataManager.Instance.LoadJson("PlayerData" + DataManager.Instance.nowSlot.ToString() + ".json");
            UpdateStatUI();
        }

        myStat = FindObjectOfType<Stat>();

        myStat.AddStatEventListener(EStat.MaxHP, OnMaxHPChanged);
        myStat.AddStatEventListener(EStat.MoveSpeed, OnMoveSpeedChanged);
        myStat.AddStatEventListener(EStat.ATK, OnATKChanged);
        myStat.AddStatEventListener(EStat.ATKSpeed, OnATKSpeedChanged);
    }

    public void OnMaxHPChanged(float oldVal, float newVal)
    {
        float MaxHp = myStat[EStat.MaxHP];
        float RawHp = myStat.GetRawStat(EStat.MaxHP);
        tmp1.text = $"{MaxHp}({RawHp} + {(MaxHp - RawHp)})";
    }

    public void OnMoveSpeedChanged(float oldVal, float newVal)
    {
        float MoveSpeed = myStat[EStat.MoveSpeed];
        float RawMoveSpeed = myStat.GetRawStat(EStat.MoveSpeed);
        tmp2.text = $"{MoveSpeed}({RawMoveSpeed} + {(MoveSpeed - RawMoveSpeed)})";
    }

    public void OnATKChanged(float oldVal, float newVal)
    {
        float ATK = myStat[EStat.ATK];
        float RawATK = myStat.GetRawStat(EStat.ATK);
        tmp3.text = $"{ATK}({RawATK} + {(ATK - RawATK)})";
    }

    public void OnATKSpeedChanged(float oldVal, float newVal)
    {
        float ATKSpeed = myStat[EStat.ATKSpeed];
        float RawATKSpeed = myStat.GetRawStat(EStat.ATKSpeed);
        tmp4.text = $"{ATKSpeed}({RawATKSpeed} + {(ATKSpeed - RawATKSpeed)})";
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            UpdateStatUI();
        }
    }

    

    void UpdateStatUI()
    {
        tmp1.text = "MaxHP: " + DataManager.Instance.pd[0].MaxHP.ToString();
        tmp2.text = "MoveSpeed: " + DataManager.Instance.pd[0].MoveSpeed.ToString();
        tmp3.text = "ATK: " + DataManager.Instance.pd[0].ATK.ToString();
        tmp4.text = "ATKSpeed: " + DataManager.Instance.pd[0].ATKSpeed.ToString();
        tmp5.text = "Priority: " + DataManager.Instance.pd[0].Priority.ToString();
    }

    public void Save()
    {
        DataManager.Instance.Save();
    }

    public void OnIncreaseStatsButtonClicked()
    {
        // StatController를 사용하여 스탯 증가
        //StatController statController = FindObjectOfType<StatController>();
        //statController.IncreasePlayerStats();

        // UI 업데이트
        UpdateStatUI();
    }
}
