using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStage1Manager : MonoBehaviour
{
    public Transform MortalBoxes;
    public Transform MortalGages;
    public Transform BossRabbit;

    public void StartGame()
    {
        MortalBoxes.gameObject.SetActive(true);
        MortalGages.gameObject.SetActive(true);
        BossRabbit.gameObject.SetActive(true);
        // 이 아래 보스를 위한 UI(ex 체력바)도 키면 됩니다.
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5))
        {
            StartGame(); // 이 함수로 보스전을 시작하면 됩니다.
        }   
    }
}
