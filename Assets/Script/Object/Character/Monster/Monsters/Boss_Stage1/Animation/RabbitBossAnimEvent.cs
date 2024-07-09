using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RabbitBossAnimEvent : MonoBehaviour
{
    public UnityEvent ThrowEvent;
    public UnityEvent SuperJumpEvent;
    public UnityEvent SpinningEvent;
    public UnityEvent RiceRainEvent;
    
    public void ThrowAnimation()
    {
        ThrowEvent?.Invoke();
    }

    public void SuperJumpAnimation()
    {
        SuperJumpEvent?.Invoke();
    }

    public void SpinningAnimation()
    {
        SpinningEvent?.Invoke();
    }

    public void RiceRainAnimation()
    {
        RiceRainEvent?.Invoke();
    }
}
