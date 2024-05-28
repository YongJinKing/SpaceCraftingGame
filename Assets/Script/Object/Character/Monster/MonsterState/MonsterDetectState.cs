using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDetectState : MonsterState
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
    }
    protected override void RemoveListeners()
    {
        base.RemoveListeners();
    }
    #endregion
    #region Public
    public override void Enter()
    {
        base.Enter();
        StartCoroutine(FollowTarget());
        StartCoroutine(SelectingAction());
    }
    public override void Exit()
    {
        base.Exit();
        StopCoroutine(FollowTarget());
    }
    #endregion
    #endregion

    #region EventHandlers
    #endregion

    #region Coroutines
    protected IEnumerator FollowTarget()
    {
        while (true)
        {
            owner.dirMove?.Activate(owner.target.transform.position - transform.position);
            yield return null;
        }
    }
    protected IEnumerator SelectingAction()
    {
        if (owner.attackActions == null)
            yield break;

        Action action = null;
        while (action == null)
        {
            action = owner.ai.SelectAction(owner.attackActions);
            if (!action.available)
                action = null;
            yield return null;
        }
        owner.activatedAction = action;
        owner.stateMachine.ChangeState<MonsterAttackState>();
    }
    #endregion

    #region MonoBehaviour
    #endregion
}
