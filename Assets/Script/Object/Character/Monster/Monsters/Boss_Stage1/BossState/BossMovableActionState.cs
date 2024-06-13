using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovableActionState : BossState
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
        }
    }
    protected override void RemoveListeners()
    {
        base.RemoveListeners();
        if (owner.activatedAction != null)
        {
            owner.activatedAction.OnActionEndEvent.RemoveListener(OnActionEnd);
        }
        owner.activatedAction = null;
    }
    #endregion
    #region Public
    public override void Enter()
    {
        base.Enter();
        StartCoroutine(FollowingTarget());
    }
    public override void Exit()
    {
        owner.activatedAction.Deactivate();
        base.Exit();
        StopAllCoroutines();
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
    }
    #endregion

    #region Coroutines
    protected IEnumerator FollowingTarget()
    {
        //보스의 이동속도의 80%(느리게 따라갈거면) 혹은 120%(빠르게 따라갈거면)로 플레이어를 따라감
        yield return null;
        
    }
    #endregion

    #region MonoBehaviour
    #endregion
}
