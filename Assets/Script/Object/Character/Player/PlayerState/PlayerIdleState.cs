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
    /// <summary>
    /// 현재 하드코딩 되어 있음
    /// 수정 필요
    /// #need to modify later
    /// </summary>
    public void OnNumberKey(int num)
    {
        //나중에 아이템에서 타입을 받아서 하는 방식으로 바꿀것.
        //현재는 하드코딩
        //Equipment Manager를 만들어서 호출하는 방식도 좋을듯
        //현재는 0은 맨손 1은 총, 2는 망치, 3은 곡괭이
        owner.myAnim.SetEquipType(num);
        owner.AR.UnEquip();
        owner.Hammer.UnEquip();
        owner.PickAxe.UnEquip();
        switch (num)
        {
            case 0:
                break;
            case 1:
                owner.AR.Equip();
                break;
            case 2:
                owner.Hammer.Equip();
                break;
            case 3:
                owner.PickAxe.Equip();
                break;
        }
    }
    #endregion

    #region Coroutines
    #endregion

    #region MonoBehaviour
    #endregion
}
