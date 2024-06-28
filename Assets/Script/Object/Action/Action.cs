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
    ///�� �׼��� �÷��̾ ������ ������ ������ ���� �ʴ����� ���� bool��
    ///</summary>
    public bool fireAndForget
    {
        get { return _fireAndForget; }
        protected set { _fireAndForget = value; }
    }
    ///<summary>
    ///�� �׼��� �������� ��� �������� �ƴ����� ���� bool��
    ///</summary>
    public bool available
    {
        get { return _available; }
        protected set { _available = value; }
    }
    ///<summary>
    ///�� �׼��� �켱����
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
    ///���Ϳ��� ����ϴ� �� �׼��� ��밡���� �Ÿ��� ���� float��
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
    ///Ŭ���� �ʱ�ȭ, Start���� ����
    ///</summary>
    protected abstract void Initialize();
    ///<summary>
    ///�� �׼��� �������� ����Ǿ� �̺�Ʈ�� invoke �����ִ� �Լ�
    ///</summary>
    protected virtual void ActionEnd()
    {
        OnActionEndEvent?.Invoke();
    }
    #endregion
    #region Public
    ///<summary>
    ///�ܺο��� �� �׼��� �����Ű�� �Լ�
    ///</summary>
    public virtual void Activate(Vector2 pos)
    {
        available = false;
        StartCoroutine(CoolTimeChecking());
    }
    ///<summary>
    ///�ܺο��� �� �׼��� ��Ȱ��ȭ ��Ű�� �Լ�
    ///</summary>
    public abstract void Deactivate();
    ///<summary>
    ///�������̽��� �켱������ �������� ���� �Լ�
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
    ///��Ÿ���� ���� �ڷ�ƾ
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
