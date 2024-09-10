using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BossRewardBoxAnimEvent : MonoBehaviour
{
    public UnityEvent bossRewardsAct;
    public UnityEvent turnOnButtonAct;

    public void BossRewardsAnimation()
    {
        bossRewardsAct?.Invoke();
        Destroy(this.gameObject);
    }

    public void TurnOnButtonAnimation()
    {
        turnOnButtonAct?.Invoke();
    }
}
