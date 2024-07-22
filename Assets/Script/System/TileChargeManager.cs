using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Class For Tile Charge
/// 물체의 Collider2D의 크기(Bound를 씀)에 따라 TileManager의 availablePlace의 available을 조절
/// </summary>
public class TileChargeManager : MonoBehaviour
{
    #region Properties
    #region Private
    private HashSet<Vector3Int> _previousTilePos = new HashSet<Vector3Int>();
    private Collider2D myCol;
    private TileManager tileManager;
    private Coroutine charging;
    #endregion
    #region Protected
    [SerializeField]protected float updateFrequency = 1.0f;
    #endregion
    #region Public
    public HashSet<Vector3Int> previousTilePos
    {
        get { return _previousTilePos; }
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
    protected void Initialize()
    {
        myCol = GetComponent<Collider2D>();
        tileManager = TileManager.Instance;
        Stat stat = GetComponent<Stat>();
        if (stat != null) 
        {
            stat.deadEvent.AddListener(OnDead);
        }
        charging = StartCoroutine(UpdatingTile());
    }

    protected void UnChargeTiles()
    {
        StopAllCoroutines();
        foreach (Vector3Int coorPos in _previousTilePos)
        {
            if (tileManager.HasTile(coorPos))
            {
                if (tileManager.availablePlaces[coorPos].Object == null)
                {
                    tileManager.availablePlaces[coorPos].available = true;
                }
            }
        }
        _previousTilePos.Clear();

        Stat stat = GetComponent<Stat>();
        if (stat != null)
        {
            stat.deadEvent.RemoveListener(OnDead);
        }
    }
    #endregion
    #region Public
    #endregion
    #endregion

    #region EventHandlers
    public void OnDead()
    {
        UnChargeTiles();
    }
    #endregion

    #region Coroutines
    protected IEnumerator UpdatingTile()
    {
        yield return null;

        Vector3Int minCoor;
        Vector3Int maxCoor;
        HashSet<Vector3Int> tempSet = new HashSet<Vector3Int>();

        while (true)
        {
            tempSet.Clear();
            minCoor = tileManager.GetTileCoordinates(myCol.bounds.center - myCol.bounds.extents);
            maxCoor = tileManager.GetTileCoordinates(myCol.bounds.center + myCol.bounds.extents);

            //타일을 차지
            for (int x = minCoor.x; x <= maxCoor.x; ++x)
            {
                for (int y = minCoor.y; y <= maxCoor.y; y++)
                {
                    Vector3Int tilePosition = new Vector3Int(x, y, 0);

                    if (tileManager.HasTile(tilePosition))
                    {
                        if (tileManager.availablePlaces[tilePosition].available || _previousTilePos.Contains(tilePosition))
                        {
                            tileManager.RemopvePlace(tilePosition);
                            tempSet.Add(tilePosition);
                        }
                    }
                }
            }

            //차지를 품
            foreach (Vector3Int coorPos in _previousTilePos)
            {
                if (!tempSet.Contains(coorPos))
                {
                    if (tileManager.availablePlaces[coorPos].Object == null)
                    {
                        tileManager.availablePlaces[coorPos].available = true;
                    }
                }
            }
            
            _previousTilePos = tempSet.ToHashSet();

            //updateFrequency마다 반복
            yield return new WaitForSeconds(updateFrequency);
        }
    }
    #endregion

    #region MonoBehaviour
    protected void Start()
    {
        Initialize();
    }

    protected void OnDestroy()
    {
        UnChargeTiles();
    }
    #endregion
}