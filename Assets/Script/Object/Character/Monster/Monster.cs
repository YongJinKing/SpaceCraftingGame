using UnityEngine;

public class Monster : Unit
{
    #region Properties
    #region Private
    private Vector3Int previousTilePos;
    #endregion
    #region Protected
    protected Vector2 _spawnPoint = Vector2.zero;
    protected GameObject _target;
    #endregion
    #region Public
    public float detectRadius;
    public LayerMask targetMask = 1 << 17;
    public DirMoveAction dirMove;
    public PointMoveAction pointMove;
    public AttackAction[] attackActions;
    public AI ai;
    public Vector2 spawnPoint
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
    protected void OnTriggerStay2D(Collider2D collision)
    {
        //Layer Check
        if ((collision.gameObject.layer & LayerMask.NameToLayer("Ground")) != 0)
        {
            Vector3Int tempCoor = TileManager.Instance.GetTileCoordinates(collision.ClosestPoint(transform.position));
            if (previousTilePos != tempCoor)
            {
                //Debug.Log($"Stay : {tempCoor}");

                if(TileManager.Instance.HasTile(previousTilePos))
                    TileManager.Instance.availablePlaces[previousTilePos].available = true;

                previousTilePos = tempCoor;
                if(TileManager.Instance.HasTile(tempCoor))
                    TileManager.Instance.RemopvePlace(tempCoor);
            }
        }
    }

    protected void OnDestroy()
    {
        if (TileManager.Instance.HasTile(previousTilePos))
        {
            if (!TileManager.Instance.availablePlaces[previousTilePos].available)
            {
                TileManager.Instance.availablePlaces[previousTilePos].available = true;
            }
        }
    }
    #endregion
}
