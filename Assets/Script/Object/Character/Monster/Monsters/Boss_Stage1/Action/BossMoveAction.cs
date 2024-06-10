using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class BossMoveAction : Action
{
    #region Properties
    #region Private
    #endregion
    #region Protected
    #endregion
    #region Public
    #endregion
    #region Events
    protected UnityEvent<Vector2> moveToPosEvent = new UnityEvent<Vector2>();
    protected UnityEvent stopMoveEvent = new UnityEvent();
    #endregion
    #endregion

    #region Constructor
    #endregion

    #region Methods
    #region Private
    #endregion
    #region Protected
    protected override void Initialize()
    {
        BossMovement movement = GetComponentInParent<BossMovement>();
        movement.moveEndEvent.AddListener(OnMoveEnd);

        moveToPosEvent.AddListener(movement.OnMoveToPos);
        stopMoveEvent.AddListener(movement.OnStop);
    }

    protected virtual void OnMoveEnd()
    {
        ActionEnd();
    }
    #endregion
    #region Public
    #endregion
    #endregion

    #region EventHandlers
    #endregion

    #region Coroutines
    #endregion

    #region MonoBehaviour
    protected virtual void OnDestroy()
    {
        UnitMovement movement = GetComponentInParent<UnitMovement>();
        if (movement != null)
        {
            movement.moveEndEvent.RemoveListener(OnMoveEnd);
        }

        moveToPosEvent.RemoveAllListeners();
        stopMoveEvent.RemoveAllListeners();
    }
    #endregion
}
