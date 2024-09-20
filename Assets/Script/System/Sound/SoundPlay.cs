using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlay : MonoBehaviour
{
    public AudioClip soundClip;

    // Start is called before the first frame update
    void OnEnable()
    {
        SoundManager.Instance.PlaySFX(soundClip);
    }
}
