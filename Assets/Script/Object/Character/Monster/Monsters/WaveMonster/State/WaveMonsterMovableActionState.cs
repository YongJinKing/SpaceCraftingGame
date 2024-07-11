using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WaveMonsterMovableActionState : MonsterState
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
        base.AddListeners();

        UnitMovement movement = GetComponent<UnitMovement>();
        if (movement != null)
            moveToDirEvent.AddListener(movement.OnMoveToDir);

        if (owner.activatedAction != null)
        {
            owner.activatedAction.OnActionEndEvent.AddListener(OnActionEnd);
        }
    }
    protected override void RemoveListeners()
    {
        base.RemoveListeners();

        UnitMovement movement = GetComponent<UnitMovement>();
        if (movement != null)
            moveToDirEvent?.RemoveListener(movement.OnMoveToDir);

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
        owner.animator.SetBool("B_Move", false);
        owner.animator.SetBool("B_Attack", false);
        base.Exit();
        StopAllCoroutines();
    }
    #endregion
    #endregion

    #region EventHandlers
    public void OnActionEnd()
    {
        if(owner.target != null)
            owner.stateMachine.ChangeState<WaveMonsterDetectState>();
        else
            owner.stateMachine.ChangeState<WaveMonsterIdleState>();
    }
    #endregion

    #region Coroutines
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
    #endregion

    #region MonoBehaviour
    #endregion
}
