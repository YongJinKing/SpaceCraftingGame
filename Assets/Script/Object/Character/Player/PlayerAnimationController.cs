using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAnimationController : UnitAnimationController
{
    #region Properties
    #region Private

    #endregion
    #region Protected
    protected int I_EquipType;
    protected int B_Move;
    protected int B_LeftClick;
    protected int T_Dead;
    protected int T_Hit;
    #endregion
    #region Public
    public GameObject PlayerSoundManager;
    #endregion
    #region Events

    #endregion
    #endregion

    #region Constructor
    #endregion

    #region Methods
    #region Private
    #endregion
    #region Protected
    #endregion
    #region Public
    public void SetEquipType(int type)
    {
        anim.SetInteger((int)I_EquipType, type);
    }
    public void SetMove(bool move)
    {
        anim.SetBool(B_Move, move);
    }
    public void SetLeftClick(bool leftClick)
    {
        anim.SetBool(B_LeftClick, leftClick);
        
    }
    public void TriggerDeath()
    {
        anim.SetTrigger(T_Dead);
    }
    public void TriggerHit()
    {
        anim.SetTrigger(T_Hit);
    }
    #endregion
    #endregion

    #region EventHandlers
    public void OnAnimationEnd(int type)
    {
        animEndEvent?.Invoke(type);
    }
    public void OnAttackAnim(int type)
    {
        attackAnimEvent?.Invoke(type);
    }
    #endregion

    #region Coroutines
    #endregion

    #region MonoBehaviour
    private void Start()
    {
        I_EquipType = Animator.StringToHash("I_EquipType");
        B_Move = Animator.StringToHash("B_Move");
        B_LeftClick = Animator.StringToHash("B_LeftClick");
        T_Dead = Animator.StringToHash("T_Dead");
        T_Hit = Animator.StringToHash("T_Hit");
    }
    #endregion


}
