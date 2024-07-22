using System.Collections;
using UnityEngine;
using UnityEngine.Events;


/// <summary>
/// #need to modify later
/// </summary>
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
    public UnityEvent<Vector2> moveToDirEvent = new UnityEvent<Vector2>();
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
        UnitMovement movement = GetComponent<UnitMovement>();
        if(movement != null)
        {
            moveToDirEvent.AddListener(movement.OnMoveToDir);
        }
    }
    protected override void RemoveListeners()
    {
        UnitMovement movement = GetComponent<UnitMovement>();
        if (movement != null)
        {
            moveToDirEvent?.RemoveListener(movement.OnMoveToDir);
        }
    }
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
        owner.animator.SetBool("B_Move", false);
        StopAllCoroutines();
        base.Exit();
    }
    #endregion
    #endregion

    #region EventHandlers
    #endregion

    #region Coroutines
    /// <summary>
    /// #need to modify later
    /// </summary>
    protected IEnumerator ApproachingToSpaceShip()
    {
        //for Test, Hard Coded
        //Must be Change Later

        owner.animator.SetBool("B_Move", true);
        while (true) 
        {
            moveToDirEvent?.Invoke(Vector3.zero - transform.position);
            yield return null;
        }
        //owner.animator.SetBool("B_Move", false);
    }

    /// <summary>
    /// 일단 1.0초 마다 디텍트 하기로 함
    /// </summary>
    protected IEnumerator Detecting()
    {
        if (owner.ai == null) yield break;

        GameObject target = null;

        while(target == null)
        {
            target = owner.ai.TargetSelect(owner.targetMask, owner[EStat.DetectRadius]);
            //못찾았을때만 1초 대기
            if(target == null)
                yield return new WaitForSeconds(1.0f);
            else
            {
                yield return null;
            }
        }

        owner.target = target;
        owner.stateMachine.ChangeState<WaveMonsterDetectState>();
        yield return null;
    }
    #endregion

    #region MonoBehaviour
    #endregion
}