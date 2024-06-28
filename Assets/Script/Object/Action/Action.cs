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
    ///<summary>
    ///이 액션이 플레이어나 몬스터의 조작을 막는지 막지 않는지에 대한 bool값
    ///</summary>
    public bool fireAndForget
    {
        get { return _fireAndForget; }
        protected set { _fireAndForget = value; }
    }
    ///<summary>
    ///이 액션이 지금현재 사용 가능한지 아닌지에 대한 bool값
    ///</summary>
    public bool available
    {
        get { return _available; }
        protected set { _available = value; }
    }
    ///<summary>
    ///이 액션의 우선순위
    ///</summary>
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
    ///<summary>
    ///몬스터에서 사용하는 이 액션이 사용가능한 거리에 대한 float값
    ///</summary>
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
    ///<summary>
    ///클래스 초기화, Start에서 실행
    ///</summary>
    protected abstract void Initialize();
    ///<summary>
    ///이 액션이 끝났을때 실행되어 이벤트를 invoke 시켜주는 함수
    ///</summary>
    protected virtual void ActionEnd()
    {
        OnActionEndEvent?.Invoke();
    }
    #endregion
    #region Public
    ///<summary>
    ///외부에서 이 액션을 실행시키는 함수
    ///</summary>
    public virtual void Activate(Vector2 pos)
    {
        available = false;
        StartCoroutine(CoolTimeChecking());
    }
    ///<summary>
    ///외부에서 이 액션을 비활성화 시키는 함수
    ///</summary>
    public abstract void Deactivate();
    ///<summary>
    ///인터페이스로 우선순위를 가져오기 위한 함수
    ///</summary>
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
    ///<summary>
    ///쿨타임을 위한 코루틴
    ///</summary>
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
