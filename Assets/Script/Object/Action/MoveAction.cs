using UnityEngine;
using UnityEngine.Events;

public abstract class MoveAction : Action
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
    protected UnityEvent<Vector2> moveToDirEvent = new UnityEvent<Vector2>();
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
        UnitMovement movement = GetComponentInParent<UnitMovement>();
        movement.moveEndEvent.AddListener(OnMoveEnd);

        moveToPosEvent.AddListener(movement.OnMoveToPos);
        moveToDirEvent.AddListener(movement.OnMoveToDir);
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
        if(movement != null)
        {
            movement.moveEndEvent.RemoveListener(OnMoveEnd);
        }

        moveToPosEvent.RemoveAllListeners();
        moveToDirEvent.RemoveAllListeners();
        stopMoveEvent.RemoveAllListeners();
    }
    #endregion
}
