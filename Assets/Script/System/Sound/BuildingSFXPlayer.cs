using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSFXPlayer : MonoBehaviour
{
    public BuildSFXType BuildSFXType;
    private void OnEnable()
    {
        SoundManager.Instance.PlaySFX(SoundManager.Instance.buildingSoundData.BuildSFXs[(int)BuildSFXType]);
    }
}
