using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitBossIdleState : MonsterState
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
    protected IEnumerator SetTarget()
    {
        GameObject target = null;

        while (target != null)
        {
            //target = FindObjectOfType<Player>();
            yield return null;
        }
        owner.target = target;
        owner.stateMachine.ChangeState<RabbitBossAlertState>();
    }
    #endregion

    #region MonoBehaviour
    #endregion
}
