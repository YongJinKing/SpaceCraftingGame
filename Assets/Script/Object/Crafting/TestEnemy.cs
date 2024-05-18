using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TestEnemy : MonoBehaviour, IDamage
{
    public SpriteRenderer spriteRenderer;
    public Transform Target;
    public float maxHp = 100f;
    private float curHp;

    // 적이 죽었을 때 알릴 이벤트
    public UnityEvent<GameObject> onEnemyDeath;

    void Start()
    {
        curHp = maxHp;
    }

    private void Update()
    {
        Vector2 dir = transform.position - Target.position;
        dir.Normalize();

        transform.Translate(-dir * 2f * Time.deltaTime);
    }

    public void TakeDamage(float _dmg)
    {
        curHp -= _dmg;
        StartCoroutine(DamaginEffect());
        if (curHp <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // 적이 죽었을 때 터렛에게 알림
        onEnemyDeath?.Invoke(gameObject);
        Destroy(gameObject);
    }

    IEnumerator DamaginEffect()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = Color.white;
    }
}
