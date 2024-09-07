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
        UnitMovement movement = GetComponent<UnitMovement>();
        if(movement != null)
        {
            moveToDirEvent.AddListener(movement.OnMoveToDir);
            stopMoveEvent.AddListener(movement.OnStop);
            moveToPathEvent.AddListener(movement.OnMoveToPath);
        }
    }
    protected override void RemoveListeners()
    {
        UnitMovement movement = GetComponent<UnitMovement>();
        if (movement != null)
        {
            moveToDirEvent?.RemoveListener(movement.OnMoveToDir);
            stopMoveEvent.RemoveAllListeners();
            moveToPathEvent.RemoveAllListeners();
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
        stopMoveEvent?.Invoke();
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
        Vector2 dir = Vector2.zero;
        Vector2 temp = Vector2.zero;
        while (true) 
        {
            dir = Vector2.zero - (Vector2)transform.position;
            dir.Normalize();

            temp = (Vector2)transform.position + (dir * 6.0f);

            if (owner.ai.PathFinding(transform.position, temp, out Vector2[] path))
            {
                moveToPathEvent?.Invoke(path);
                yield return new WaitForSeconds(1.1f);
            }
            else
            {
                moveToDirEvent?.Invoke(dir);
                yield return new WaitForSeconds(1.1f);
            }
           
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