using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MonsterDataViewUI : MonoBehaviour
{
    public TextMeshProUGUI tmm1;
    public TextMeshProUGUI tmm2;
    public TextMeshProUGUI tmm3;
    public TextMeshProUGUI tmm4;
    public TextMeshProUGUI tmm5;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            ShowData();
        }
    }

    void ShowData()
    {
        tmm1.text = "MaxHP: " + MonsterDataManager.Instance.md[0].MaxHP.ToString();
        tmm2.text = "MoveSpeed: " + MonsterDataManager.Instance.md[0].moveSpeed.ToString();
        tmm3.text = "ATK: " + MonsterDataManager.Instance.md[0].ATK.ToString();
        tmm4.text = "ATKSpeed: " + MonsterDataManager.Instance.md[0].ATKSpeed.ToString();
        tmm5.text = "Priority: " + MonsterDataManager.Instance.md[0].Priority.ToString();
    }
}
