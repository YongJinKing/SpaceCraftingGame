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
    public Transform attackEffect;
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
        attackEffect.gameObject.SetActive(true);

        for (int i = 0; i < hitBoxes.Length; ++i)
        {
            hitBoxes[i].Activate(pos);

            Quaternion rot = hitBoxes[i].transform.rotation;

            Vector3 eulerRotation = rot.eulerAngles;
            eulerRotation.z += 180f;
            attackEffect.gameObject.transform.rotation = Quaternion.Euler(eulerRotation);
        }

        attackEffect.GetComponent<ParticleSystem>().Play();
        camController.StartCameraShake(3f,0.5f);
        yield return null;
    }
    
    // 현재 토끼의 위치로부터 타겟의 위치까지 height의 높이로 duration의 시간만큼 포물선 이동을 구현
    // 타겟의 위치에 다다르면 히트박스를 킨다.
    IEnumerator JumpToTarget(Vector2 start, Vector2 target, float height, float duration)
    {
        SetRabbitLookPlayer(target);
        float jumpTime = 0;

        // 플레이어의 위치를 보정하여 타겟보다 앞쪽으로 떨어지게 함
        Vector2 direction = (target - start).normalized;  // 타겟 방향 벡터
        float offsetDistance = 2f;  // 히트박스가 플레이어에 맞도록 조정할 거리
        Vector2 adjustedTarget = target - direction * offsetDistance;

        // 애니메이션 트리거
        ownerAnim.SetTrigger("JumpAttack");

        while (jumpTime < duration)
        {
            jumpTime += Time.deltaTime;
            float t = jumpTime / duration;

            // 포물선 이동 계산
            float x = Mathf.Lerp(start.x, adjustedTarget.x, t);
            float y = Mathf.Lerp(start.y, adjustedTarget.y, t) + height * Mathf.Sin(Mathf.PI * t);
            rb.position = new Vector2(x, y);
            yield return null;
        }

        // 정확한 위치 보정
        rb.position = adjustedTarget;

        // 착지 후 히트박스 활성화
        yield return StartCoroutine(HitBoxOn(target));
    }

    #endregion
    #region Protected
    protected override void ActionEnd()
    {
        base.ActionEnd();
        StopAllCoroutines();
        if (owner.GetComponent<Rigidbody2D>().velocity != Vector2.zero) owner.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }
    #endregion
    #region Public
    public override void Activate(Vector2 pos)
    {
        base.Activate(pos);
        //rb = transform.parent.GetComponentInParent<Rigidbody2D>();
        rb = owner.transform.GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        StartCoroutine(JumpToTarget(transform.position, pos, 2.5f, 1f));
        // 포물선을 그리면서 점프를 하는 함수를 만들고
        // 그 함수가 끝날 때쯤 히트박스를 킨다.
        
    }
    public override void Deactivate()
    {
        attackEffect.GetComponent<ParticleSystem>().Stop();
        attackEffect.gameObject.SetActive(false);
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
