using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public float forceAmount = 10f;  // ���� ũ��
    public float destroytime = 10f;

    private Rigidbody2D rb2D;

    void Start()
    {
        // Rigidbody ������Ʈ�� ������
        rb2D = GetComponent<Rigidbody2D>();

        // Rigidbody�� �ִ��� Ȯ��
        if (rb2D != null)
        {
            // ������Ʈ�� ������ ���� ����
            rb2D.AddForce(transform.right * forceAmount, ForceMode2D.Impulse);
            Destroy(gameObject, destroytime);
        }
    }
}
