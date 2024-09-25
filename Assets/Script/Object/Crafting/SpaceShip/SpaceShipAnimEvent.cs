using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpaceShipAnimEvent : MonoBehaviour
{
    public UnityEvent TurnOnPlanetExplain;
    public UnityEvent TurnOffPlanetExplain;

    public void TurnOn()
    {
        TurnOnPlanetExplain?.Invoke();
    }

    public void TurnOff()
    {
        TurnOffPlanetExplain?.Invoke();
    }
}
