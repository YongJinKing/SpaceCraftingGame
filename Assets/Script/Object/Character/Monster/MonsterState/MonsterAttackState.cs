using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MonsterAttackState : MonsterState
{
    #region Properties
    #region Private
    #endregion
    #region Protected
    #endregion
    #region Public
    #endregion
    #region Events
    public UnityEvent<Vector2[]> moveToPathEvent = new UnityEvent<Vector2[]>();
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
        UnitMovement movement = GetComponent<UnitMovement>();
        moveToPathEvent.AddListener(movement.OnMoveToPath);

    }
    protected override void RemoveListeners()
    {
        base.RemoveListeners();
        moveToPathEvent.RemoveAllListeners();
    }
    #endregion
    #region Public
    public override void Enter()
    {
        base.Enter();
        StartCoroutine(Attacking());
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
    protected IEnumerator Attacking()
    {
        if(owner.activatedAction == null)
        {
            owner.stateMachine.ChangeState<MonsterIdleState>();
        }


        StartCoroutine(TravelingPath());
        yield return null;

        Vector2 dir;
        float dist;
        do
        {
            dir = owner.target.transform.position - transform.position;
            dist = dir.magnitude;
        }
        while (dist > owner.activatedAction.activeRadius);
        owner.activatedAction.Activate(owner.target.transform.position);

        if (owner.activatedAction.fireAndForget)
            owner.stateMachine.ChangeState<MonsterMovableActionState>();
        else
            owner.stateMachine.ChangeState<MonsterUnmovableActionState>();
    }

    protected IEnumerator TravelingPath()
    {
        //findpath every 1.1f seconds
        //because pathfinding is very heavy
        while(owner.ai.PathFinding(transform.position, owner.target.transform.position, out Vector2[] path))
        {
            moveToPathEvent?.Invoke(path);
            yield return new WaitForSeconds(1.1f);
        }
        //if cannot find path
        owner.stateMachine.ChangeState<MonsterIdleState> ();
    }
    #endregion

    #region MonoBehaviour
    #endregion
}
