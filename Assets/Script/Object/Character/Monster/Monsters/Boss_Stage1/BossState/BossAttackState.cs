using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BossAttackState : BossState
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
        if (owner.activatedAction == null)
        {
            owner.stateMachine.ChangeState<BossIdleState>();
        }

        yield return null;
        
        owner.activatedAction.Activate(owner.target.transform.position); // 구현해놓은 각 패턴들을 실행한다.


        if (owner.activatedAction.fireAndForget) // 각 패턴들이 실행중일 때, 보스가 움직일 수 있는지 없는지, 즉 패턴을 실행하며 플레이어를 따라올지 아니면 패턴에 움직임을 제한시킬지 등을 설정된 값에 따라서 해당 스테이트로 넘겨준다.
            owner.stateMachine.ChangeState<BossMovableActionState>();
        else
            owner.stateMachine.ChangeState<BossUnMovableActionState>();
    }
    #endregion

    #region MonoBehaviour
    #endregion
}
