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
            owner.stateMachine.ChangeState<MonsterIdleState>();
        }

        
        Vector2 dir;
        float dist;
        do
        {
            dir = owner.target.transform.position - transform.position;
            dist = dir.magnitude;

            if (owner.target == null)
            {
                owner.stateMachine.ChangeState<MonsterIdleState>();
            }

            //follow target
            owner.dirMove?.Activate(dir);

            yield return null;
        }

        while (dist - owner.activatedAction.activeRadius < 0);

        owner.activatedAction.Activate(owner.target.transform.position);

        if (owner.activatedAction.fireAndForget)
            owner.stateMachine.ChangeState<MonsterMovableActionState>();
        else
            owner.stateMachine.ChangeState<MonsterUnmovableActionState>();
    }
    #endregion

    #region MonoBehaviour
    #endregion
}
