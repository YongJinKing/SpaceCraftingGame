using System.Collections;
using System.Collections.Generic;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

public abstract class Stat : MonoBehaviour
{
    #region Properties
    #region Private
    private Dictionary<EStat, float> _Stats = new Dictionary<EStat, float>();
    #endregion
    #region Protected
    [SerializeField]protected float MaxHP;
    [SerializeField]protected float HP;
    #endregion
    #region Public
    public float this[EStat type]
    {
        get 
        {
            if (!this._Stats.ContainsKey(type)) return 0.0f;
            return GetModifiedStat(type);
        }
        set
        {
            if (this._Stats.ContainsKey(type))
            {
                _Stats[type] = value;
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
        AddStat(EStat.MaxHP, MaxHP);
        AddStat(EStat.HP, HP);
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
    protected void AddStat(EStat type, float value)
    {
        _Stats.Add(type, value);
    }
    #endregion
    #region Public
    #endregion
    #endregion

    #region EventHandlers
    #endregion

    #region Coroutines
    #endregion

    #region MonoBehaviour
    #endregion
}
