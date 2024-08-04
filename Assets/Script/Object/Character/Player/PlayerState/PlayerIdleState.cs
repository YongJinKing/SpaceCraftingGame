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
    /// ���� �ϵ��ڵ� �Ǿ����� ���� �ʿ�
    /// #need to modify later
    /// </summary>
    public void OnMouse(int type, Vector2 pos)
    {
        //����κп��� Player�� Action �κ��� �迭�� ����
        //�ϴ°͵� ���ɼ� ������
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
    /// ���� �ϵ��ڵ� �Ǿ� ����
    /// ���� �ʿ�
    /// #need to modify later
    /// </summary>
    public void OnNumberKey(int num)
    {
        //���߿� �����ۿ��� Ÿ���� �޾Ƽ� �ϴ� ������� �ٲܰ�.
        //����� �ϵ��ڵ�
        //Equipment Manager�� ���� ȣ���ϴ� ��ĵ� ������
        //����� 0�� �Ǽ� 1�� ��, 2�� ��ġ, 3�� ���
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
