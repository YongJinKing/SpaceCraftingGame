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


[CreateAssetMenu(fileName = "SoundData", menuName = "Scriptable Object/SoundData")]

public class SoundData: ScriptableObject
{
    public AudioClip[] bgm;

    public AudioClip typingSFX;

    #region Boss Sound
    public AudioClip bossJumpAttack;
    public AudioClip bossSpinAttack;
    public AudioClip bossSuperJumpAttack;
    public AudioClip bossThrowAttack;
    public AudioClip bossFootstep;
    public AudioClip bossDead;
    #endregion
}
