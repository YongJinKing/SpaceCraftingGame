using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveMonster : Monster
{
    #region Properties
    #region Private
    private bool isRight = true;
    #endregion
    #region Protected
    #endregion
    #region Public
    public Transform graphicTransform;
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
    protected override void OnDead()
    {
        base.OnDead();
        GetComponent<UnitMovement>().onMovingEvent.RemoveListener(OnMove);
        stateMachine.ChangeState<WaveMonsterDeadState>();
    }
    protected override void Initialize()
    {
        base.Initialize();
        animator = GetComponentInChildren<Animator>();
        GetComponent<UnitMovement>().onMovingEvent.AddListener(OnMove);
        stateMachine.ChangeState<WaveMonsterInitState>();
    }
    #endregion
    #region Public
    #endregion
    #endregion

    #region EventHandlers
    public void OnMove(Vector2 dir)
    {
        if(dir.x >= 0 ^ isRight && graphicTransform != null)
        {
            graphicTransform.localScale = new Vector3(-graphicTransform.localScale.x, graphicTransform.localScale.y, graphicTransform.localScale.z);
            isRight = !isRight;
        }
    }
    #endregion

    #region Coroutines
    #endregion

    #region MonoBehaviour
    #endregion
}
