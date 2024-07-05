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
    [SerializeField] float _activeDuration = 0.0f;
    #endregion
    #region Public
    /// <summary>
    /// �ٸ� �������� ĵ���� �����Ҷ������� �ð�
    /// </summary>
    public float activeDuration
    {
        get { return _activeDuration; }
        set { _activeDuration = value; }
    }
    #endregion
    #region Events
    #endregion
    #endregion

    #region Constructor
    WeaponAttackAction() { fireAndForget = true; }
    #endregion

    #region Methods
    #region Private
    #endregion
    #region Protected
    #endregion
    #region Public
    public override void Activate(Vector2 pos)
    {
        base.Activate(pos);

        ActivateHitBoxes(pos);
        StartCoroutine(ActiveDurationChecking());
    }
    #endregion
    #endregion

    #region EventHandlers
    public override void OnHitBoxEnd()
    {
    }
    #endregion

    #region Coroutines
    protected IEnumerator ActiveDurationChecking()
    {
        if(activeDuration > 0.0f)
            yield return new WaitForSeconds(activeDuration);
        ActionEnd();
    }
    #endregion

    #region MonoBehaviour
    #endregion
}
