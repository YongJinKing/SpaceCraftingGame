using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UnitAnimationController : MonoBehaviour
{
    #region Properties
    #region Private
    private Animator _anim;
    #endregion
    #region Protected
    protected Animator anim
    {
        get
        {
            if (_anim == null)
            {
                _anim = GetComponent<Animator>();
            }
            return _anim;
        }
    }
    #endregion
    #region Public
    public UnityEvent<int> animEndEvent = new UnityEvent<int>();
    public UnityEvent<int> attackAnimEvent = new UnityEvent<int>();
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
    #endregion
    #endregion

    #region EventHandlers
    #endregion

    #region Coroutines
    #endregion

    #region MonoBehaviour
    #endregion
}
