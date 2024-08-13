using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 나중에 지울 스크립트 >>>>>>>>>>>>>>>>>>>
public class BossStage2Manager : MonoBehaviour
{
    public Transform doodlebug;

    public void TurnOnDoodle()
    {
        doodlebug.gameObject.SetActive(true);
    }
}
