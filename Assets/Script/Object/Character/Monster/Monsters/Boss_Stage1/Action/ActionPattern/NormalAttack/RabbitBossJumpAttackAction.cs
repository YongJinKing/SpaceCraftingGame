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
