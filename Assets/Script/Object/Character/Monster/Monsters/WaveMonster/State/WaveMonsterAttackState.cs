using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WaveMonsterAttackState : MonsterState
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
        StartCoroutine(TravelingPath());
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
            owner.stateMachine.ChangeState<WaveMonsterIdleState>();
        }


        StartCoroutine(TravelingPath());
        yield return null;

        Vector2 dir;
        float dist;
        do
        {
            if (owner.target == null) break;

            dir = owner.target.transform.position - transform.position;
            dist = dir.magnitude;
            yield return null;
        }
        while (dist > owner.activatedAction.activeRadius);

        if(owner.target == null)
        {
            owner.stateMachine.ChangeState<WaveMonsterIdleState>();
            yield break;
        }

        owner.activatedAction.Activate(owner.target.transform.position);

        if (owner.activatedAction.fireAndForget)
            owner.stateMachine.ChangeState<WaveMonsterMovableActionState>();
        else
            owner.stateMachine.ChangeState<WaveMonsterUnmovableActionState>();
    }

    protected IEnumerator TravelingPath()
    {
        Vector2 dir;

        while (owner.target != null)
        {
            dir = owner.target.transform.position - transform.position;
            /*
            if(dir.magnitude < owner.activatedAction.activeRadius)
            {
                owner.dirMove.Activate(owner.target.transform.position);
            }
            else 
            */
            if(owner.ai.PathFinding(transform.position, owner.target.transform.position, out Vector2[] path))
            {
                moveToPathEvent?.Invoke(path);
                yield return new WaitForSeconds(1.1f);
            }
            else
            {
                break;
            }

            yield return null;
        }

        //if cannot find path
        owner.stateMachine.ChangeState<WaveMonsterIdleState> ();
    }
    #endregion

    #region MonoBehaviour
    #endregion
}
