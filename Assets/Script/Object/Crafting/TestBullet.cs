using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBullet : MonoBehaviour
{
    public LayerMask layerMask;
    public Transform target;
    public float damage = 0f;
    CircleCollider2D targetCollider;
    public Transform destroyVFX;
    bool readyToFire = false;

    private void OnEnable()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        if (target == null || !targetCollider.enabled || !readyToFire)
        {
            Release();
            return;
        }
        
        Vector2 dir = target.position - transform.position;
        float dist = dir.magnitude;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (dist > 1.0f) this.transform.rotation = Quaternion.Euler(0, 0, angle);

        this.gameObject.transform.Translate(Vector2.right * Time.deltaTime * 9f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((1 << collision.gameObject.layer & layerMask) != 0)
        {
            IDamage character = collision.gameObject.GetComponent<IDamage>();
            character.TakeDamage(damage);
            //Destroy(this.gameObject);
            //this.gameObject.SetActive(false);
            Release();
        }
    }

    public void SetTarget(Transform _target)
    {
        this.target = _target;
        targetCollider = target.GetComponent<CircleCollider2D>();
    }

    public void SetDamage(float _damage)
    {
        this.damage = _damage;
    }

    public void SetRotation(Transform _target)
    {
        if (_target == null) return;
        Vector2 dir = _target.position - transform.position;
        float dist = dir.magnitude;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (dist > 1.0f) this.transform.rotation = Quaternion.Euler(0, 0, -angle);
        readyToFire = true;
    }

    public void LostTarget()
    {
        this.gameObject.SetActive(false);
    }
    void Release()
    {
        Instantiate(destroyVFX, this.transform.position, Quaternion.identity, null);
        ObjectPool.Instance.ReleaseObject<TestBullet>(gameObject);
    }
}
