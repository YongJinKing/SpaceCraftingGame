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
    public enum AnimState
    {
        Boss_Idle, Boss_Move, Boss_JumpAttack_1, Boss_JumpAttack_2, Boss_PickUpAttack, Boss_Throw, Boss_TurningReady, Boss_TurningAttack, Boss_TurningEnd,
        Boss_SkyJump_Start, Boss_SkyJump_Attack, Boss_SkyJump_Falling, Boss_TeaBagging_1, Boss_TeaBagging_2, Boss_Dead
    }
    #endregion
    #region Events
    #endregion
    #endregion

    #region Constructor
    public Boss_Rabbit() : base()
    {
        
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
