using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "PlayerSoundData", menuName = "Scriptable Object/PlayerSoundData")]
public class PlayerSoundData : ScriptableObject
{
    [Header("ÃÑ »ç¿îµå"), Space(.5f)]
    public AudioClip Gun_Fire_Sound;
    public AudioClip Gun_Hit_Sound;
}
