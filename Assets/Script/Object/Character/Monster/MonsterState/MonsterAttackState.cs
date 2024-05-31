using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttackState : MonsterState
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
    #endregion
    #region Public
    public override void Enter()
    {
        base.Enter();
        StartCoroutine(Attacking());
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
    protected IEnumerator Attacking()
    {
        if(owner.activatedAction == null)
        {
            yield return StartCoroutine(SelectingAction());
        }

        float dist = 0.0f;
        Vector2 dir;
        while (true)
        {
            if(owner.target == null)
            {
                owner.stateMachine.ChangeState<MonsterIdleState>();
            }

            dir = owner.target.transform.position - transform.position;

            //follow target
            owner.dirMove?.Activate(dir);


            dist = dir.magnitude;
            if (dist - owner.activatedAction.activeRadius < 0)
            {
                break;
            }
            yield return null;
        }

        owner.activatedAction.Activate(owner.target.transform.position);

        if (owner.activatedAction.fireAndForget)
            owner.stateMachine.ChangeState<MonsterMovableActionState>();
        else
            owner.stateMachine.ChangeState<MonsterUnmovableActionState>();
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
