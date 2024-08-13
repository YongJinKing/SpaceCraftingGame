using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GimicStone : MonoBehaviour
{
    public LayerMask layerMask;
    public Transform brokenVFX;

    private Vector3 targetPos;
    private Rigidbody2D rb;

    bool arrive;

    public void Initialize(Vector3 target)
    {
        this.targetPos = target;
        rb = GetComponent<Rigidbody2D>();
        arrive = false;
    }

    private void FixedUpdate()
    {
        if (arrive) return;
        // 목표 위치를 향해 레이캐스트를 사용하여 충돌 검사
        // 목표 위치에 도달했는지 확인

        Vector2 direction = rb.velocity.normalized;
        float distance = rb.velocity.magnitude * Time.fixedDeltaTime;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, distance);

        if (hit.collider != null && hit.point.y <= targetPos.y)
        {
            // 목표 위치에 도달했는지 확인
            //Collider2D hitCollider = Physics2D.OverlapPoint(targetPos, layerMask);
            Collider2D hitCollider = Physics2D.OverlapCircle(targetPos, 2f, layerMask);
            if (hitCollider != null)
            {
                // 플레이어가 있다면 데미지 처리
                Unit player = hitCollider.GetComponent<Unit>();
                if (player != null)
                {
                    Debug.Log("플레이어 히트!");
                    player.TakeDamage(1f);

                    Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
                    if (playerRb != null)
                    {
                        // 플레이어를 밀어낼 방향 계산 (바위의 위치에서 플레이어 위치 방향으로 밀어냄)
                        Vector2 knockbackDirection = (player.transform.position - targetPos).normalized;

                        // 힘의 크기 설정 (필요에 따라 크기를 조정)
                        float knockbackForce = 5f;

                        // 플레이어에게 힘을 가해 밀어내기
                        playerRb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);

                        // 일정 시간 후에 힘을 0으로 만들어 더 이상 밀리지 않도록 함 (밀려나는 시간을 조정할 수 있음)
                        float knockbackDuration = 0.5f; // 0.5초 후에 밀리지 않도록 설정
                        StartCoroutine(StopKnockback(playerRb, knockbackDuration));
                    }
                }
            }
            rb.gravityScale = 0f;
            rb.velocity = Vector3.zero;
            this.transform.position = targetPos;
            arrive = true;
            // 여기서 터지는 무언가를 생성한다. 터지는 이펙트 같은거.
            //Instantiate(brokenVFX, gameObject.transform.position, Quaternion.identity, null);
        }

    }

    // 플레이어를 일정 시간 후에 멈추게 하는 코루틴 << 이건 여기에 둬야할지 플레이어한테 둬야할 지 잘 모르겠음
    private IEnumerator StopKnockback(Rigidbody2D rb, float duration)
    {
        yield return new WaitForSeconds(duration);
        rb.velocity = Vector2.zero; // 현재 속도를 0으로 설정
    }

}
