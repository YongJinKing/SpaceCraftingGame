using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BuildSFXType
{
    BuildStart, DronWorking, BuildComplete, BuildDestroy, MAXINDEX
}

[CreateAssetMenu(fileName = "BuildingSoundData", menuName = "Scriptable Object/BuildingSoundData")]
public class BuildingSoundData : ScriptableObject
{
    public AudioClip[] BuildSFXs;
    
}
