using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BossRewardBoxAnimEvent : MonoBehaviour
{
    public GameObject rewardBox;
    public UnityEvent bossRewardsAct;
    public UnityEvent turnOnButtonAct;

    public void BossRewardsAnimation()
    {
        bossRewardsAct?.Invoke();
    }

    public void TurnOnButtonAnimation()
    {
        turnOnButtonAct?.Invoke();
        Destroy(rewardBox);
    }
}
