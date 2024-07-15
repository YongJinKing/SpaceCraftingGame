using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    public enum EquipType
    {
        BareHand, Gun, Hammer, Pickaxe, Length
    }

    #region Properties
    #region Private
    private Animator _anim;
    private Animator anim
    {
        get
        {
            if (_anim == null)
            {
                _anim = GetComponent<Animator>();
            }
            return _anim;
        }
    }
    #endregion
    #region Protected
    protected EquipType I_EquipType;
    protected int B_Move;
    protected int B_LeftClick;
    protected int T_Dead;
    #endregion
    #region Public
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
        if (type > (int)EquipType.Length)
            I_EquipType = EquipType.BareHand;

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
    #endregion
    #endregion

    #region EventHandlers
    #endregion

    #region Coroutines
    #endregion

    #region MonoBehaviour
    private void Start()
    {
        I_EquipType = (EquipType)Animator.StringToHash("I_EquipType");
        B_Move = Animator.StringToHash("B_Move");
        B_LeftClick = Animator.StringToHash("B_LeftClick");
        T_Dead = Animator.StringToHash("T_Dead");
    }
    #endregion
}
