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
        // ��ǥ ��ġ�� ���� ����ĳ��Ʈ�� ����Ͽ� �浹 �˻�
        // ��ǥ ��ġ�� �����ߴ��� Ȯ��

        Vector2 direction = rb.velocity.normalized;
        float distance = rb.velocity.magnitude * Time.fixedDeltaTime;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, distance);

        if (hit.collider != null && hit.point.y <= targetPos.y)
        {
            // ��ǥ ��ġ�� �����ߴ��� Ȯ��
            //Collider2D hitCollider = Physics2D.OverlapPoint(targetPos, layerMask);
            Collider2D hitCollider = Physics2D.OverlapCircle(targetPos, 2f, layerMask);
            if (hitCollider != null)
            {
                // �÷��̾ �ִٸ� ������ ó��
                Unit player = hitCollider.GetComponent<Unit>();
                if (player != null)
                {
                    Debug.Log("�÷��̾� ��Ʈ!");
                    player.TakeDamage(1f);

                    Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
                    if (playerRb != null)
                    {
                        // �÷��̾ �о ���� ��� (������ ��ġ���� �÷��̾� ��ġ �������� �о)
                        Vector2 knockbackDirection = (player.transform.position - targetPos).normalized;

                        // ���� ũ�� ���� (�ʿ信 ���� ũ�⸦ ����)
                        float knockbackForce = 5f;

                        // �÷��̾�� ���� ���� �о��
                        playerRb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);

                        // ���� �ð� �Ŀ� ���� 0���� ����� �� �̻� �и��� �ʵ��� �� (�з����� �ð��� ������ �� ����)
                        float knockbackDuration = 0.5f; // 0.5�� �Ŀ� �и��� �ʵ��� ����
                        StartCoroutine(StopKnockback(playerRb, knockbackDuration));
                    }
                }
            }
            rb.gravityScale = 0f;
            rb.velocity = Vector3.zero;
            this.transform.position = targetPos;
            arrive = true;
            // ���⼭ ������ ���𰡸� �����Ѵ�. ������ ����Ʈ ������.
            //Instantiate(brokenVFX, gameObject.transform.position, Quaternion.identity, null);
        }

    }

    // �÷��̾ ���� �ð� �Ŀ� ���߰� �ϴ� �ڷ�ƾ << �̰� ���⿡ �־����� �÷��̾����� �־��� �� �� �𸣰���
    private IEnumerator StopKnockback(Rigidbody2D rb, float duration)
    {
        yield return new WaitForSeconds(duration);
        rb.velocity = Vector2.zero; // ���� �ӵ��� 0���� ����
    }

}
