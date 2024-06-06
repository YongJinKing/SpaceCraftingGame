using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

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
    [SerializeField] protected int scale;
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

    public int mScale
    {
        get { return scale; }
        set
        {
            scale = value;
        }
    }
    #endregion
    #region Events
    public UnityEvent<Vector3> DestroyEvent;
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

    protected override void OnDead()
    {
        base.OnDead();
        DestroyEvent?.Invoke(this.transform.position);
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
