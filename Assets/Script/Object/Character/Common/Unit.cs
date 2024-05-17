using UnityEngine;

public abstract class Unit : Stat
{
    #region Properties
    #region Private
    #endregion
    #region Protected
    [SerializeField] protected float moveSpeed;
    #endregion
    #region Public
    #endregion
    #region Events
    #endregion
    #endregion

    #region Constructor
    public Unit() : base() 
    {
        AddStat(EStat.MoveSpeed, moveSpeed);
    }
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
