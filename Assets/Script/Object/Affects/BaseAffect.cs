using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class BaseAffect : MonoBehaviour
{
    #region Properties
    #region Private
    #endregion
    #region Protected
    [SerializeField]protected LayerMask _targetMask;
    protected Stat myStat = null;
    #endregion
    #region Public
    public LayerMask targetMask
    {
        get { return _targetMask; } 
        set { _targetMask = value; }
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
    protected virtual void Initialize()
    {
        if (myStat == null)
        {
            myStat = GetComponentInParent<Stat>();
        }

        StartCoroutine(DelaiedInit());
    }
    #endregion
    #region Public
    #endregion
    #endregion

    #region EventHandlers
    public abstract void OnActivate(Collider2D target, Vector2 hitWorldPos);
    #endregion

    #region Coroutines
    protected virtual IEnumerator DelaiedInit()
    {
        yield return new WaitForEndOfFrame();
        HitBox hitbox = GetComponentInParent<HitBox>();
        if (hitbox != null) 
        {
            if (targetMask != 0)
            {
                for (int i = 0; i < 32; ++i)
                {
                    if ((targetMask & (1 << i)) != 0)
                    {
                        hitbox.RegisterToHitEvent((1 << i), OnActivate);
                    }
                }
            }
        }
    }
    #endregion

    #region MonoBehaviour
    protected virtual void Start()
    {
        Initialize();
    }
    protected virtual void OnDestroy()
    {
        HitBox hitbox = GetComponentInParent<HitBox>();
        if (hitbox != null)
        {
            if (targetMask != 0)
            {
                for (int i = 0; i < 32; ++i)
                {
                    if ((targetMask & (1 << i)) != 0)
                    {
                        hitbox.DisregisterToHitEvent((1 << i), OnActivate);
                    }
                }
            }
        }
    }
    #endregion
}
