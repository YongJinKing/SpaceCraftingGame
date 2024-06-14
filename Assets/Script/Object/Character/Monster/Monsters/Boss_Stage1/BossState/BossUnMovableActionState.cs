using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossUnMovableActionState : BossState
{
    #region Properties
    #region Private
    #endregion
    #region Protected
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
    protected override void AddListeners()
    {
        base.AddListeners();
        if (owner.activatedAction != null)
        {
            owner.activatedAction.OnActionEndEvent.AddListener(OnActionEnd);
            Debug.Log("보스 언무버블 애드 리ㅏ스너");
        }
    }
    protected override void RemoveListeners()
    {
        base.RemoveListeners();
        if (owner.activatedAction != null)
        {
            owner.activatedAction.OnActionEndEvent.RemoveListener(OnActionEnd);
            Debug.Log("보스 언무버블 리무브 리ㅏ스너");
        }
        owner.activatedAction = null;
    }
    #endregion
    #region Public
    public override void Enter()
    {
        base.Enter();
    }
    public override void Exit()
    {
        owner.activatedAction.Deactivate();
        base.Exit();
    }
    #endregion
    #endregion

    #region EventHandlers
    public void OnActionEnd()
    {
        if (owner.target != null)
            owner.stateMachine.ChangeState<BossAlertState>();
        else
            owner.stateMachine.ChangeState<BossIdleState>();
        Debug.Log("보스 언무버블 액션앤드");
    }
    #endregion

    #region Coroutines
    #endregion

    #region MonoBehaviour
    #endregion
}
