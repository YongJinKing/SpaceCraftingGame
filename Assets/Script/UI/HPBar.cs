using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class HPBar : MonoBehaviour
{
    #region Properties
    #region Private
    private Slider _mySlider;
    [SerializeField]private Image _fillImage;
    [SerializeField]private Stat _myTarget;
    #endregion
    #region Protected
    [SerializeField] protected Color fullHPColor = Color.green;
    [SerializeField] protected Color halfHPColor = Color.yellow;
    [SerializeField] protected Color lowHPColor = Color.red;
    [SerializeField] protected Vector2 _offSet;
    #endregion
    #region Public
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
        StartCoroutine(Following());
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


        //�׸��� 1�϶� 1, 0.5�϶� 0�� ���� y = 2x - 1
        //��Ȳ�� 0.5�϶� 1, 1�϶� 0, 0�϶� 0�� ���� ���� 0 ~ 0.5���� y = 2x, 0.5 ~ 1���� y = -2x+2
        //����� 0.5������ 0, 0�϶� 1 y = -2x + 1
        //�̶� x�� persent ��

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

    }
    #endregion

    #region Coroutines
    protected IEnumerator Following()
    {
        Vector2 screenPos;
        while (true) 
        {
            screenPos = Camera.main.WorldToScreenPoint(_myTarget.transform.position);
            transform.position = screenPos + _offSet;

            yield return null;
        }
    }
    #endregion

    #region MonoBehaviour
    private void Start()
    {
        Initialize();
    }
    private void OnDestroy()
    {
        RemoveListeners();
    }
    #endregion
}
