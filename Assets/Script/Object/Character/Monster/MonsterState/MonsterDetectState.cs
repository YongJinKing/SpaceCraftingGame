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
        StartCoroutine(ProcessingState());
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

        while (!owner.ai.PathFinding(transform.position, owner.target.transform.position, out Vector2[] path))
        {
            Vector2 dir = owner.target.transform.position - transform.position;

            //targeting owner`s opponent
            RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, dir.magnitude, owner.targetMask);
            if(hit.collider != null)
            {
                owner.target = hit.collider.gameObject;
                Debug.Log(owner.target.transform.position);
            }
            yield return new WaitForSeconds(1.1f);
        }

        owner.stateMachine.ChangeState<MonsterAttackState>();
        yield return null;
    }

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
    }
    #endregion

    #region MonoBehaviour
    #endregion
}
