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
    public UnityEvent stopMoveEvent = new UnityEvent();
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
        stopMoveEvent.AddListener(movement.OnStop);
        moveToPathEvent.AddListener(movement.OnMoveToPath);
    }
    protected override void RemoveListeners()
    {
        base.RemoveListeners();
        stopMoveEvent.RemoveAllListeners();
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
        StopAllCoroutines();
        stopMoveEvent?.Invoke();
        owner.animator.SetBool("B_Move", false);
        base.Exit();
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

        yield return null;

        bool closeEnough = false;
        while (!closeEnough)
        {
            if (owner.target == null)
            {
                owner.stateMachine.ChangeState<WaveMonsterIdleState>();
                yield break;
            }

            Collider2D[] temp = Physics2D.OverlapCircleAll(transform.position, owner.activatedAction.activeRadius);

            foreach (Collider2D c in temp) 
            { 
                if(c.gameObject == owner.target)
                {
                    closeEnough = true;
                }
            }
            yield return null;
        }

        owner.activatedAction.Activate(owner.target.transform.position);
        owner.animator.SetBool("B_Attack", true);

        if (owner.activatedAction.fireAndForget)
            owner.stateMachine.ChangeState<WaveMonsterMovableActionState>();
        else
            owner.stateMachine.ChangeState<WaveMonsterUnmovableActionState>();
    }

    protected IEnumerator TravelingPath()
    {
        owner.animator.SetBool("B_Move", true);
        while (owner.target != null)
        {
            if(owner.ai.PathFinding(transform.position, owner.target.transform.position, out Vector2[] path))
            {

                moveToPathEvent?.Invoke(path);
                yield return new WaitForSeconds(1.1f);
            }
            else
            {
                break;
            }
        }
        yield return null;
        owner.animator.SetBool("B_Move", false);

        //if cannot find path
        owner.stateMachine.ChangeState<WaveMonsterIdleState>();
    }
    #endregion

    #region MonoBehaviour
    #endregion
}
