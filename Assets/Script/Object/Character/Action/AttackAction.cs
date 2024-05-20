using UnityEngine;

public abstract class AttackAction<T> : Action<T>
{
    #region Properties
    #region Private
    #endregion
    #region Protected
    protected LayerMask _targetMask;
    #endregion
    #region Public
    public LayerMask targetMask
    {
        get { return _targetMask; }
        set { _targetMask = value; }
    }
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
    #endregion
    #region Public
    #endregion
    #endregion

    #region EventHandlers
    #endregion

    #region Coroutines
    #endregion

    #region MonoBehaviour
    #endregion
}
