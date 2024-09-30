using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Stat : MonoBehaviour, IDamage, IGetPriority
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
    public float Priority;
    ///<summary>
    ///버프 디버프에 의해서 변형된 스텟을 가져옴
    ///set할때는 this[EStat.type] = this.GetRawStat(EStat.type) +- value 식으로 원래 값을 가져온 다음에 변형해야된다.
    ///</summary>
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
    public UnityEvent deadEvent = new UnityEvent();
    #endregion
    #endregion

    #region Constructor
    public Stat()
    {
        AddStat(EStat.MaxHP, 0);
        AddStat(EStat.HP, 0);
        AddStat(EStat.Priority, 0);
    }
    #endregion

    #region Methods
    #region Private
    private float GetModifiedStat(EStat type)
    {
        ValueChanger vc = new ValueChanger(_Stats[type]);
        IGetStatValueModifiers[] modifiers = GetComponentsInChildren<IGetStatValueModifiers>();

        foreach (IGetStatValueModifiers modi in modifiers)
        {
            List<ValueModifier> vm = modi.GetStatValueModifiers(type);
            if (vm != null)
                vc.AddModifiers(vm);
        }

        return vc.GetModifiedValue();
    }
    #endregion
    #region Protected
    /// <summary>
    /// 클래스 초기화 함수, Start 함수에서 실행
    /// </summary>
    protected virtual void Initialize()
    {
        this[EStat.MaxHP] = MaxHP;
        this[EStat.HP] = this[EStat.MaxHP];
        this[EStat.Priority] = Priority;
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
    /// <summary>
    /// TakeDamage 인터페이스에서 실행될 예정인 죽었을때 실행되는 함수
    /// </summary>
    protected virtual void OnDead()
    {
        deadEvent?.Invoke();
    }
    #endregion
    #region Public
    public abstract void TakeDamage(float damage);
    public void AddStatEventListener(EStat type, UnityAction<float, float> function)
    {
        if (_StatChangedEvents.ContainsKey(type))
        {
            Debug.Log("Stat.AddStatEventListener");
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
    /// <summary>
    /// IGetPriority 인터페이스 함수
    /// 몬스터에서 이객체가 타겟이 될지 말지 우선순위를 주는 함수
    /// </summary>
    /// <returns>계산된 우선순위 float값</returns>
    public virtual float GetPriority()
    {
        //if Priority is below 0, this Object don`t becomes target
        if (this[EStat.Priority] < 0) 
            return -1.0f;

        return Mathf.Clamp(this[EStat.Priority] - 2 * (this[EStat.HP] / this[EStat.MaxHP]), 0, float.MaxValue);
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
