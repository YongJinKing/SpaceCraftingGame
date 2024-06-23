using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RabbitBossAnimEvent : MonoBehaviour
{
    public UnityEvent ThrowEvent;
    public UnityEvent SuperJumpEvent;
    public void ThrowAnimation()
    {
        ThrowEvent?.Invoke();
    }

    public void SuperJumpAnimation()
    {
        SuperJumpEvent?.Invoke();
    }
}
