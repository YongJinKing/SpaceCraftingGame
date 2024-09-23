using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BGM // 이 순서대로 스크립터블 오브젝트에 사운드를 넣어야함 꼭!
{
    START,
    STAGESELECT,
    MAIN,
    BOSSENTRANCE,
    BOSS,
    MAXCOUNT
}

[CreateAssetMenu(fileName = "BGMSoundData", menuName = "Scriptable Object/BGMSoundData")]
public class BGMSoundData : ScriptableObject
{
    public AudioClip[] bgm;
}
