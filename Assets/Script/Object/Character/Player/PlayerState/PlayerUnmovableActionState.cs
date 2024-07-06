using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerUnmovableActionState : PlayerState
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
        base.AddListeners(); ;
        InputController.Instance.mouseUpEvent.AddListener(OnMouseUpEvent);

        if (owner.activatedAction != null)
        {
            owner.activatedAction.OnActionEndEvent.AddListener(OnActionEnd);
        }
    }
    protected override void RemoveListeners()
    {
        base.RemoveListeners();
        InputController.Instance.mouseUpEvent.RemoveListener(OnMouseUpEvent);

        if (owner.activatedAction != null)
        {
            owner.activatedAction.OnActionEndEvent.RemoveListener(OnActionEnd);
        }
        owner.activatedAction = null;
    }
    #endregion
    #region Public
    public override void Exit()
    {
        owner.activatedAction.Deactivate();
        owner.weaponRotationAxis.SetActive(false);
        base.Exit();
    }
    #endregion
    #endregion

    #region EventHandlers
    public void OnMouseUpEvent(int type)
    {
        //나중에 마우스 뗀 타입에 따른 스위치문 작성 필요
        owner.myAnim.SetLeftClick(false);
    }
    public void OnActionEnd()
    {
        owner.stateMachine.ChangeState<PlayerIdleState>();
    }
    #endregion

    #region Coroutines
    #endregion

    #region MonoBehaviour
    #endregion
}
