using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���߿� ���� ��ũ��Ʈ >>>>>>>>>>>>>>>>>>>
public class BossStage2Manager : MonoBehaviour
{
    public Transform doodlebug;

    public void TurnOnDoodle()
    {
        doodlebug.gameObject.SetActive(true);
    }
}
