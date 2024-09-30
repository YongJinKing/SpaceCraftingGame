using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    #region Properties
    #region Private
    private Slider _mySlider;
    [SerializeField]private Image _fillImage;
    [SerializeField]private Stat _myTarget;
    private bool initialized = false;
    private Coroutine following;
    #endregion
    #region Protected
    [SerializeField] protected Color fullHPColor = Color.green;
    [SerializeField] protected Color halfHPColor = Color.yellow;
    [SerializeField] protected Color lowHPColor = Color.red;
    
    #endregion
    #region Public
    public Stat myTarget
    {
        get { return _myTarget; }
        set { _myTarget = value; }
    }
    public Vector2 _offSet;
    #endregion
    #region Events
    #endregion
    #endregion

    #region Constructor
    #endregion

    #region Methods
    #region Private
    private void Initialize()
    {
        AddListeners();
        _mySlider = GetComponent<Slider>();
        _mySlider.value = 1.0f;
        _fillImage.color = fullHPColor;
        initialized = true;
        OnHPChanged(0, 0);
        if(following != null)
        {
            StopCoroutine(following);
        }
        following = StartCoroutine(Following());
    }
    #endregion
    #region Protected
    protected void AddListeners()
    {
        if (_myTarget != null)
        {
            _myTarget.AddStatEventListener(EStat.HP, OnHPChanged);
            _myTarget.AddStatEventListener(EStat.MaxHP, OnHPChanged);
            _myTarget.deadEvent.AddListener(OnDead);
        }
    }
    protected void RemoveListeners()
    {
        if (_myTarget != null)
        {
            _myTarget.RemoveStatEventListener(EStat.HP, OnHPChanged);
            _myTarget.RemoveStatEventListener(EStat.MaxHP, OnHPChanged);
            _myTarget.deadEvent.RemoveListener(OnDead);
        }
    }
    #endregion
    #region Public
    #endregion
    #endregion

    #region EventHandlers
    public void OnHPChanged(float oldVal, float newVal)
    {
        Debug.Log("HPBar.OnHPChanged");

        float persent = _myTarget[EStat.HP] / _myTarget[EStat.MaxHP];
        _mySlider.value = persent;


        //그린은 1일때 1, 0.5일때 0인 직선 y = 2x - 1
        //주황은 0.5일때 1, 1일때 0, 0일때 0인 꺾인 직선 0 ~ 0.5에서 y = 2x, 0.5 ~ 1에서 y = -2x+2
        //레드는 0.5까지는 0, 0일때 1 y = -2x + 1
        //이때 x는 persent 값

        if(_fillImage != null)
        {
            if(persent > 0.5)
            {
                _fillImage.color =
                    (2.0f * persent - 1.0f) * fullHPColor +
                    (-2.0f * persent + 2.0f) * halfHPColor;
            }
            else
            {
                _fillImage.color =
                    (2.0f * persent) * halfHPColor +
                    (-2.0f * persent + 1.0f) * lowHPColor;
            }
        }
    }
    public void OnDead()
    {
        RemoveListeners();
        StartCoroutine(Dying());
    }
    #endregion

    #region Coroutines
    protected IEnumerator Following()
    {
        yield return new WaitUntil(() => initialized == true);
        Vector2 screenPos;
        while (true) 
        {
            screenPos = Camera.main.WorldToScreenPoint(_myTarget.transform.position);
            transform.position = screenPos + _offSet;

            yield return null;
        }
    }
    protected IEnumerator Dying()
    {
        yield return new WaitForSeconds(1.0f);
        Destroy(this.gameObject);
    }
    #endregion

    #region MonoBehaviour
    private void Start()
    {
        Initialize();
    }
    private void OnEnable()
    {
        if(following != null)
        {
            StopCoroutine(following);
        }
        following = StartCoroutine(Following());
    }
    private void OnDestroy()
    {
        RemoveListeners();
    }
    #endregion
}
