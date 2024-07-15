using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using System.IO;

public class UnitMovement : MonoBehaviour
{
    #region Properties
    #region Private
    private Coroutine MoveC;
    private Coroutine PathC;
    private Coroutine RotC;
    //for debug, Serialize
    [SerializeField]private float Speed = 1;
    #endregion
    #region Protected
    #endregion
    #region Public
    #endregion
    #region Events
    public UnityEvent<Vector2> onMovingEvent = new UnityEvent<Vector2>();
    public UnityEvent moveEndEvent = new UnityEvent();
    public UnityEvent pathEndEvent = new UnityEvent();
    #endregion
    #endregion

    #region Constructor
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
    public void OnStop()
    {
        StopAllCoroutines();
    }
    public void OnMoveToPos(Vector2 pos)
    {
        if (MoveC != null)
        {
            StopCoroutine(MoveC);
        }
        if (PathC != null)
        {
            StopCoroutine(PathC);
            PathC = null;
        }
        MoveC = StartCoroutine(MovingToPos(pos));
    }

    public void OnMoveToPath(Vector2[] path)
    {
        if(MoveC != null)
        {
            StopCoroutine(MoveC);
            MoveC = null;
        }
        if(PathC != null)
        {
            StopCoroutine(PathC);
        }
        PathC = StartCoroutine(MovingToPath(path));
    }

    public void OnMoveToDir(Vector2 dir)
    {
        dir.Normalize();
        transform.Translate(dir * Speed * Time.deltaTime);
    }

    public void OnMoveSpeedStatChanged(float oldSpeed ,float newSpeed)
    {
        this.Speed = newSpeed;
    }
    #endregion

    #region Coroutines
    protected IEnumerator MovingToPos(Vector2 pos)
    {
        Vector2 dir = pos - (Vector2)transform.position;
        float dist = dir.magnitude;
        float delta = 0;
        dir.Normalize();

        while(dist > 0)
        {
            delta = Time.deltaTime * Speed;
            if(delta > dist)
            {
                delta = dist;
            }
            dist -= delta;

            transform.Translate(dir * delta);
            onMovingEvent?.Invoke(dir);
            yield return null;
        }

        moveEndEvent?.Invoke();
        yield return null;
    }

    protected IEnumerator MovingToPath(Vector2[] path)
    {
        for(int i = 0; i < path.Length; ++i)
        {
            if (MoveC != null)
            {
                StopCoroutine(MoveC);
            }
            yield return MoveC = StartCoroutine(MovingToPos(path[i]));
        }
        pathEndEvent?.Invoke();
    }

    protected IEnumerator DelaiedLoad()
    {
        yield return new WaitForEndOfFrame();
        Stat self = GetComponent<Stat>();
        this.Speed = self[EStat.MoveSpeed];
    }
    #endregion

    #region MonoBehaviour
    private void OnEnable()
    {
        Stat self = GetComponent<Stat>();
        self.AddStatEventListener(EStat.MoveSpeed, this.OnMoveSpeedStatChanged);
    }
    private void OnDisable()
    {
        Stat self = GetComponent<Stat>();
        self.RemoveStatEventListener(EStat.MoveSpeed, this.OnMoveSpeedStatChanged);
    }
    private void Start()
    {
        StartCoroutine(DelaiedLoad());
    }
    #endregion
}