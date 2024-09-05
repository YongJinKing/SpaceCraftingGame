using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStage1Manager : MonoBehaviour
{
    public Transform MortalBoxes;
    public Transform MortalGages;
    public Transform BossRabbit;
    public GameObject playerUI;

    public void StartGame()
    {
        playerUI.SetActive(true);
        MortalBoxes.gameObject.SetActive(true);
        MortalGages.gameObject.SetActive(true);
        BossRabbit.GetComponent<BossReadyState>().StartFight();
        // 이 아래 보스를 위한 UI(ex 체력바)도 키면 됩니다.
    }
}
