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
        
        owner.activatedAction.Activate(owner.target.transform.position); // �����س��� �� ���ϵ��� �����Ѵ�.


        if (owner.activatedAction.fireAndForget) // �� ���ϵ��� �������� ��, ������ ������ �� �ִ��� ������, �� ������ �����ϸ� �÷��̾ ������� �ƴϸ� ���Ͽ� �������� ���ѽ�ų�� ���� ������ ���� ���� �ش� ������Ʈ�� �Ѱ��ش�.
            owner.stateMachine.ChangeState<BossMovableActionState>();
        else
            owner.stateMachine.ChangeState<BossUnMovableActionState>();
    }
    #endregion

    #region MonoBehaviour
    #endregion
}
