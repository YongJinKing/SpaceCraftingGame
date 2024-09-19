using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageAffect : BaseAffect
{
    #region Properties
    #region Private
    #endregion
    #region Protected
    [SerializeField] protected float _power;
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
        }
    }
    #endregion
    #endregion

    #region EventHandlers
    #endregion

    #region Coroutines
    #endregion

    #region MonoBehaviour
    #endregion
}
