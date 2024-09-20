using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BossDamageAffects : BaseAffect
{
    #region Properties
    #region Private
    #endregion
    #region Protected
    [SerializeField] protected float _power;
    [SerializeField] float knockBackPower;
    [SerializeField] float knockBackTime;
    #endregion
    #region Public
    public float power
    {
        get { return _power; }
        set { _power = value; }
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
    public override void OnActivate(Collider2D target, Vector2 hitWorldPos)
    {
        //Debug.Log($"DamageAffect myStat[EStat.ATK] = {myStat[EStat.ATK]}, GetRawStat = {myStat.GetRawStat(EStat.ATK)}");

        IDamage damage = target.GetComponent<IDamage>();

        if (damage != null)
        {
            float temp = 0;
            if (myStat != null)
            {
                temp = myStat[EStat.ATK] * power;
            }
            Debug.Log("TakeDamage To " + target.name + " with " + temp.ToString());
            damage.TakeDamage(temp);
            StartCoroutine(KnockBackTarget(target.transform, knockBackTime));
        }
    }
    #endregion
    #endregion

    #region EventHandlers
    #endregion

    #region Coroutines
    IEnumerator KnockBackTarget(Transform target, float _time)
    {
        // �����ڿ� ��� ������ ���� ���
        Vector2 knockBackDirection = (target.transform.position - myStat.transform.position).normalized;

        Rigidbody2D targetRb = target.GetComponent<Rigidbody2D>();
        // Knockback �������� Rigidbody2D�� �� ����
        targetRb.velocity = knockBackDirection * knockBackPower;

        yield return new WaitForSeconds(_time);

        // �ð��� ������ velocity�� 0���� �����ؼ� ���� ���·� ����
        targetRb.velocity = Vector2.zero;
    }
    #endregion

    #region MonoBehaviour
    #endregion
}
