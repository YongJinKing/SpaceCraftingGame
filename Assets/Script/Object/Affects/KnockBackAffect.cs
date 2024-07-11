using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBackAffect : BaseAffect
{
    #region Properties
    #region Private
    #endregion
    #region Protected
    [SerializeField] protected float _knockBackPower;
    #endregion
    #region Public
    public float knockBackPower
    {
        get { return _knockBackPower; }
        set { _knockBackPower = value; }
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
        Rigidbody2D rb = target.attachedRigidbody;
        if (rb == null) return;

        Vector3 dir = target.transform.position - transform.position;
        dir.Normalize();

        rb.AddForce(dir * knockBackPower, ForceMode2D.Impulse);
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
