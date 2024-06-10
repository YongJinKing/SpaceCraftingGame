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
        //StartCoroutine(ProcessingState()); // �̵��� ���� �ڿ� State������ �ؾ���..
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
        //����� �����ϸ� ������ �i�ư��⸸ �ϴ� �˰��� >> Ÿ���� �߽����� ���� �׸��� ����ϴ� �˰������� ���� ����, �Ʒ��� �ϴ� ������ �Լ�
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
