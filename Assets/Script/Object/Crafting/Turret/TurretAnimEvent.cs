using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class TurretAnimEvent : MonoBehaviour
{
    public UnityEvent attackEvent;


    public void StartAttackAnimation()
    {
        attackEvent?.Invoke();
        Debug.Log("");
    }
}
