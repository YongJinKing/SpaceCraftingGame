using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildDronAnimEvent : MonoBehaviour
{
    public BuildSFXType buildSFXType;
    public void PlayBuildSound()
    {
        SoundManager.Instance.PlaySFX(SoundManager.Instance.buildingSoundData.BuildSFXs[(int)buildSFXType]);
    }
}
