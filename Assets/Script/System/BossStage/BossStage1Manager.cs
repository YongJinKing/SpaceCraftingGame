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
        // �� �Ʒ� ������ ���� UI(ex ü�¹�)�� Ű�� �˴ϴ�.
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5))
        {
            StartGame(); // �� �Լ��� �������� �����ϸ� �˴ϴ�.
        }   
    }
}
