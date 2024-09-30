using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBuffVFX : MonoBehaviour
{
    [SerializeField] ParticleSystem buffVFX;
    public void PlayDuringTime(float time)
    {
        StartCoroutine(BuffVFX(time));
    }

    IEnumerator BuffVFX(float time)
    {
        buffVFX.Play();
        yield return new WaitForSeconds(time);
        buffVFX.Stop();
    }
}
