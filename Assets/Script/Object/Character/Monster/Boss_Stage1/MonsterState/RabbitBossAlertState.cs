using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitBossAlertState : MonsterState
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
        //StartCoroutine(ProcessingState()); // 이동이 끝난 뒤에 State변경을 해야함..
        StartCoroutine(FollowingTarget());
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
    protected IEnumerator ProcessingState()
    {
        //Wait until Select Action
        yield return StartCoroutine(SelectingAction());

        owner.stateMachine.ChangeState<MonsterAttackState>();
        yield return null;
    }

    protected IEnumerator FollowingTarget()
    {
        //현재는 감지하면 무조건 쫒아가기만 하는 알고리즘 >> 타겟을 중심으로 원을 그리며 경계하는 알고리즘으로 변경 예정, 아래는 일단 변경한 함수
        /*while (true)
        {
            owner.bossDirMove?.Activate(owner.target.transform.position - transform.position);
            yield return null;
        }*/
        owner.bossDirMove?.Activate(owner.target.transform.position - transform.position);
        yield return StartCoroutine(ProcessingState());

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
    }
    #endregion

    #region MonoBehaviour
    #endregion
}
