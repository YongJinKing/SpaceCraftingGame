using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
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
        InputController.Instance.moveEvent.AddListener(OnMove);
        InputController.Instance.mouseEvent.AddListener(OnMouse);
        InputController.Instance.numberKeyEvent.AddListener(OnNumberKey);
    }
    protected override void RemoveListeners()
    {
        base.RemoveListeners();
        InputController.Instance.moveEvent.RemoveListener(OnMove);
        InputController.Instance.mouseEvent.RemoveListener(OnMouse);
        InputController.Instance.numberKeyEvent.RemoveListener(OnNumberKey);
    }
    #endregion
    #region Public
    public override void Enter()
    {
        base.Enter();
    }
    public override void Exit() 
    {
        owner.moveAction.Deactivate();
        base.Exit();
    }
    #endregion
    #endregion

    #region EventHandlers
    public void OnMove(Vector2 dir)
    {
        owner.moveAction.Activate(dir);
        owner.myAnim.SetMove(true);

        if (Mathf.Approximately(dir.x, 0.0f) && Mathf.Approximately(dir.y, 0.0f))
            owner.myAnim.SetMove(false);
    }
    public void OnMouse(int type, Vector2 pos)
    {
        owner.attackAction.Activate(pos);
        owner.myAnim.SetLeftClick(true);
        owner.weaponRotationAxis.SetActive(true);
        owner.activatedAction = owner.attackAction;

        if (owner.activatedAction.fireAndForget)
            owner.stateMachine.ChangeState<PlayerMovableActionState>();
        else
            owner.stateMachine.ChangeState<PlayerUnmovableActionState>();
    }
    public void OnNumberKey(int num)
    {
        owner.myAnim.SetEquipType(num);
    }
    #endregion

    #region Coroutines
    #endregion

    #region MonoBehaviour
    #endregion
}
