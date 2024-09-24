using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonAttackAction : AttackAction
{
    #region Properties
    #region Private
    private Vector2 pos;
    #endregion
    #region Protected
    //���� �ִϸ��̼� Ÿ�� ��ȣ
    [SerializeField] protected int _animationType;

    #region Animation Oriented
    // AnimationEvent�� ���ؼ� HitBox�� �����Ǵ°� ������ bool ��
    [SerializeField] protected bool _animationOriented = false;
    protected bool _isActivated = false;
    #endregion

    #region Time Oriented
    [SerializeField] protected float _activatingHitBoxTime;
    //ĵ�� ���� �ð�
    [SerializeField] protected float _cancelableTime;
    #endregion


    #endregion
    #region Public
    public bool animationOriented
    {
        get { return _animationOriented; }
        set { _animationOriented = value; }
    }
    public int animationType
    {
        get { return _animationType; }
        set { _animationType = value; }
    }

    public float activatingHitBoxTime 
    {
        get { return _activatingHitBoxTime; }
        set { _activatingHitBoxTime = value; }
    }

    public float cancelableTime
    {
        get { return _cancelableTime; }
        set { _cancelableTime = value; }
    }
    #endregion
    #region Events
    #endregion
    #endregion

    #region Constructor
    public CommonAttackAction() { fireAndForget = true; }
    #endregion

    #region Methods
    #region Private
    #endregion
    #region Protected
    protected override void Initialize()
    {
        base.Initialize();
        if (_animationOriented)
        {
            //�ִϸ��̼� �̺�Ʈ�� ����ϴ� �ڵ� �ʿ�
            UnitAnimationController animCtrl = GetComponentInParent<Stat>().GetComponentInChildren<UnitAnimationController>();
            if (animCtrl != null) 
            {
                animCtrl.attackAnimEvent.AddListener(OnAttackAnimation);
                animCtrl.animEndEvent.AddListener(OnAttackEndAnimation);
            }
        }
    }
    #endregion
    #region Public
    public override void Activate(Vector2 pos)
    {
        if (!gameObject.activeInHierarchy)
        {
            return;
        }

        base.Activate(pos);
        _isActivated = true;
        if (_animationOriented)
        {
            this.pos = pos;
        }
        else
        {
            StartCoroutine(ActivatingHitBox(pos));
            StartCoroutine(CancelableTimeChecking());
        }
    }
    public override void Deactivate()
    {
        if (!gameObject.activeInHierarchy)
        {
            return;
        }

        _isActivated = false;
        base.Deactivate();
    }
    #endregion
    #endregion

    #region EventHandlers
    protected void OnAttackAnimation(int type)
    {
        if (!gameObject.activeInHierarchy)
        {
            return;
        }

        if (_animationOriented)
        {
            ActivateHitBoxes(pos);

            switch (type)
            {
                case 0:
                    break;
                case 1:
                    break;
                case 2:
                    break;
            }
        }
    }
    protected void OnAttackEndAnimation(int type)
    {
        if (!gameObject.activeInHierarchy)
        {
            return;
        }

        if (_animationOriented)
            ActionEnd();
    }
    protected override void OnHitBoxEnd()
    {
    }
    #endregion

    #region Coroutines
    protected IEnumerator ActivatingHitBox(Vector2 pos)
    {
        if(_activatingHitBoxTime > 0.0f)
            yield return new WaitForSeconds(_activatingHitBoxTime);
        ActivateHitBoxes(pos);
    }
    protected IEnumerator CancelableTimeChecking()
    {
        if (_cancelableTime > 0.0f)
            yield return new WaitForSeconds(_cancelableTime);
        ActionEnd();
    }
    #endregion

    #region MonoBehaviour
    #endregion
}