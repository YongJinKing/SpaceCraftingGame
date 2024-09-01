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
    [SerializeField]protected float _hitFrequency = -1.0f;
    ///<summary>
    ///이 히트박스가 발사후 삭제되는지에 대한 bool값
    ///</summary>
    [SerializeField] protected bool _isDestroy = false;
    protected Vector2 pos;
    protected bool isCircle = false;
    ///<summary>
    ///이미 히트가 처리된 게임 오브젝트들을 저장하는 저장소
    ///</summary>
    protected HashSet<Stat> calculatedObject = new HashSet<Stat>();
    ///<summary>
    ///레이어 마스크에 따른 히트 이벤트를 저장한곳
    ///LayerMask : 무조건 한개씩으로 저장된다. 모든 경우의 수를 넣으면 너무 많아지기 때문이다.
    ///</summary>
    protected Dictionary<LayerMask, UnityEvent<Collider2D, Vector2>> onHitEvents = new Dictionary<LayerMask, UnityEvent<Collider2D, Vector2>>();
    #endregion
    #region Public
    public Transform originPos;
    public GameObject hitEffectPrefab;
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
    ///<summary>
    ///이 히트박스가 발사후 삭제되는지에 대한 bool값
    ///SerializeField 된것은 디버깅을 위한것으로 수정하지 말것
    ///</summary>
    public bool isDestroy
    {
        get { return _isDestroy; }
        protected set { _isDestroy = value; }
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
    ///<summary>
    ///클래스 초기화, Start 함수에서 실행
    ///</summary>
    protected virtual void Initialize()
    {
        if(hitBoxSize.y < 0) isCircle = true;

        BaseAffect[] affects = GetComponentsInChildren<BaseAffect>();

        if(targetMask != 0)
        {
            for(int i = 0; i < 32; ++i)
            {
                if((targetMask & (1 << i)) != 0)
                {
                    onHitEvents.Add(1 << i, new UnityEvent<Collider2D, Vector2>());

                    //Register Affects
                    for(int j = 0; j < affects.Length; ++j)
                    {
                        if ((affects[j].targetMask & (1 << i)) != 0)
                        {
                            onHitEvents[1 << i].AddListener(affects[j].OnActivate);
                        }
                    }

                    //Debug.Log($"{LayerMask.LayerToName(i)}");
                }
            }
        }
        //곧바로 false 해버리면 자식 오브젝트들의 초기화가 오류가남
        StartCoroutine(DelaiedInit());
        //gameObject.SetActive(false);
    }

    ///<summary>
    ///히트시에 이펙트를 출력하는 함수
    ///</summary>
    ///<param name="targetPos">히트 이펙트를 출력할 위치, 월드 포지션값</param>>
    protected virtual void HitEffectPlay(Vector2 targetPos)
    {
        if(hitEffectPrefab != null)
        {
            Instantiate(hitEffectPrefab, targetPos, Quaternion.identity);
        }
    }
    ///<summary>
    ///히트 체크가 끝날경우 호출되는 함수
    ///</summary>
    protected virtual void HitCheckEnd()
    {
        Refresh();
    }

    ///<summary>
    ///투사체같은 경우 자기자신을 파괴할때 쓰는 함수. 파괴시의 이펙트가 있으면 출력
    ///</summary>
    protected virtual void DestroyHitBox()
    {
        if(destroyEffectPrefab != null)
        {
            Instantiate(destroyEffectPrefab, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
    #endregion
    #region Public
    ///<summary>
    ///히트가 계산되었던 게임 오브젝트들을 초기화 하여 다시 히트가 가능해지도록 만드는 힘수
    ///</summary>
    public virtual void Refresh()
    {
        calculatedObject.Clear();
    }
    ///<summary>
    ///외부에서 호출하여 히트박스 활성화
    ///</summary>
    public virtual void Activate(Vector2 pos)
    {
        gameObject.SetActive(true);
        this.pos = pos;
        StartCoroutine(HitChecking());
        StartCoroutine(Refreshing());
    }
    ///<summary>
    ///외부에서 호출하여 히트박스 비활성화
    ///</summary>
    public virtual void Deactivate()
    {
        StopAllCoroutines();
        gameObject.SetActive(false);
        HitCheckEnd();
    }
    #endregion
    #endregion

    #region EventHandlers
    #endregion

    #region Coroutines
    protected abstract IEnumerator HitChecking();
    ///<summary>
    ///다단히트를 위한 Refresh함수를 계속 호출시키는 코루틴
    ///</summary>
    protected virtual IEnumerator Refreshing()
    {
        if(hitFrequency < 0) yield break;
        while(duration > 0)
        {
            yield return new WaitForSeconds(hitFrequency);
            Refresh();
        }
    }
    private IEnumerator DelaiedInit()
    {
        yield return null;
        gameObject.SetActive(false);
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
