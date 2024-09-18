using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���̾ �ٲ㼭 �����ð� ����
/// </summary>
public class PlayerHitState : PlayerState
{
    #region Properties
    #region Private
    private LayerMask playerMask;
    private LayerMask playerAvoidMask;
    private Coroutine process;
    #endregion
    #region Protected
    #endregion
    #region Public
    public float invincibleTime = 1.0f;
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
        owner.myAnim.animEndEvent.AddListener(OnAnimEnd);
    }
    protected override void RemoveListeners()
    {
        base.RemoveListeners();
        owner.myAnim.animEndEvent.RemoveListener(OnAnimEnd);
    }
    #endregion
    #region Public
    public override void Enter()
    {
        owner.weaponRotationAxis.SetActive(false);
        owner.myAnim.TriggerHit();

        if(process != null)
        {
            StopCoroutine(process);
        }
        process = StartCoroutine(ProcessingState());

        base.Enter();
    }
    public override void Exit()
    {
        base.Exit();
    }
    #endregion
    #endregion

    #region EventHandlers
    public void OnAnimEnd(int type)
    {
        if(type == 0)
        {
            if(owner.modeType == 0)
                owner.stateMachine.ChangeState<PlayerIdleState>();
            else if(owner.modeType == 1)
                owner.stateMachine.ChangeState<PlayerBuildModeState>();
        }
    }
    #endregion

    #region Coroutines
    private IEnumerator ProcessingState()
    {
        owner.gameObject.layer = playerAvoidMask;
        yield return null;
        yield return new WaitForSeconds(invincibleTime);
        owner.gameObject.layer = playerMask;
    }

    #endregion

    #region MonoBehaviour
    protected override void Awake() 
    {
        base.Awake();
        playerMask = LayerMask.NameToLayer("Player");
        playerAvoidMask = LayerMask.NameToLayer("Player_Avoid");
    }
    #endregion
}
