using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossReadyState : BossState
{
    #region Properties
    #region Private
    Coroutine readyCoroutine;
    #endregion
    #region Protected
    #endregion
    #region Public
    #endregion
    #region Events
    #endregion
    #endregion

    #region Constructor
    #endregion

    #region Methods
    #region Private
    #endregion
    #region Protected
    protected override void AddListeners()
    {
        base.AddListeners();
    }
    protected override void RemoveListeners()
    {
        base.RemoveListeners();
    }
    #endregion
    #region Public
    public override void Enter()
    {
        base.Enter();
        readyCoroutine = StartCoroutine(WaitUntilReady());
    }
    public override void Exit()
    {
        base.Exit();
        StopAllCoroutines();
    }

    public void StartFight()
    {
        StopCoroutine(readyCoroutine);
        readyCoroutine = null;
        StartCoroutine(GoToFight());
    }
    #endregion
    #endregion

    #region EventHandlers
    #endregion

    #region Coroutines
    private IEnumerator GoToFight()
    {
        yield return null;
        owner.stateMachine.ChangeState<BossAlertState>();
    }

    private IEnumerator WaitUntilReady()
    {
        while (true)
        {
            yield return null;
        }
    }
    #endregion

    #region MonoBehaviour
    #endregion
}
