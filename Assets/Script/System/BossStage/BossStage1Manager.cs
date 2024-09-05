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
        // �� �Ʒ� ������ ���� UI(ex ü�¹�)�� Ű�� �˴ϴ�.
    }
}
