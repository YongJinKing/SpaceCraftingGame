using UnityEngine;

//플레이어는 공격 애니메이션을 트리거하지 않고 Action을 통해서 간접적으로 트리거
//플레이어는 이동, idle 애니메이션을 트리거

public class Player : Unit
{
    #region Properties
    #region Private
    #endregion
    #region Protected
    #endregion
    #region Public
    public MoveAction moveAction;
    public AttackAction attackAction;
    public Transform graphicTransform;
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
    protected override void OnDead()
    {
        stateMachine.ChangeState<PlayerDeadState>();
    }
    protected override void Initialize()
    {
        base.Initialize();
        stateMachine.ChangeState<PlayerInitState>();
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
