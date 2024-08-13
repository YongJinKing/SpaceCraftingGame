using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���̾ �ٲ㼭 �����ð� ����
/// </summary>
public class PlayerHitState : PlayerState
{
    #region Properties
    #region Private
    private LayerMask playerMask;
    private LayerMask playerAvoidMask;
    #endregion
    #region Protected
    #endregion
    #region Public
    public float invincibleTime;
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
    protected override void AddListeners()
    {
        base.AddListeners();
        owner.myAnim.animEndEvent.AddListener(OnAnimEnd);
    }
    protected override void RemoveListeners()
    {
        base.RemoveListeners();
        owner.myAnim.animEndEvent.RemoveListener(OnAnimEnd);
    }
    #endregion
    #region Public
    public override void Enter()
    {
        owner.weaponRotationAxis.SetActive(false);
        owner.gameObject.layer = playerAvoidMask;
        owner.myAnim.TriggerHit();
        base.Enter();
    }
    public override void Exit()
    {
        owner.gameObject.layer = playerMask;
        base.Exit();
    }
    #endregion
    #endregion

    #region EventHandlers
    public void OnAnimEnd(int type)
    {
        if(type == 0)
        {
            owner.stateMachine.ChangeState<PlayerIdleState>();
        }
    }
    #endregion

    #region Coroutines
    private IEnumerator ProcessingState()
    {
        yield return null;
    }

    #endregion

    #region MonoBehaviour
    protected override void Awake() 
    {
        base.Awake();
        playerMask = LayerMask.NameToLayer("Player");
        playerAvoidMask = LayerMask.NameToLayer("Player_Avoid");
    }
    #endregion
}
