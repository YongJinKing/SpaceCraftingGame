using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.Events;

public abstract class MoveAction : Action<Vector2>
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
    #endregion
    #endregion

    #region Constructor
    #endregion

    #region Methods
    #region Private
    #endregion
    #region Protected
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
        moveToPosEvent.RemoveAllListeners();
        moveToDirEvent.RemoveAllListeners();
    }
    #endregion
}
