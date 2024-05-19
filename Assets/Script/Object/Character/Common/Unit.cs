using Unity.VisualScripting;
using UnityEngine;

public abstract class Unit : Stat
{
    #region Properties
    #region Private
    #endregion
    #region Protected
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected float ATK;
    [SerializeField] protected float ATKSpeed;
    protected StateMachine stateMachine;
    #endregion
    #region Public
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
        base.Initialize();
        AddStat(EStat.MoveSpeed, moveSpeed);
        AddStat(EStat.ATK, ATK);
        AddStat(EStat.ATKSpeed, ATKSpeed);

        if (stateMachine == null)
            stateMachine = transform.AddComponent<StateMachine>();
    }
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
