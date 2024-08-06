using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RabbitWorkerAnimEvent : MonoBehaviour
{
    public UnityEvent StartWorkAct;
    public UnityEvent StopWorkAct;

    public void StartWorkingEvent()
    {
        StartWorkAct?.Invoke();
    }

    public void StopWorkingEvent()
    {
        StopWorkAct?.Invoke();
    }
}

