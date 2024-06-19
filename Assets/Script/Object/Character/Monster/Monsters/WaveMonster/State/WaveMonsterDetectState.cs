using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        for(int i = 0; i < owner[EStat.DetectRadius] && owner.target != null; ++i)
        {
            if (!owner.ai.PathFinding(transform.position, owner.target.transform.position, out Vector2[] path))
            {
                owner.target = owner.ai.TargetSelect(owner.targetMask, owner[EStat.DetectRadius] - i);
                yield return null;
            }
            else
            {
                break;
            }
        }

        if (owner.target == null)
            owner.stateMachine.ChangeState<WaveMonsterIdleState>();
        else
            owner.stateMachine.ChangeState<WaveMonsterAttackState>();
        yield return null;
    }

    protected IEnumerator FollowingTarget()
    {
        //현재는 감지하면 무조건 쫒아가기만 하는 알고리즘
        while (owner.target != null)
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
            yield return null;
        }

        owner.activatedAction = action;
    }
    #endregion

    #region MonoBehaviour
    #endregion
}
