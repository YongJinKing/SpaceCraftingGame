using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BGM // �� ������� ��ũ���ͺ� ������Ʈ�� ���带 �־���� ��!
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
