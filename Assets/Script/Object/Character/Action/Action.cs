using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Action : MonoBehaviour
{
    #region Properties
    #region Private
    #endregion
    #region Protected
    protected bool _fireAndForget;
    protected bool _available = true;
    #endregion
    #region Public
    public bool fireAndForget
    {
        get { return _fireAndForget; }
        set { _fireAndForget = value; }
    }
    public bool available
    {
        get { return _available; }
        protected set { _available = value; }
    }
    #endregion
    #region Events
    public UnityEvent OnActionEndEvent = new UnityEvent();
    #endregion
    #endregion

    #region Constructor
    #endregion

    #region Methods
    #region Private
    #endregion
    #region Protected
    protected abstract void Initialize();
    protected virtual void ActionEnd()
    {
        OnActionEndEvent?.Invoke();
    }
    #endregion
    #region Public
    public abstract void Activate(Vector2 pos);
    public abstract void Deactivate();
    #endregion
    #endregion

    #region EventHandlers
    #endregion

    #region Coroutines
    #endregion

    #region MonoBehaviour
    protected virtual void Start()
    {
        Initialize();
    }
    #endregion
}
