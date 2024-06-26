using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Rabbit : Boss
{
    #region Properties
    #region Private
    #endregion
    #region Protected

    #endregion
    #region Public
    
    public RabitBossAnimState _animState;
    public string currentAnim;
    #endregion
    #region Events
    #endregion
    #endregion

    #region Constructor
    public Boss_Rabbit() : base()
    {
        _animState = RabitBossAnimState.Boss_Idle;
    }
    #endregion

    #region Methods
    #region Private
    #endregion
    #region Protected
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    protected override void Initialize()
    {
        base.Initialize();
        
    }
    #endregion
    #region Public
    // Update is called once per frame
    void Update()
    {
        
    }
    #endregion
    #endregion

    #region EventHandlers
    #endregion

    #region Coroutines
    #endregion

    #region MonoBehaviour
    #endregion


}
