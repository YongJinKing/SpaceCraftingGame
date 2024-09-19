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


[CreateAssetMenu(fileName = "BossSoundData", menuName = "Scriptable Object/BossSoundData")]

public class BossSoundData: ScriptableObject
{
    public AudioClip[] bgm;

    public AudioClip typingSFX;

    #region Boss Sound
    [Header("보스 패턴 관련 사운드"), Space(.5f)]
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

    [Header("상태 관련 사운드"), Space(.5f)]
    public AudioClip bossFootstep;
    public AudioClip bossDead;
    public AudioClip bossDeadImpact;

    [Header("클리어 시 사운드"), Space(.5f)]
    public AudioClip rewardBoxDrop;
    public AudioClip rewardBoxMove;
    public AudioClip rewardBoxShinning;
    public AudioClip rewardSpawnItem;
    
    #endregion
}
