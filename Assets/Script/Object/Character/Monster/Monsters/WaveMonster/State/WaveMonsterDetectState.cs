using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WaveMonsterDetectState : MonsterState
{
    #region Properties
    #region Private
    #endregion
    #region Protected
    #endregion
    #region Public
    #endregion
    #region Events
    public UnityEvent<Vector2> moveToDirEvent = new UnityEvent<Vector2>();
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
        UnitMovement movement = GetComponent<UnitMovement>();
        if (movement != null)
            moveToDirEvent.AddListener(movement.OnMoveToDir);
    }
    protected override void RemoveListeners()
    {
        UnitMovement movement = GetComponent<UnitMovement>();
        if (movement != null)
            moveToDirEvent?.RemoveListener(movement.OnMoveToDir);
    }
    #endregion
    #region Public
    public override void Enter()
    {
        base.Enter();
        StartCoroutine(ProcessingState());
        StartCoroutine(FollowingTarget());
    }
    public override void Exit()
    {
        StopAllCoroutines();
        base.Exit();
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

        if (owner.target == null)
        {
            Debug.Log("WaveMonsterDetectState.ProcessingState Idle 전환");
            owner.stateMachine.ChangeState<WaveMonsterIdleState>();
        }
        else
        {
            owner.stateMachine.ChangeState<WaveMonsterAttackState>();
        }
        yield return null;
    }

    protected IEnumerator FollowingTarget()
    {
        owner.animator.SetBool("B_Move", true);
        //현재는 감지하면 무조건 쫒아가기만 하는 알고리즘
        while (owner.target != null)
        {
            moveToDirEvent?.Invoke(owner.target.transform.position - transform.position);
            yield return null;
        }
        owner.animator.SetBool("B_Move", false);
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
            yield return null;
        }

        owner.activatedAction = action;
    }
    #endregion

    #region MonoBehaviour
    #endregion
}
