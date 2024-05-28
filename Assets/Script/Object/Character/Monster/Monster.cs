
using UnityEngine;

public class Monster : Unit
{
    #region Properties
    #region Private
    #endregion
    #region Protected
    protected Vector2 _spawnPoint = Vector2.zero;
    protected GameObject _target;
    #endregion
    #region Public
    public float detectRadius;
    public DirMoveAction dirMove;
    public PointMoveAction pointMove;
    public AttackAction[] attackActions;
    public AI ai;
    public Vector3 spawnPoint
    {
        get { return _spawnPoint; }
        protected set { _spawnPoint = value; }
    }
    public GameObject target
    {
        get { return _target; }
        set { _target = value; }
    }
    #endregion
    #region Events
    #endregion
    #endregion

    #region Constructor
    public Monster() : base()
    {
        AddStat(EStat.DetectRadius, 0.0f);
    }
    #endregion

    #region Methods
    #region Private
    #endregion
    #region Protected
    protected override void Initialize()
    {
        base.Initialize();
        this[EStat.DetectRadius] = detectRadius;
        spawnPoint = transform.position;

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
