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
        // 공격자와 대상 사이의 방향 계산
        Vector2 knockBackDirection = (target.transform.position - myStat.transform.position).normalized;

        Rigidbody2D targetRb = target.GetComponent<Rigidbody2D>();
        // Knockback 방향으로 Rigidbody2D에 힘 적용
        targetRb.velocity = knockBackDirection * knockBackPower;

        yield return new WaitForSeconds(_time);

        // 시간이 지나면 velocity를 0으로 설정해서 정상 상태로 복귀
        targetRb.velocity = Vector2.zero;
    }
    #endregion

    #region MonoBehaviour
    #endregion
}
