using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Monster
{
    #region Properties
    #region Private
    #endregion
    #region Protected
    
    #endregion
    #region Public
    public Transform player; // 임시 변수, 플레이어 위치에 따라 보스가 좌우를 제대로 바라보도록 하려함
    public BossDirMoveAction bossDirMove;
    public Transform bossImg;

    public Action[] specialActions;
    #endregion
    #region Events
    #endregion
    #endregion

    #region Constructor
    public Boss() : base()
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
        spawnPoint = transform.position;
        stateMachine.ChangeState<BossInitState>(); // 
    }
    #endregion
    #region Public
    // Update is called once per frame
    protected virtual void Update()
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
