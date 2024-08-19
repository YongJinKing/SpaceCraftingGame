using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    AudioSource m_audioSource;

    private void Awake()
    {
        Initialize();
    }

    public void SetClip(AudioClip clip)
    {
        m_audioSource.clip = clip;
    }

    public void PlayClip()
    {
        m_audioSource.Play();
    }

    public void Stopclip()
    {
        m_audioSource.Stop();
    }
    
}
