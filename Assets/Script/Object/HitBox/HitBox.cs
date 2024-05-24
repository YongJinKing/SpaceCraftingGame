using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

public abstract class HitBox : MonoBehaviour
{
    #region Properties
    #region Private
    #endregion
    #region Protected
    //if (_hitBoxSize.y < 0) , Circle Collider 
    [SerializeField]protected Vector2 _hitBoxSize;
    [SerializeField]protected LayerMask _targetMask;
    //duration for safe
    [SerializeField]protected float _duration = 5.0f;
    [SerializeField]protected float _hitFrequency = 0.5f;
    protected Vector2 pos;
    protected bool isCircle = false;
    protected HashSet<Stat> calculatedObject = new HashSet<Stat>();
    //Dictionary`s UnityEvent<Collider hit_Collider, Vector2 hitPos>
    protected Dictionary<LayerMask, UnityEvent<Collider2D, Vector2>> onHitEvents = new Dictionary<LayerMask, UnityEvent<Collider2D, Vector2>>();
    #endregion
    #region Public
    public GameObject hitEffectPrefeb;
    public GameObject destroyEffectPrefab;
    public Vector2 hitBoxSize
    {
        get { return _hitBoxSize; }
        set { _hitBoxSize = value; }
    }
    public LayerMask targetMask
    {
        get {return _targetMask; } 
        set {_targetMask = value; }
    }
    public float duration
    {
        get { return _duration; }
        set {  _duration = value; }
    }

    public float hitFrequency
    {
        set { _hitFrequency = value; }
        get { return _hitFrequency; }
    }
    #endregion
    #region Events
    public UnityEvent OnDurationEndEvent = new UnityEvent();
    #endregion
    #endregion

    #region Constructor
    #endregion

    #region Methods
    #region Private
    #endregion
    #region Protected
    protected virtual void Initialize()
    {
        if(hitBoxSize.y < 0) isCircle = true;
        if(targetMask != 0)
        {
            for(int i = 0; i < 32; ++i)
            {
                if((targetMask & (1 << i)) != 0)
                {
                    onHitEvents.Add(1 << i, new UnityEvent<Collider2D, Vector2>());
                    Debug.Log($"{LayerMask.LayerToName(i)}");
                }
            }
        }
        gameObject.SetActive(false);
    }
    protected virtual void HitEffectPlay(Vector2 targetPos)
    {
        if(hitEffectPrefeb != null)
        {
            Instantiate(hitEffectPrefeb, targetPos, Quaternion.identity);
        }
    }

    protected virtual void DestroyHitBox()
    {
        if(destroyEffectPrefab != null)
        {
            Instantiate(destroyEffectPrefab, transform.position, Quaternion.identity);
        }
        Destroy(this);
    }
    #endregion
    #region Public
    public virtual void Refresh()
    {
        calculatedObject.Clear();
    }
    public virtual void Activate(Vector2 pos)
    {
        gameObject.SetActive(true);
        this.pos = pos;
        StartCoroutine(HitChecking());
        StartCoroutine(Refreshing());
    }
    public virtual void Deactivate()
    {
        StopAllCoroutines();
        gameObject.SetActive(false);
        Refresh();
    }
    #endregion
    #endregion

    #region EventHandlers
    #endregion

    #region Coroutines
    protected abstract IEnumerator HitChecking();
    protected virtual IEnumerator Refreshing()
    {
        if(hitFrequency < 0) yield break;
        while(duration > 0)
        {
            yield return new WaitForSeconds(hitFrequency);
            Refresh();
        }
    }
    #endregion

    #region MonoBehaviour
    protected virtual void Start()
    {
        Initialize();
    }
    //for Debug
#if UNITY_EDITOR
    public bool debuging = false;
    protected virtual void OnDrawGizmos()
    {
        if (debuging)
        {
            //Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.color = new Color(0.0f, 1.0f, 0.0f, 0.1f);
            if (!isCircle)
                Gizmos.DrawCube(transform.position, hitBoxSize);
            else
                Gizmos.DrawSphere(transform.position, hitBoxSize.x * 0.5f);
        }
    }
#endif
    #endregion
}
