using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StructureAnimEvent : MonoBehaviour
{
    public UnityEvent BuildCompleteVFXEvent;
    public UnityEvent DestroyEvent;
    public UnityEvent TurnOnMiningVFXEvent;
    public UnityEvent TurnOffMiningVFXEvent;

    public void StopBuildCompleteVFXEvent()
    {
        BuildCompleteVFXEvent?.Invoke();
    }
    public void PlayDestroyEvent()
    {
        DestroyEvent?.Invoke();
    }

    public void PlayTurnOnMiningVFXEvent()
    {
        TurnOnMiningVFXEvent?.Invoke();
    }

    public void PlayTurnOffMiningVFXEvent()
    {
        TurnOffMiningVFXEvent?.Invoke();
    }
}
