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
        StartCoroutine(FollowingTarget());
        StartCoroutine(SelectingAction());
    }
    public override void Exit()
    {
        base.Exit();
        StopAllCoroutines();
    }
    #endregion
    #endregion

    #region EventHandlers
    #endregion

    #region Coroutines
    protected IEnumerator FollowingTarget()
    {
        //현재는 감지하면 무조건 쫒아가기만 하는 알고리즘
        while (true)
        {
            owner.dirMove?.Activate(owner.target.transform.position - transform.position);
            yield return null;
        }
    }
    protected IEnumerator SelectingAction()
    {
        if (owner.attackActions == null)
        {
            Debug.Log("attackActions == null");
            yield break;
        }
            

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
