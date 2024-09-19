using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MortalBoxAnimEvent : MonoBehaviour
{

    public UnityEvent riceRainAnimation;
    
    public void PlayRiceRainAnimation()
    {
        riceRainAnimation?.Invoke();
    }

    public void JulguSpawnRiceSound()
    {
        SoundManager.Instance.PlaySFX(SoundManager.Instance.bossSoundData.mortalBoxSpawnRice);
    }
}
