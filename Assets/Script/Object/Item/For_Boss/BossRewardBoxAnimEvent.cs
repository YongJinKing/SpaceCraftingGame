using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BossRewardBoxAnimEvent : MonoBehaviour
{
    public UnityEvent bossRewardsAct;

    public void BossRewardsAnimation()
    {
        bossRewardsAct?.Invoke();
        Destroy(this.gameObject);
    }
}
