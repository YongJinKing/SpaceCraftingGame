using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveMonster : Monster
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
    protected override void OnDead()
    {
        base.OnDead();
        stateMachine.ChangeState<WaveMonsterDeadState>();
    }
    protected override void Initialize()
    {
        base.Initialize();
        stateMachine.ChangeState<WaveMonsterInitState>();
    }
    #endregion
    #region Public
    #endregion
    #endregion

    #region EventHandlers
    #endregion

    #region Coroutines
    #endregion

    #region MonoBehaviour
    #endregion
}
