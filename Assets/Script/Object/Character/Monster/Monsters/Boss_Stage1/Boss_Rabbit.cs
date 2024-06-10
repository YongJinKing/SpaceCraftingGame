using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Rabbit : Monster
{
    #region Properties
    #region Private
    #endregion
    #region Protected
    
    #endregion
    #region Public
    
    #endregion
    #region Events
    #endregion
    #endregion

    #region Constructor
    public Boss_Rabbit() : base()
    {
        AddStat(EStat.DetectRadius, 0.0f);
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
        spawnPoint = transform.position;

        stateMachine.ChangeState<RabbitBossInitState>();
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
