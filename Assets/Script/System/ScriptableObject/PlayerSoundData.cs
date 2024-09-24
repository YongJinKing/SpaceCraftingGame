using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "PlayerSoundData", menuName = "Scriptable Object/PlayerSoundData")]
public class PlayerSoundData : ScriptableObject
{
    [Header("총 사운드"), Space(.5f)]
    public AudioClip Gun_Fire_Sound;
    public AudioClip Gun_Hit_Sound;

    [Header("제작 관련 사운드"), Space(.5f)]
    public AudioClip pickAxeSFX;
    public AudioClip hammerSFX;
}
