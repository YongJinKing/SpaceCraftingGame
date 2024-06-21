using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Action : MonoBehaviour, IGetPriority
{
    #region Properties
    #region Private
    #endregion
    #region Protected
    protected bool _fireAndForget = false;
    protected bool _available = true;
    //for AI
    [SerializeField]protected float _priority = 0.0f;
    [SerializeField]protected float _coolTime = -1;
    [SerializeField]protected float _activeRadius = 1.0f;

    #endregion
    #region Public
    //for spine animation
    public SkeletonAnimation skeletonAnimation;
    public AnimationReferenceAsset[] animClip;
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
    public float activeRadius
    {
        get { return _activeRadius; }
        protected set { _activeRadius = value; }
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
    public virtual float GetPriority()
    {
        return priority;
    }
    public void AsyncAnimation(int idx, bool loop)
    {
        skeletonAnimation.state.SetAnimation(0, animClip[idx], loop).TimeScale = 1;
        skeletonAnimation.loop = loop;
        skeletonAnimation.timeScale = 1;
    }
    #endregion
    #endregion

    #region EventHandlers
    #endregion

    #region Coroutines
    protected IEnumerator CoolTimeChecking()
    {
        if (available && coolTime < 0)
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
