using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BossSoundData", menuName = "Scriptable Object/BossSoundData")]

public class BossSoundData: ScriptableObject
{
    public AudioClip typingSFX;

    #region Boss Sound
    [Header("���� ���� ���� ����"), Space(.5f)]
    public AudioClip bossJump;
    public AudioClip bossJumpAttack;
    public AudioClip bossSpinAttack;
    public AudioClip bossSuperJumpAttack;
    public AudioClip bossThrowReady;
    public AudioClip bossThrowAttack;
    public AudioClip meteorRiceFall;
    public AudioClip slowingGround;
    public AudioClip bossEat;
    public AudioClip bossBuff;
    public AudioClip bossHitMortalBox;
    public AudioClip mortalBoxSpawnRice;
    public AudioClip riceBomb;

    [Header("���� ���� ����"), Space(.5f)]
    public AudioClip bossFootstep;
    public AudioClip bossDead;
    public AudioClip bossDeadImpact;

    [Header("Ŭ���� �� ����"), Space(.5f)]
    public AudioClip rewardBoxDrop;
    public AudioClip rewardBoxMove;
    public AudioClip rewardBoxShinning;
    public AudioClip rewardSpawnItem;
    
    #endregion
}
