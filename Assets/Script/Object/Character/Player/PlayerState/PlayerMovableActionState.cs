using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovableActionState : PlayerState
{
    #region Properties
    #region Private
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
    public override void Exit()
    {
        owner.activatedAction.Deactivate();
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
        //나중에 마우스 뗀 타입에 따른 스위치문 작성 필요
        owner.myAnim.SetLeftClick(false);
        owner.weaponRotationAxis.SetActive(false);
    }
    public void OnActionEnd()
    {
        if(!owner.isDead)
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
