using UnityEngine;
using UnityEngine.Events;

public abstract class AttackAction : Action
{
    #region Properties
    #region Private
    #endregion
    #region Protected
    [SerializeField]protected LayerMask _targetMask;
    #endregion
    #region Public
    public HitBox[] hitBoxes;
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
    protected override void Initialize()
    {
        for (int i = 0; i < hitBoxes.Length; ++i)
        {
            hitBoxes[i].OnDurationEndEvent.AddListener(OnHitBoxEnd);
        }
    }
    #endregion
    #region Public
    #endregion
    #endregion

    #region EventHandlers
    public virtual void OnHitBoxEnd()
    {
        ActionEnd();
    }
    #endregion

    #region Coroutines
    #endregion

    #region MonoBehaviour
    protected virtual void OnDestroy()
    {
        for (int i = 0; i < hitBoxes.Length; ++i)
        {
            hitBoxes[i].OnDurationEndEvent.RemoveListener(OnHitBoxEnd);
        }
    }
    #endregion
}
