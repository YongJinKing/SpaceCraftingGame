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
    ///�� ��Ʈ�ڽ��� �߻��� �����Ǵ����� ���� bool��
    ///</summary>
    [SerializeField] protected bool _isDestroy = false;
    protected Vector2 pos;
    protected bool isCircle = false;
    ///<summary>
    ///�̹� ��Ʈ�� ó���� ���� ������Ʈ���� �����ϴ� �����
    ///</summary>
    protected HashSet<Stat> calculatedObject = new HashSet<Stat>();
    ///<summary>
    ///���̾� ����ũ�� ���� ��Ʈ �̺�Ʈ�� �����Ѱ�
    ///LayerMask : ������ �Ѱ������� ����ȴ�. ��� ����� ���� ������ �ʹ� �������� �����̴�.
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
    ///�� ��Ʈ�ڽ��� �߻��� �����Ǵ����� ���� bool��
    ///SerializeField �Ȱ��� ������� ���Ѱ����� �������� ����
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
    ///Ŭ���� �ʱ�ȭ, Start �Լ����� ����
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
        //��ٷ� false �ع����� �ڽ� ������Ʈ���� �ʱ�ȭ�� ��������
        StartCoroutine(DelaiedInit());
        //gameObject.SetActive(false);
    }

    ///<summary>
    ///��Ʈ�ÿ� ����Ʈ�� ����ϴ� �Լ�
    ///</summary>
    ///<param name="targetPos">��Ʈ ����Ʈ�� ����� ��ġ, ���� �����ǰ�</param>>
    protected virtual void HitEffectPlay(Vector2 targetPos)
    {
        if(hitEffectPrefab != null)
        {
            Instantiate(hitEffectPrefab, targetPos, Quaternion.identity);
        }
    }
    ///<summary>
    ///��Ʈ üũ�� ������� ȣ��Ǵ� �Լ�
    ///</summary>
    protected virtual void HitCheckEnd()
    {
        Refresh();
    }

    ///<summary>
    ///����ü���� ��� �ڱ��ڽ��� �ı��Ҷ� ���� �Լ�. �ı����� ����Ʈ�� ������ ���
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
    ///��Ʈ�� ���Ǿ��� ���� ������Ʈ���� �ʱ�ȭ �Ͽ� �ٽ� ��Ʈ�� ������������ ����� ����
    ///</summary>
    public virtual void Refresh()
    {
        calculatedObject.Clear();
    }
    ///<summary>
    ///�ܺο��� ȣ���Ͽ� ��Ʈ�ڽ� Ȱ��ȭ
    ///</summary>
    public virtual void Activate(Vector2 pos)
    {
        gameObject.SetActive(true);
        this.pos = pos;
        StartCoroutine(HitChecking());
        StartCoroutine(Refreshing());
    }
    ///<summary>
    ///�ܺο��� ȣ���Ͽ� ��Ʈ�ڽ� ��Ȱ��ȭ
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
    ///�ٴ���Ʈ�� ���� Refresh�Լ��� ��� ȣ���Ű�� �ڷ�ƾ
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
