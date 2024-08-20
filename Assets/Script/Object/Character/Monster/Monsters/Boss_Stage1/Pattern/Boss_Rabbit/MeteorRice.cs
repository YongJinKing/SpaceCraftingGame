using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorRice : MonoBehaviour
{
    public LayerMask layerMask;
    public Transform brokenVFX;

    private Vector3 targetPos;
    private Rigidbody2D rb;


    public void Initialize(Vector3 target)
    {
        this.targetPos = target;
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {

        // 목표 위치를 향해 레이캐스트를 사용하여 충돌 검사
        // 목표 위치에 도달했는지 확인

        Vector2 direction = rb.velocity.normalized;
        float distance = rb.velocity.magnitude * Time.fixedDeltaTime;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, distance);

        if (hit.collider != null && hit.point.y <= targetPos.y)
        {
            // 목표 위치에 도달했는지 확인
            //Collider2D hitCollider = Physics2D.OverlapPoint(targetPos, layerMask);
            Collider2D hitCollider = Physics2D.OverlapCircle(targetPos, 0.5f, layerMask);
            if (hitCollider != null)
            {
                // 플레이어가 있다면 데미지 처리
                Unit player = hitCollider.GetComponent<Unit>();
                if (player != null)
                {
                    Debug.Log("플레이어 히트!");
                    player.TakeDamage(1f);
                }
            }
            // 물체를 제거
            //Destroy(gameObject);
            Release();
            SoundManager.Instance.PlaySFX(SoundManager.Instance.soundData.bossSpinAttack, false);
            // 여기서 터지는 무언가를 생성한다. 터지는 이펙트 같은거.
            //Instantiate(brokenVFX, gameObject.transform.position, Quaternion.identity, null);
            var obj = ObjectPool.Instance.GetObject(brokenVFX.gameObject.name, brokenVFX.gameObject, gameObject.transform);
            obj.transform.SetParent(null);
        }

    }

    void Release()
    {
        ObjectPool.Instance.ReleaseObject<MeteorRice>(gameObject);
    }

}
