using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

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

        if (owner.activatedAction != null)
        {
            owner.activatedAction.OnActionEndEvent.AddListener(OnActionEnd);
        }
    }
    protected override void RemoveListeners()
    {
        base.RemoveListeners();
        InputController.Instance.moveEvent.RemoveListener(OnMove);

        if(owner.activatedAction != null)
        {
            owner.activatedAction.OnActionEndEvent.RemoveListener(OnActionEnd);
        }
        owner.activatedAction = null;
    }
    #endregion
    #region Public
    public override void Exit()
    {
        owner.moveAction.Deactivate();
        owner.activatedAction.Deactivate();
        owner.myAnim.SetLeftClick(false);
        owner.weaponRotationAxis.SetActive(false);
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
    public void OnActionEnd()
    {
        Debug.Log("movablestate.onactionend");
        owner.stateMachine.ChangeState<PlayerIdleState>();
    }
    #endregion

    #region Coroutines
    #endregion

    #region MonoBehaviour
    #endregion
}
