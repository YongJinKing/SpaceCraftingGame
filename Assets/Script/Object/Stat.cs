using System.Collections;
using System.Collections.Generic;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using UnityEngine.Events;

public abstract class Stat : MonoBehaviour, IDamage
{
    #region Properties
    #region Private
    private Dictionary<EStat, float> _Stats = new Dictionary<EStat, float>();
    //UnityEvent = first float : oldValue, second float : newValue
    private Dictionary<EStat, UnityEvent<float, float>> _StatChangedEvents = new Dictionary<EStat, UnityEvent<float, float>>();
    #endregion
    #region Protected

    #endregion
    #region Public
    public float MaxHP;
    public float this[EStat type]
    {
        get
        {
            if (!this._Stats.ContainsKey(type)) return -1.0f;
            return GetModifiedStat(type);
        }
        set
        {
            if (this._Stats.ContainsKey(type))
            {
                float old = GetModifiedStat(type);
                _Stats[type] = value;
                _StatChangedEvents[type]?.Invoke(old, GetModifiedStat(type));
            }
        }
    }
    #endregion
    #region Events
    #endregion
    #endregion

    #region Constructor
    public Stat()
    {
        AddStat(EStat.MaxHP, 0);
        AddStat(EStat.HP, 0);
    }
    #endregion

    #region Methods
    #region Private
    private float GetModifiedStat(EStat type)
    {
        return _Stats[type];
    }
    #endregion
    #region Protected
    protected virtual void Initialize()
    {
        this[EStat.MaxHP] = MaxHP;
        this[EStat.HP] = this[EStat.MaxHP];
    }
    protected void AddStat(EStat type, float value)
    {
        if (!_Stats.ContainsKey(type))
            _Stats.Add(type, value);
        else
            _Stats[type] = value;

        if (!_StatChangedEvents.ContainsKey(type))
            _StatChangedEvents.Add(type, new UnityEvent<float, float>());
    }
    #endregion
    #region Public
    public abstract void TakeDamage(float damage);
    public void AddStatEventListener(EStat type, UnityAction<float, float> function)
    {
        if (_StatChangedEvents.ContainsKey(type))
        {
            _StatChangedEvents[type].AddListener(function);
        }
    }
    public void RemoveStatEventListener(EStat type, UnityAction<float, float> function)
    {
        if (_StatChangedEvents.ContainsKey(type))
        {
            _StatChangedEvents[type].RemoveListener(function);
        }
    }
    public float GetRawStat(EStat type)
    {
        if ( _Stats.ContainsKey(type))
            return _Stats[type];
        return -1.0f;
    }
    #endregion
    #endregion

    #region EventHandlers
    #endregion

    #region Coroutines
    #endregion

    #region MonoBehaviour
    protected virtual void Start()
    {
        Initialize();
    }
    #endregion
}
