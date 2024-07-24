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
        }
    }
    #endregion
    #region Public
    public override void Activate(Vector2 pos)
    {
        base.Activate(pos);
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
        base.Deactivate();
    }
    #endregion
    #endregion

    #region EventHandlers
    public void OnAttackAnimation()
    {
        if(_animationOriented)
            ActivateHitBoxes(pos);
    }
    public void OnAttackEndAnimation()
    {
        if(_animationOriented)
            ActionEnd();
    }
    public override void OnHitBoxEnd()
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