using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BossAlertState : BossState
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
        //StartCoroutine(ProcessingState()); // 이동이 끝난 뒤에 State변경을 해야함..
        StartCoroutine(FollowAndRotate());
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
    private IEnumerator FollowAndRotate()
    {
        while (true)
        {
            // 1. 플레이어에게 일정 거리까지 다가간다.
            yield return StartCoroutine(MoveTowardsTarget());

            // 2. 일정 거리에 다다르면 플레이어를 중심으로 회전한다.
            yield return StartCoroutine(RotateAroundTarget());

            // 3. 회전 후 다시 플레이어에게 다가간다.
        }
    }

    private IEnumerator MoveTowardsTarget()
    {
        while (true)
        {
            Vector2 dir = (Vector2)owner.target.transform.position - (Vector2)transform.position;
            float dist = dir.magnitude;
            if (dist <= 5f)
            {
                yield break;
            }

            dir.Normalize();
            transform.Translate(dir * 1f * Time.deltaTime, Space.World);
            yield return null;
        }
    }
    private IEnumerator RotateAroundTarget()
    {
        float startTime = Time.time;
        float rotateTime = Random.Range(2f, 5f);
        Vector2 initialPosition = transform.position;
        Vector2 targetPosition = owner.target.transform.position; // 회전 시작 시의 플레이어 위치 고정
        float angle = 0f;

        Vector2 relativePosition = initialPosition - targetPosition;
        float initialAngle = Mathf.Atan2(relativePosition.y, relativePosition.x);

        while (Time.time - startTime < rotateTime)
        {
            //angle += 1f * Time.deltaTime;

            angle = initialAngle + 1f * (Time.time - startTime);

            // 새로운 위치 계산
            float x = Mathf.Cos(angle) * 5f;
            float y = Mathf.Sin(angle) * 5f;

            transform.position = new Vector3(targetPosition.x + x, targetPosition.y + y, transform.position.z);
            yield return null;
        }

        // 회전이 끝난 후 다시 초기 위치로 돌아감
        //transform.position = initialPosition;
    }

    protected IEnumerator ProcessingState()
    {
        //Wait until Select Action
        /*yield return StartCoroutine(SelectingAction());

        owner.stateMachine.ChangeState<MonsterAttackState>();*/
        Debug.Log("Boss Processing State");
        yield return new WaitForSeconds(1f);

        //yield return StartCoroutine(FollowingTarget());
    }

    protected IEnumerator MovingCirclularPos(Vector2 initialPos)
    {
        Debug.Log("무빙 서큘러 포스");
        float moveTime = Random.Range(2f, 5f);
        float angle = 0.0f; // 각도
        while (moveTime > 0f)
        {

            angle += 1f * Time.deltaTime;

            // 새로운 위치 계산
            float x = Mathf.Cos(angle) * 4f;
            float y = Mathf.Sin(angle) * 4f;

            // 새로운 위치 설정
            transform.position = new Vector3(initialPos.x + x, initialPos.y + y, transform.position.z);
            yield return null;
            moveTime -= Time.deltaTime;
        }

        yield return StartCoroutine(ProcessingState());
    }
    #endregion

    #region MonoBehaviour
    #endregion
}
