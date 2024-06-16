using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveMonsterUnmovableActionState : MonsterState
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
        }
    }
    protected override void RemoveListeners()
    {
        base.RemoveListeners();
        if (owner.activatedAction != null)
        {
            owner.activatedAction.OnActionEndEvent.RemoveListener(OnActionEnd);
        }
        owner.activatedAction = null;
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
        if (owner.target != null)
            owner.stateMachine.ChangeState<WaveMonsterDetectState>();
        else
            owner.stateMachine.ChangeState<WaveMonsterIdleState>();
    }
    #endregion

    #region Coroutines
    #endregion

    #region MonoBehaviour
    #endregion
}
