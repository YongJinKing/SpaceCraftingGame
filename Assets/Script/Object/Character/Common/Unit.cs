using Unity.VisualScripting;
using UnityEngine;

public abstract class Unit : Stat
{
    #region Properties
    #region Private
    #endregion
    #region Protected
    protected StateMachine _stateMachine;
    protected Action _activatedAction = null;
    #endregion
    #region Public
    public float moveSpeed;
    public float ATK;
    public float ATKSpeed;
    public StateMachine stateMachine 
    {
        get { return _stateMachine; } 
    }
    public Action activatedAction
    {
        get { return _activatedAction; }
        set { _activatedAction = value; }
    }
    #endregion
    #region Events
    #endregion
    #endregion

    #region Constructor
    public Unit() : base()
    {
        AddStat(EStat.MoveSpeed, 0);
        AddStat(EStat.ATK, 0);
        AddStat(EStat.ATKSpeed, 0);
    }
    #endregion

    #region Methods
    #region Private
    #endregion
    #region Protected
    protected override void Initialize()
    {
        base.Initialize();
        this[EStat.MoveSpeed] = moveSpeed;
        this[EStat.ATK] = ATK;
        this[EStat.ATKSpeed] = ATKSpeed;

        if (_stateMachine == null)
            _stateMachine = transform.AddComponent<StateMachine>();
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
