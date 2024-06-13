using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.GraphicsBuffer;

public class BossMovement : MonoBehaviour
{
    #region Properties
    #region Private
    private Coroutine MoveC;
    
    //for debug, Serialize
    [SerializeField] private float Speed = 1;
    #endregion
    #region Protected
    
    #endregion
    #region Public
    #endregion
    #region Events
    public UnityEvent moveEndEvent = new UnityEvent();
    public float limitDist = 5f;
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
        
        if(pos.magnitude > 0f)MoveC = StartCoroutine(MovingToPos(pos));
        else MoveC = StartCoroutine(MovingCirclularPos(pos));
    }


    public void OnMoveToDir(Vector2 dir)
    {
        dir.Normalize();
        transform.Translate(dir * Speed * Time.deltaTime);
    }

    public void OnMoveSpeedStatChanged(float oldSpeed, float newSpeed)
    {
        this.Speed = newSpeed;
    }
    #endregion

    #region Coroutines
    protected IEnumerator MovingToPos(Vector2 pos)
    {
        Debug.Log("무빙 투 포스");
        Vector2 dir = pos - (Vector2)transform.position;
        float dist = dir.magnitude - limitDist;
        if (dist < 0f) dist = limitDist;
        float delta = 0;
        dir.Normalize();

        while (dist > 0)
        {
            delta = Time.deltaTime * Speed;
            if (delta > dist)
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

    protected IEnumerator MovingCirclularPos(Vector2 pos)
    {
        Debug.Log("무빙 서큘러 포스");
        float moveTime = Random.Range(2f, 5f);
        float angle = 0.0f; // 각도
        while (moveTime > 0f)
        {
            angle += Speed * Time.deltaTime;

            // 새로운 위치 계산
            float x = Mathf.Cos(angle) * limitDist;
            float y = Mathf.Sin(angle) * limitDist;

            // 새로운 위치 설정
            transform.position = new Vector3(pos.x + x, pos.y + y, transform.position.z);
            yield return null;
            moveTime -= Time.deltaTime;
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
