
using UnityEngine;

public class Monster : Unit
{
    #region Properties
    #region Private
    #endregion
    #region Protected

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
        stateMachine.ChangeState<MonsterInitState>();
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
