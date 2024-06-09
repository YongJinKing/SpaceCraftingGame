using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitBossInitState : MonsterState
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
        StartCoroutine(Init());
    }

    public override void Exit()
    {
        base.Exit();
    }
    #endregion
    #endregion

    #region EventHandlers
    #endregion

    #region Coroutines
    private IEnumerator Init()
    {
        yield return null;
        owner.stateMachine.ChangeState<RabbitBossIdleState>();
    }
    #endregion

    #region MonoBehaviour
    #endregion
}
