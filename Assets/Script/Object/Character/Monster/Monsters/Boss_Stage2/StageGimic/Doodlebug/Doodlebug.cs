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

        //rb.AddForce(direction * pullPower); // << 이건 힘을 지속적으로 가해 만약 계속 아슬아슬하게 범위 안에 걸쳐있으면 나중엔 힘을 너무 많이받아 절대 빠져나가지 못하게 됨, 이건 pullPower를 20~25 정도로 둬야함
        rb.velocity = direction * pullPower; // << 이건 일정한 힘을 가해 범위 안에 얼마나 있던 밖으로 나갈 수 있음, 이건 pullPower를 1 정도로 둬야함
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
            target.GetComponent<IDamage>().TakeDamage(1000f); // 그냥 즉사시키는 코드, 안에 1000f는 정확히 얼마나 줘야 죽을지 몰라서 대충 넣음
            MissTarget(); // 죽인 뒤엔 그냥 null로 보내주기
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
