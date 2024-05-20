using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerIdleState : PlayerState
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
        InputController inputController = GameObject.Find("InputController").GetComponent<InputController>();
        inputController.moveEvent.AddListener(OnMove);
    }
    protected override void RemoveListeners()
    {
        base.RemoveListeners();
        InputController inputController = GameObject.Find("InputController").GetComponent<InputController>();
        if(inputController != null)
            inputController.moveEvent.RemoveListener(OnMove);
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
        owner.moveAction.Deactivate();
    }
    #endregion
    #endregion

    #region EventHandlers
    public void OnMove(Vector2 dir)
    {
        owner.moveAction.Activate(dir);
    }
    #endregion

    #region Coroutines
    #endregion

    #region MonoBehaviour
    #endregion
}
