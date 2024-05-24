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
    protected bool _fireAndForget = false;
    protected bool _available = true;
    //for AI
    protected float _priority = 0.0f;
    [SerializeField]protected float _coolTime = -1;
    #endregion
    #region Public
    public bool fireAndForget
    {
        get { return _fireAndForget; }
        protected set { _fireAndForget = value; }
    }
    public bool available
    {
        get { return _available; }
        protected set { _available = value; }
    }
    public float priority
    {
        get { return _priority; }
        protected set { _priority = value; }
    }
    public float coolTime
    {
        get { return _coolTime; }
        set { _coolTime = value; }
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
    public virtual void Activate(Vector2 pos)
    {
        available = false;
        StartCoroutine(CoolTimeChecking());
    }
    public abstract void Deactivate();
    #endregion
    #endregion

    #region EventHandlers
    #endregion

    #region Coroutines
    protected IEnumerator CoolTimeChecking()
    {
        if (available || coolTime < 0)
        {
            yield break;
        }

        float remainCoolTime = coolTime;
        while(remainCoolTime > 0) 
        {
            remainCoolTime -= Time.deltaTime;
            yield return null;
        }
        available = true;
    }
    #endregion

    #region MonoBehaviour
    protected virtual void Start()
    {
        Initialize();
    }
    #endregion
}
