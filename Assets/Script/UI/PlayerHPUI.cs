using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHPUI : MonoBehaviour
{
    #region Properties
    #region Private
    [SerializeField] Stat _myTarget;
    [SerializeField] Transform _gridLayOut;
    #endregion
    #region Protected
    [SerializeField] protected Sprite fullHeart;
    [SerializeField] protected Sprite halfHeart;
    [SerializeField] protected Sprite ZeroHeart;
    protected List<Image> Hearts = new List<Image>();
    protected int heartCount;
    protected int fulls;
    protected int half;
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
        _gridLayOut = transform.GetChild(0);
        OnMaxHPChanged(0, 0);
    }
    #endregion
    #region Protected
    
    protected void AddListeners()
    {
        if (_myTarget != null)
        {
            _myTarget.AddStatEventListener(EStat.HP, OnHPChanged);
            _myTarget.AddStatEventListener(EStat.MaxHP, OnMaxHPChanged);
            _myTarget.deadEvent.AddListener(OnDead);
        }
    }
    protected void RemoveListeners()
    {
        if (_myTarget != null)
        {
            _myTarget.RemoveStatEventListener(EStat.HP, OnHPChanged);
            _myTarget.RemoveStatEventListener(EStat.MaxHP, OnMaxHPChanged);
            _myTarget.deadEvent.RemoveListener(OnDead);
        }
    }

    #endregion
    #region Public
    #endregion
    #endregion

    #region EventHandlers
    public void OnMaxHPChanged(float oldVal, float newVal)
    {
        if (Hearts.Count > 0)
        {
            Image[] temp = Hearts.ToArray();
            Hearts.Clear();
            for(int i = 0; i < temp.Length; ++i)
            {
                Destroy(temp[i].gameObject);
            }
        }

        heartCount = (int)(_myTarget[EStat.MaxHP] + 0.5) / 2;
        fulls = (int)(_myTarget[EStat.HP] + 0.5) / 2;
        half = (int)(_myTarget[EStat.HP] + 0.5) % 2 == 0 ? 0 : 1;
        for (int i = 0; i < fulls; i++)
        {
            GameObject temp = new GameObject("Full");
            Image heartImg = temp.AddComponent<Image>();
            heartImg.sprite = fullHeart;
            temp.transform.SetParent(_gridLayOut);
            Hearts.Add(heartImg);
        }
        if (half > 0)
        {
            GameObject temp = new GameObject("Half");
            Image heartImg = temp.AddComponent<Image>();
            heartImg.sprite = halfHeart;
            temp.transform.SetParent(_gridLayOut);
            Hearts.Add(heartImg);
        }
        for (int i = 0; i < heartCount - fulls - half; ++i)
        {
            GameObject temp = new GameObject("Zero");
            Image heartImg = temp.AddComponent<Image>();
            heartImg.sprite = ZeroHeart;
            temp.transform.SetParent(_gridLayOut);
            Hearts.Add(heartImg);
        }
    }
    public void OnHPChanged(float oldVal, float newVal)
    {
        fulls = (int)(_myTarget[EStat.HP] + 0.5) / 2;
        half = (int)(_myTarget[EStat.HP] + 0.5) % 2 > 0 ? 1 : 0;

        for (int i = 0; i < fulls; ++i)
        {
            Hearts[i].sprite = fullHeart;
        }
        if(half > 0)
        {
            Hearts[fulls].sprite = halfHeart;
        }

        for(int i = fulls + half; i < heartCount; ++i)
        {
            Hearts[i].sprite = ZeroHeart;
        }

        Debug.Log($"PlayerHPUI.OnHPChanged fulls = {fulls}, half = {half}, heartCount = {heartCount}");
    }
    public void OnDead()
    {
        RemoveListeners();
        StartCoroutine(Dying());
    }
    #endregion

    #region Coroutines
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
    private void OnDestroy()
    {
        RemoveListeners();
    }
    #endregion
}
