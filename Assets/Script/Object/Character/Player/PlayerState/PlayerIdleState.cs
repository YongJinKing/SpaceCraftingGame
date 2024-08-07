using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;


/// <summary>
/// #need to modify later
/// </summary>
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
        InputController.Instance.mouseEvent.AddListener(OnMouse);

        UnitMovement unitMovement = GetComponent<UnitMovement>();
        moveEvent.AddListener(unitMovement.OnMoveToDir);
    }
    protected override void RemoveListeners()
    {
        base.RemoveListeners();
        InputController.Instance.moveEvent.RemoveListener(OnMove);
        InputController.Instance.mouseEvent.RemoveListener(OnMouse);

        UnitMovement unitMovement = GetComponent<UnitMovement>();
        moveEvent.RemoveListener(unitMovement.OnMoveToDir);
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
        base.Exit();
        owner.canEquip = false;
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
    /// <summary>
    /// 현재 하드코딩 되어있음 수정 필요
    /// #need to modify later
    /// </summary>
    public void OnMouse(int type, Vector2 pos)
    {
        //여기부분에서 Player의 Action 부분을 배열로 만들어서
        //하는것도 가능성 있을듯
        switch (type)
        {
            case 0:
                {
                    if(owner.mainAction != null)
                    {
                        owner.weaponRotationAxis.SetActive(true);
                        owner.mainAction.Activate(pos);
                        owner.myAnim.SetLeftClick(true);

                        owner.activatedAction = owner.mainAction;
                    }
                }
                break;
            case 1:
                {
                    if (owner.secondAction != null)
                    {
                        owner.weaponRotationAxis.SetActive(true);
                        owner.secondAction.Activate(pos);
                        //owner.myAnim.SetRightClick(true);

                        owner.activatedAction = owner.secondAction;
                    }
                }
                break;
            case 2:
                break;
        }
        
        if(owner.activatedAction != null && !owner.isDead)
        {
            if (owner.activatedAction.fireAndForget)
                owner.stateMachine.ChangeState<PlayerMovableActionState>();
            else
                owner.stateMachine.ChangeState<PlayerUnmovableActionState>();
        }
    }
    #endregion

    #region Coroutines
    #endregion

    #region MonoBehaviour
    #endregion
}
