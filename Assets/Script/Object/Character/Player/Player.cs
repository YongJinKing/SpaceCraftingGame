

using UnityEngine;

public class Player : Unit
{
    #region Properties
    #region Private
    #endregion
    #region Protected
    #endregion
    #region Public
    public MoveAction moveAction;
    public VectorAttackAction attackAction;
    #endregion
    #region Events
    #endregion
    #endregion

    #region Constructor
    public Player() : base()
    {
    }
    #endregion

    #region Methods
    #region Private
    #endregion
    #region Protected
    protected override void Initialize()
    {
        base.Initialize();
        stateMachine.ChangeState<PlayerInitState>();
    }
    #endregion
    #region Public
    public override void TakeDamage(float damage)
    {
        this[EStat.HP] -= damage;
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
