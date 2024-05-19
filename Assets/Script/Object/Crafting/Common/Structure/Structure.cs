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
    #endregion
    #region Public
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
