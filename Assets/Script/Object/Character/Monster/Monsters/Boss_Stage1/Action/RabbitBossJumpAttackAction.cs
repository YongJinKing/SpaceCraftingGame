using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RabbitBossJumpAttackAction : AttackAction
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
    IEnumerator JumpToTarget(Vector2 pos)
    {
        startPos = transform.position;
        Vector2 velocity = CalculateJumpVelocity(startPos, pos, jumpHeight); // 필요한 속도 계산
        rb.velocity = velocity;
        rb.gravityScale = 1; // 점프 시작 시 중력 활성화

        // 목표 지점에 도착할 때까지 대기
        while (true)
        {
            if (rb.velocity.y < 0)
            {
                Vector2 currentPosition = transform.position;
                if (currentPosition.y <= pos.y && currentPosition.x >= pos.x) // 해당 위치에 다다르면
                {
                    rb.velocity = Vector2.zero;
                    rb.gravityScale = 0;
                    break;
                }
            }
            yield return null; // 다음 프레임까지 대기
        }

        yield return StartCoroutine(HitBoxOn(pos));
    }
    // 포물선을 그리며 점프할 때, 주어진 높이와 목표 위치로 도달하기 위한 초기 속도를 계산하는 함수
    Vector2 CalculateJumpVelocity(Vector2 start, Vector2 end, float height)
    {
        float displacementX = end.x - start.x; // 목표 위치와 시작 위치 사이의 수평 거리
        float displacementY = end.y - start.y; // 목표 위치와 시작 위치 사이의 수직 거리
        float time = Mathf.Sqrt(-2 * height / gravity) + Mathf.Sqrt(2 * (displacementY - height) / gravity);
        // 점프가 최대 높이에 도달하는 데 걸리는 시간 + 대 높이에서 목표 위치까지 내려가는 데 걸리는 시간 <<< sqrt(2h / g) + sqrt(2(y - h) / g) 중력이 음수로 설정되어 -를 붙임
        // 

        float velocityX = displacementX / time; // 수평 변위를 전체 비행 시간으로 나눈 값 >> 수평 속도
        float velocityY = Mathf.Sqrt(-2 * gravity * height); //  최대 높이에 도달하기 위한 초기 속도 >> 수직속도, 여기서도 중력이 음수로 설정 되어 - 를 붙임

        return new Vector2(velocityX, velocityY); // 계산된 수평 속도 (velocityX)와 수직 속도 (velocityY)를 포함하는 Vector2를 반환
    }

    #endregion
    #region Protected
    protected override void ActionEnd()
    {
        base.ActionEnd();
    }
    #endregion
    #region Public
    public override void Activate(Vector2 pos)
    {
        base.Activate(pos);
        rb = transform.parent.GetComponentInParent<Rigidbody2D>();
        rb.gravityScale = 0;
        StartCoroutine(JumpToTarget(pos));
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
