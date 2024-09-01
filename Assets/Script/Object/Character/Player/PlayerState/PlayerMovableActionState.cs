using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovableActionState : PlayerState
{
    #region Properties
    #region Private
    private bool isClick = false;
    private Vector2 mousePos;
    #endregion
    #region Protected
    #endregion
    #region Public
    #endregion
    #region Events
    private UnityEvent<Vector2> moveEvent = new UnityEvent<Vector2>();
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
        InputController.Instance.mouseUpEvent.AddListener(OnMouseUpEvent);
        InputController.Instance.getMousePosEvent.AddListener(OnMouseMove);

        if (owner.activatedAction != null)
        {
            owner.activatedAction.OnActionEndEvent.AddListener(OnActionEnd);
        }

        UnitMovement unitMovement = GetComponent<UnitMovement>();
        moveEvent.AddListener(unitMovement.OnMoveToDir);
    }
    protected override void RemoveListeners()
    {
        base.RemoveListeners();
        InputController.Instance.moveEvent.RemoveListener(OnMove);
        InputController.Instance.mouseUpEvent.RemoveListener(OnMouseUpEvent);
        InputController.Instance.getMousePosEvent.RemoveListener(OnMouseMove);

        if (owner.activatedAction != null)
        {
            owner.activatedAction.OnActionEndEvent.RemoveListener(OnActionEnd);
        }
        owner.activatedAction = null;

        UnitMovement unitMovement = GetComponent<UnitMovement>();
        moveEvent.RemoveListener(unitMovement.OnMoveToDir);
    }
    #endregion
    #region Public
    public override void Enter()
    {
        isClick = true;

        base.Enter();
    }
    public override void Exit()
    {
        owner.activatedAction.Deactivate();

        //나중에 마우스 뗀 타입에 따른 스위치문 작성 필요
        owner.myAnim.SetLeftClick(false);
        owner.weaponRotationAxis.SetActive(false);

        base.Exit();
    }
    #endregion
    #endregion

    #region EventHandlers
    public void OnMove(Vector2 dir)
    {
        moveEvent?.Invoke(dir);

        owner.myAnim.SetMove(true);

        if (Mathf.Approximately(dir.x, 0.0f) && Mathf.Approximately(dir.y, 0.0f))
            owner.myAnim.SetMove(false);
    }
    public void OnMouseUpEvent(int type)
    {
        isClick = false;
    }
    public void OnMouseMove(Vector2 pos)
    {
        mousePos = pos;
    }
    public void OnActionEnd()
    {
        if (isClick)
        {
            owner.activatedAction.Activate(mousePos);
            return;
        }

        if(owner.modeType == 0)
        {
            owner.stateMachine.ChangeState<PlayerIdleState>();
            return;
        }
        else if (owner.modeType == 1)
        {
            owner.stateMachine.ChangeState<PlayerBuildModeState>();
            return;
        }

        if (!owner.isDead)
        {
            //Debug.Log("movablestate.onactionend");
            owner.stateMachine.ChangeState<PlayerIdleState>();
        }
    }
    #endregion

    #region Coroutines
    #endregion

    #region MonoBehaviour
    #endregion
}
