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

        // ��ǥ ��ġ�� ���� ����ĳ��Ʈ�� ����Ͽ� �浹 �˻�
        // ��ǥ ��ġ�� �����ߴ��� Ȯ��

        Vector2 direction = rb.velocity.normalized;
        float distance = rb.velocity.magnitude * Time.fixedDeltaTime;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, distance);

        if (hit.collider != null && hit.point.y <= targetPos.y)
        {
            // ��ǥ ��ġ�� �����ߴ��� Ȯ��
            //Collider2D hitCollider = Physics2D.OverlapPoint(targetPos, layerMask);
            Collider2D hitCollider = Physics2D.OverlapCircle(targetPos, 0.5f, layerMask);
            if (hitCollider != null)
            {
                // �÷��̾ �ִٸ� ������ ó��
                Unit player = hitCollider.GetComponent<Unit>();
                if (player != null)
                {
                    Debug.Log("�÷��̾� ��Ʈ!");
                    player.TakeDamage(1f);
                }
            }
            // ��ü�� ����
            //Destroy(gameObject);
            Release();
            SoundManager.Instance.PlaySFX(SoundManager.Instance.soundData.bossSpinAttack, false);
            // ���⼭ ������ ���𰡸� �����Ѵ�. ������ ����Ʈ ������.
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
