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
    [SerializeField] protected Animator animator;
    #endregion
    #region Public
    public Transform destroyVFX;
    public Transform buildVFX;
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

    public override void TakeDamage(float damage)
    {
        float dmg = damage - this[EStat.DEF];
        if (dmg <= 0.0f) dmg = 1f;
        this[EStat.HP] -= dmg;
        if (this[EStat.HP] < 0)
        {
            OnDead();
        }
    }
    protected override void OnDead()
    {
        base.OnDead();
        Debug.Log("Ondead ½ÇÇà");
        animator.SetTrigger("Destroy");
        DestroyEvent?.Invoke(this.transform.position);
        Destroy(this.gameObject, 2f);
    }

    public void PlayDestoryVFX()
    {
        destroyVFX.gameObject.SetActive(true);
    }

    public void PlayBuildVFX()
    {
        buildVFX.gameObject.SetActive(true);
    }

    public void StopBuildVFX()
    {
        buildVFX.gameObject.SetActive(false);
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

    protected virtual void OnEnable()
    {
        PlayBuildVFX();
    }

    #endregion
}
