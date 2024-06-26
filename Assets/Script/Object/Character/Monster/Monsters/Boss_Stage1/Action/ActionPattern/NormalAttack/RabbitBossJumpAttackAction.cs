using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class RabbitBossJumpAttackAction : BossAction
{
    #region Properties
    #region Private
    private Rigidbody2D rb;
    private Vector2 startPos;

    #endregion
    #region Protected
    #endregion
    #region Public
    public Vector2 targetPosition; // 목표 지점의 위치
    public float jumpHeight = 2.5f; // 점프 높이
    public float gravity = -9.81f; // 중력

    #endregion
    #region Events
    #endregion
    #endregion

    #region Constructor
    public RabbitBossJumpAttackAction()
    {
        fireAndForget = false;
    }
    #endregion

    #region Methods
    #region Private
    IEnumerator HitBoxOn(Vector2 pos)
    {
        for (int i = 0; i < hitBoxes.Length; ++i)
        {
            hitBoxes[i].Activate(pos);
        }
        yield return null;
    }
    
    // 현재 토끼의 위치로부터 타겟의 위치까지 height의 높이로 duration의 시간만큼 포물선 이동을 구현
    // 타겟의 위치에 다다르면 히트박스를 킨다.
    IEnumerator JumpToTarget(Vector2 start, Vector2 target, float height, float duration)
    {
        float jumpTime = 0;
        //AsyncAnimation(0,false);
        ownerAnim.SetTrigger("JumpAttack");
        while (jumpTime < duration)
        {
            jumpTime += Time.deltaTime;
            float t = jumpTime / duration;
            float x = Mathf.Lerp(start.x, target.x, t);
            float y = Mathf.Lerp(start.y, target.y, t) + height * Mathf.Sin(Mathf.PI * t);
            rb.position = new Vector2(x, y);
            yield return null;
        }
        rb.position = target; // 정확한 위치 보정

        yield return StartCoroutine(HitBoxOn(target));
        //ActionEnd();
    }

    // 포물선을 그리며 점프할 때, 주어진 높이와 목표 위치로 도달하기 위한 초기 속도를 계산하는 함수 >> Rigidbody2D gravityScale을 사용
    // >> 버그가 잦아 일단 보류
    Vector2 CalculateJumpVelocity(Vector2 start, Vector2 end, float height)
    {
        float displacementX = end.x - start.x; // 목표 위치와 시작 위치 사이의 수평 거리
        float displacementY = end.y - start.y; // 목표 위치와 시작 위치 사이의 수직 거리

        // 최대 높이에 도달하는 데 걸리는 시간 계산
        float sqrtTerm1 = Mathf.Sqrt(-2 * height / gravity);
        // 목표 위치까지 내려가는 데 걸리는 시간 계산
        float sqrtTerm2 = Mathf.Sqrt(2 * (displacementY - height) / gravity);

        // sqrtTerm1과 sqrtTerm2가 유효한지 확인
        if (float.IsNaN(sqrtTerm1) || float.IsNaN(sqrtTerm2))
        {
            return Vector2.zero;
        }

        float time = sqrtTerm1 + sqrtTerm2;

        // time이 유효한지 확인
        if (time <= 0)
        {
            return Vector2.zero;
        }

        float velocityX = displacementX / time; // 수평 변위를 전체 비행 시간으로 나눈 값 >> 수평 속도
        float velocityY = Mathf.Sqrt(-2 * gravity * height); // 최대 높이에 도달하기 위한 초기 속도 >> 수직속도

        // velocityY가 유효한지 확인
        if (float.IsNaN(velocityY))
        {
            return Vector2.zero;
        }

        return new Vector2(velocityX, velocityY); // 계산된 수평 속도 (velocityX)와 수직 속도 (velocityY)를 포함하는 Vector2를 반환
    }

    #endregion
    #region Protected
    protected override void ActionEnd()
    {
        base.ActionEnd();
        StopAllCoroutines();
    }
    #endregion
    #region Public
    public override void Activate(Vector2 pos)
    {
        base.Activate(pos);
        rb = transform.parent.GetComponentInParent<Rigidbody2D>();
        rb.gravityScale = 0;
        //StartCoroutine(JumpToTarget(pos));
        StartCoroutine(JumpToTarget(transform.position, pos, 2.5f, 1f));
        // 포물선을 그리면서 점프를 하는 함수를 만들고
        // 그 함수가 끝날 때쯤 히트박스를 킨다.
        
    }
    public override void Deactivate()
    {
        for (int i = 0; i < hitBoxes.Length; ++i)
        {
            hitBoxes[i].Deactivate();
        }
        ActionEnd();
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
