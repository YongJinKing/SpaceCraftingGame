using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class For Tile Charge
/// ��ü�� Collider2D�� ũ��(Bound�� ��)�� ���� TileManager�� availablePlace�� available�� ����
/// </summary>
public class TileChargeManager : MonoBehaviour
{
    #region Properties
    #region Private
    private HashSet<Vector3Int> _previousTilePos = new HashSet<Vector3Int>();
    private Collider2D myCol;
    private TileManager tileManager;
    #endregion
    #region Protected
    [SerializeField]protected float updateFrequency = 0.1f;
    #endregion
    #region Public
    public HashSet<Vector3Int> previousTilePos
    {
        get { return previousTilePos; }
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
        StartCoroutine(UpdatingTile());
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

            //Ÿ���� ����
            for (int x = minCoor.x; x <= maxCoor.x; ++x)
            {
                for (int y = minCoor.y; y <= maxCoor.y; y++)
                {
                    Vector3Int tilePosition = new Vector3Int(x, y, 0);

                    if (tileManager.HasTile(tilePosition) && !_previousTilePos.Contains(tilePosition))
                    {
                        if (tileManager.availablePlaces[tilePosition].available)
                        {
                            tileManager.RemopvePlace(tilePosition);
                            _previousTilePos.Add(tilePosition);
                            tempSet.Add(tilePosition);
                        }
                    }
                }
            }

            //������ ǰ
            foreach(Vector3Int coorPos in _previousTilePos)
            {
                if (!tempSet.Contains(coorPos))
                {
                    if (tileManager.HasTile(coorPos))
                    {
                        if (tileManager.availablePlaces[coorPos].Object == null)
                        {
                            tileManager.availablePlaces[coorPos].available = true;
                        }
                    }
                }
            }
            _previousTilePos = tempSet;

            //updateFrequency���� �ݺ�
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