using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossUnMovableActionState : BossState
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
        if (owner.activatedAction != null)
        {
            owner.activatedAction.OnActionEndEvent.AddListener(OnActionEnd);
            Debug.Log("���� �𹫹��� �ֵ� ��������");
        }
    }
    protected override void RemoveListeners()
    {
        base.RemoveListeners();
        if (owner.activatedAction != null)
        {
            owner.activatedAction.OnActionEndEvent.RemoveListener(OnActionEnd);
            Debug.Log("���� �𹫹��� ������ ��������");
        }
        //owner.activatedAction = null;
    }
    #endregion
    #region Public
    public override void Enter()
    {
        base.Enter();
    }
    public override void Exit()
    {
        owner.activatedAction.Deactivate();
        base.Exit();
    }
    #endregion
    #endregion

    #region EventHandlers
    public void OnActionEnd()
    {
        /*if (owner.target != null)
        {
            owner.stateMachine.ChangeState<BossAlertState>();
            Debug.Log("unmovable -> alert");
        }
        else
        {
            owner.stateMachine.ChangeState<BossIdleState>();
            Debug.Log("unmovable -> idle");
        }*/
        StartCoroutine(ChangeStateAfterDelay(owner.activatedAction.delayTime));
        Debug.Log("���� �𹫹��� �׼Ǿص�");
    }
    #endregion

    #region Coroutines
    IEnumerator ChangeStateAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (owner.target != null)
        {
            owner.stateMachine.ChangeState<BossAlertState>();
            Debug.Log("unmovable -> alert");
        }
        else
        {
            owner.stateMachine.ChangeState<BossIdleState>();
            Debug.Log("unmovable -> idle");
        }
    }
    #endregion

    #region MonoBehaviour
    #endregion
}
