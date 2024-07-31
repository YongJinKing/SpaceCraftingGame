using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovableActionState : BossState
{
    #region Properties
    #region Private
    float moveSpeed;
    float moveTimer;
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
        if (owner.activatedAction != null)
        {
            owner.activatedAction.OnActionEndEvent.AddListener(OnActionEnd);
        }
    }
    protected override void RemoveListeners()
    {
        base.RemoveListeners();
        if (owner.activatedAction != null)
        {
            owner.activatedAction.OnActionEndEvent.RemoveListener(OnActionEnd);
        }
        owner.activatedAction = null;
    }
    #endregion
    #region Public
    public override void Enter()
    {
        base.Enter();
        moveTimer = 5f;
        moveSpeed = owner.moveSpeed * 0.6f;
        StartFollowing();
    }
    public override void Exit()
    {
        owner.activatedAction.Deactivate();
        base.Exit();
        StopAllCoroutines();
    }

    public void StartFollowing()
    {
        StartCoroutine(FollowingTarget());
    }
    #endregion
    #endregion

    #region EventHandlers
    public void OnActionEnd()
    {
        if (owner.target != null)
            owner.stateMachine.ChangeState<BossAlertState>();
        else
            owner.stateMachine.ChangeState<BossIdleState>();
    }
    #endregion

    #region Coroutines
    protected IEnumerator FollowingTarget()
    {
        yield return new WaitForSeconds(1f);
        //보스의 이동속도의 80%(느리게 따라갈거면) 혹은 120%(빠르게 따라갈거면)로 플레이어를 따라감
        while(moveTimer >= 0.0f)
        {
            moveTimer -= Time.deltaTime;
            Vector2 dir = owner.target.transform.position - transform.position;
            dir.Normalize();
            transform.Translate(dir * moveSpeed * Time.deltaTime, Space.World);
            yield return null;
        }

        yield return null;
        
    }
    #endregion

    #region MonoBehaviour
    #endregion
}
