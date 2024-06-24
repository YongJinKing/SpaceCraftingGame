using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class WaveMonsterIdleState : MonsterState
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
    #endregion

    #region Methods
    #region Private
    #endregion
    #region Protected
    #endregion
    #region Public
    public override void Enter()
    {
        base.Enter();
        StartCoroutine(Detecting());
        StartCoroutine(ApproachingToSpaceShip());
    }
    public override void Exit()
    {
        base.Exit();
        StopAllCoroutines();
    }
    #endregion
    #endregion

    #region EventHandlers
    #endregion

    #region Coroutines
    protected IEnumerator ApproachingToSpaceShip()
    {
        //for Test, Hard Coded
        //Must be Change Later
        while (true) 
        {
            owner.dirMove.Activate(Vector2.zero);
            yield return null;
        }
    }

    protected IEnumerator Detecting()
    {
        if (owner.ai == null) yield break;

        GameObject target = null;

        while(target == null)
        {
            target = owner.ai.TargetSelect(owner.targetMask, owner[EStat.DetectRadius]);
            yield return null;
        }

        owner.target = target;
        owner.stateMachine.ChangeState<WaveMonsterDetectState>();
    }
    #endregion

    #region MonoBehaviour
    #endregion
}