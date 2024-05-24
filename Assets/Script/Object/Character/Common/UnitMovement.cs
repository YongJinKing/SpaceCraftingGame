using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class UnitMovement : MonoBehaviour
{
    #region Properties
    #region Private
    private Coroutine move;
    private Coroutine rot;
    //for debug, Serialize
    [SerializeField]private float Speed = 1;
    #endregion
    #region Protected
    #endregion
    #region Public
    #endregion
    #region Events
    public UnityEvent moveEndEvent = new UnityEvent();
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
        if (move != null)
        {
            StopCoroutine(move);
        }
        StartCoroutine(MovingToPos(pos));
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
            yield return null;
        }

        moveEndEvent?.Invoke();
        yield return null;
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