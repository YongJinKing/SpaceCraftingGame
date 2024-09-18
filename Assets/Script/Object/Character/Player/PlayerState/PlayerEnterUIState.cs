using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnterUIState : PlayerState
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
        FindObjectOfType<SpaceShipEnter>().UIExitEvent.AddListener(OnExit);
    }

    protected override void RemoveListeners()
    {
        base.RemoveListeners();
        FindObjectOfType<SpaceShipEnter>().UIExitEvent.RemoveListener(OnExit);
    }
    #endregion
    #region Public
    public override void Enter()
    {
        base.Enter();
        owner.canEquip = true;
    }

    public override void Exit()
    {
        owner.canEquip = false;
        base.Exit();
    }
    #endregion
    #endregion

    #region EventHandlers
    public void OnExit()
    {
        switch (owner.modeType)
        {
            case 0:
                owner.stateMachine.ChangeState<PlayerIdleState>();
                break;
            case 1:
                owner.stateMachine.ChangeState<PlayerBuildModeState>();
                break;
        }
    }
    #endregion

    #region Coroutines
    #endregion

    #region MonoBehaviour
    #endregion
}
