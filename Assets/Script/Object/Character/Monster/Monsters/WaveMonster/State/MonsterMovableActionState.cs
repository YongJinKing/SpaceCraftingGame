using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovableActionState : MonsterState
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
        if(owner.target != null)
            owner.stateMachine.ChangeState<MonsterDetectState>();
        else
            owner.stateMachine.ChangeState<MonsterIdleState>();
    }
    #endregion

    #region Coroutines
    protected IEnumerator FollowingTarget()
    {
        //����� �����ϸ� ������ �i�ư��⸸ �ϴ� �˰���
        while (owner.target != null)
        {
            owner.dirMove?.Activate(owner.target.transform.position - transform.position);
            yield return null;
        }
    }
    #endregion

    #region MonoBehaviour
    #endregion
}
