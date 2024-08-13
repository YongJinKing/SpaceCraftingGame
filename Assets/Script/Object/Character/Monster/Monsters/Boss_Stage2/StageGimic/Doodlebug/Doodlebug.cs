using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doodlebug : MonoBehaviour, IDamage
{
    [SerializeField] GameObject target;
    [SerializeField] float pullPower = 1f;
    [SerializeField] float MaxHP;
    [SerializeField] float HP;
    Rigidbody2D rb;

    void Start()
    {
        HP = MaxHP;
    }

    void FixedUpdate()
    {
        if (target == null) return;

        Vector2 direction = (transform.position - target.transform.position).normalized;

        //rb.AddForce(direction * pullPower); // << �̰� ���� ���������� ���� ���� ��� �ƽ��ƽ��ϰ� ���� �ȿ� ���������� ���߿� ���� �ʹ� ���̹޾� ���� ���������� ���ϰ� ��, �̰� pullPower�� 20~25 ������ �־���
        rb.velocity = direction * pullPower; // << �̰� ������ ���� ���� ���� �ȿ� �󸶳� �ִ� ������ ���� �� ����, �̰� pullPower�� 1 ������ �־���
    }

    public void SetTarget(GameObject _target)
    {
        target = _target;
        rb = target.GetComponent<Rigidbody2D>();
    }

    public void MissTarget()
    {
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
        }
        target = null;
        rb = null;
    }

    public void KillTarget()
    {
        if (target != null)
        {
            target.GetComponent<IDamage>().TakeDamage(1000f); // �׳� ����Ű�� �ڵ�, �ȿ� 1000f�� ��Ȯ�� �󸶳� ��� ������ ���� ���� ����
            MissTarget(); // ���� �ڿ� �׳� null�� �����ֱ�
        }
    }

    public void TakeDamage(float damage)
    {
        HP -= damage;

        if(HP <= 0.0f)
        {
            MissTarget();
            Destroy(this.gameObject);
        }
    }
}
