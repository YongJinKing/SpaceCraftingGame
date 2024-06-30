using System.Collections;
using UnityEngine;
using UnityEngine.Events;

//�� �ִϸ��̼ǰ� ĳ���� �ִϸ��̼� ��Ʈ�ѷ��� �и��� �ʿ��ϴ�.
//���� ��������� ĳ���Ϳ��� ���� �����ϱ� �����̴�. �Ф̤Ф�

//1. Action�� �ߵ��� �ִϸ��̼� Ʈ����
//2. �ִϸ��̼� �ߵ� ���� Ư�� �ִϸ��̼ǿ��� �̺�Ʈ �߻�
//      ���� ��� �ѱ��� ��½ �ϴ� ������ Ʈ����
//3. �̺�Ʈ �߻��� ��Ʈ�ڽ� ����
//4. �ִϸ��̼ǿ��� ĵ�� ������ ������ ���޽� �̺�Ʈ �߻�
//5. Action�� ���� ��Ʈ�ڽ��� Deactivate �ϰų� �׳� Action�� �����ٰ� �̺�Ʈ �߻�
//6. �÷��̾�� �ٽ� ������� ���ƿ�


//1-1. �ִϸ��̼��� ���� ��� ���� �ð��� �����Ŀ� Action�� �׳� ����

public class WeaponAttackAction : AttackAction
{
    #region Properties
    #region Private
    #endregion
    #region Protected
    [SerializeField] protected int componentIndex;
    [SerializeField] protected int animationType;
    [SerializeField] protected float actionDuration;
    /// <summary>
    /// ���� componentIndex �� animationType ���� �ϳ��� �����̸� false
    /// </summary>
    protected bool isAnimationed
    {
        get { return componentIndex > 0 || animationType > 0; }
    }
    #endregion
    #region Public
    #endregion
    #region Events
    /// <summary>
    /// �ִϸ��̼��� Ʈ���� ���ִ� �̺�Ʈ
    /// ù int�� ������Ʈ index, �ι�° int�� �ִϸ��̼� ��ȣ
    /// </summary>
    public UnityEvent<int, int> animationTriggerEvent = new UnityEvent<int, int>();
    public UnityEvent animationEndEvent = new UnityEvent();
    #endregion
    #endregion

    #region Constructor
    WeaponAttackAction() { fireAndForget = true; }
    #endregion

    #region Methods
    #region Private
    #endregion
    #region Protected
    protected override void Initialize()
    {
        base.Initialize();
        //�÷��̾��� �ִϸ��̼ǰ� ���� �ִϸ��̼��� �и� �Ǿ� �־����
        //
        //���⿡ �ִϸ��̼� ��Ʈ�ѷ��� �̺�Ʈ�� �� Ŭ������ �ڵ鷯�� ���
        //���� �� Ŭ������ �̺�Ʈ�� �ִϸ��̼� ��Ʈ�ѷ��� �ڵ鷯�� ���
        //�ִϸ��̼��� �ʿ����� ������ ������� ����
    }
    #endregion
    #region Public
    public override void Activate(Vector2 pos)
    {
        base.Activate(pos);

        if (!isAnimationed) 
        {
            //�ִϸ��̼����� ��� ���� �ʴ°��(�ִϸ��̼��� ���°��)
            ActivateHitBoxes(pos);
            //���⿡ ���� ȸ����Ű�� �Լ�
            //���⿡ ���� ��������Ʈ�� ��Ȱ��ȭ�� �Լ�
            //���⿡ 
        }
        else
        {
            animationTriggerEvent?.Invoke(componentIndex, animationType);
        }
    }
    #endregion
    #endregion

    #region EventHandlers
    public override void OnHitBoxEnd()
    {
    }
    #endregion

    #region Coroutines
    #endregion

    #region MonoBehaviour
    #endregion
}
