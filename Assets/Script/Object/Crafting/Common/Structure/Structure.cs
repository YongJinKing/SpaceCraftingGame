using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Structure : Stat
{
    #region Properties
    #region Private
    private UnityEvent<StructureState> structureStateChangeEvents = new UnityEvent<StructureState>();
    #endregion
    #region Protected
    [SerializeField] protected StructureState _state = new StructureState();
    [SerializeField] protected float def;
    [SerializeField] protected float buildingSpd;
    [SerializeField] protected byte priority;
    [SerializeField] protected string componentName;
    #endregion
    #region Public
    public byte mPriority
    {
        get
        {
            return priority;
        }
        set
        {
            priority = value;
        }
    }
    public StructureState mState
    {
        get
        {
            return _state;
        }
        set
        {
            structureStateChangeEvents?.Invoke(value);
            _state = value;
        }
    }

    public float mBuildingSpeed
    {
        get
        {
            return buildingSpd;
        }
    }
    
    public string mComponentName
    {
        get { return componentName; }
        set
        {
            componentName = value;
        }
    }
    #endregion
    #region Events
    #endregion
    #endregion

    #region Constructor
    public Structure() : base()
    {
        
    }
    #endregion

    #region Method

    protected float GetEfficiency()
    {
        return this[EStat.HP]/ this[EStat.MaxHP];
    }
    #endregion

    #region MonoBehaviour
    protected override void Start()
    {
        base.Start();
        _state = StructureState.BuildProgress;
        AddStat(EStat.DEF, def);
        AddStat(EStat.BuildingSpeed, buildingSpd);
    }
    #endregion
}
