using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : Structure
{
    #region Properties
    #region Private
    [SerializeField] private float turretAttackTimer; // 공격 타이머
    [SerializeField] private float turretAttackCooldown; // 공격 쿨타임
    #endregion
    #region Protected
    [SerializeField] protected TurretPerception perception;
    [SerializeField] protected float dmg;
    #endregion
    #region Public
    public Transform bullet;
    public Transform attackPoint;
    public Transform header;
    #endregion
    #region Events
    #endregion
    #endregion

    #region Constructor
    public Turret() : base()
    {
        
    }
    #endregion

    #region OnEnable, Start, Update
    private void OnEnable()
    {
        perception = GetComponentInChildren<TurretPerception>();
        perception.detectEnemyEvents.AddListener(OnEnemyDetected);
        perception.lostEnemyEvents.AddListener(OnEnemyLost);

        /*turretAttackTimer = this[EStat.ATKSpeed];
        turretAttackCooldown = this[EStat.ATKDelay];*/
    }
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        AddStat(EStat.ATKSpeed, turretAttackTimer);
        AddStat(EStat.ATKDelay, turretAttackCooldown);
        AddStat(EStat.ATK, dmg);
    }

    // Update is called once per frame
    void Update()
    {
        turretAttackTimer -= Time.deltaTime;

        if (turretAttackTimer <= 0f)
        {
            GameObject targetEnemy = FindClosestEnemy();

            if (targetEnemy != null)
            {
                Vector2 dir = targetEnemy.transform.position - attackPoint.transform.position;
                float targetAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                float currentAngle = header.rotation.eulerAngles.z;

                // Convert angles to range [-180, 180]
                targetAngle = (targetAngle + 360f) % 360f;
                currentAngle = (currentAngle + 360f) % 360f;
                if (targetAngle > 180f) targetAngle -= 360f;
                if (currentAngle > 180f) currentAngle -= 360f;

                // Clamp targetAngle to the desired range [-60, 60] based on currentAngle
                float clampedAngle = Mathf.Clamp(targetAngle, -60f, 60f);

                // Smoothly rotate towards the clamped angle
                float newAngle = Mathf.LerpAngle(currentAngle, clampedAngle, Time.deltaTime * 5f); // Adjust 5f to control rotation speed
                header.rotation = Quaternion.Euler(0, 0, newAngle);

                Attack(targetEnemy);
                turretAttackTimer = turretAttackCooldown;
            }
        }
    }
    #endregion

    #region Method
    #region private
    void OnEnemyDetected(GameObject enemy)
    {
        /*TestEnemy targetEnemy = enemy.GetComponent<TestEnemy>();
        if (targetEnemy != null)
        {
            targetEnemy.onEnemyDeath.AddListener(RemoveEnemyFromList);
        }*/
    }

    void OnEnemyLost(GameObject enemy)
    {
        RemoveEnemyFromList(enemy);
    }

    void RemoveEnemyFromList(GameObject enemy)
    {
        perception.GetDetectedEnemies().Remove(enemy);
    }

    // 가장 가까운 적을 찾는 함수
    GameObject FindClosestEnemy()
    {
        List<GameObject> enemies = perception.GetDetectedEnemies();
        GameObject closestEnemy = null;
        float shortestDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            if (enemy != null)
            {
                float distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position);

                if (distanceToEnemy < shortestDistance)
                {
                    shortestDistance = distanceToEnemy;
                    closestEnemy = enemy;
                }
            }
        }

        return closestEnemy;
    }

    #endregion
    #region public
    public override void TakeDamage(float damage)
    {
        float dmg = damage - this[EStat.DEF];
        if (dmg <= 0.0f) dmg = 1f;
        this[EStat.HP] = dmg;
    }

    // 적을 공격하는 함수
    // 무조건 맞는건데 투사체가 보였으면 좋겠음
    void Attack(GameObject enemy)
    {
        IDamage targetEnemy = enemy.GetComponent<IDamage>();

        if (targetEnemy != null && bullet != null)
        {
            //targetEnemy.TakeDamage(damage * GetEfficiency()); // 현재 내구도 상태에 따라 데미지를 달리 줌
            //targetEnemy.TakeDamage(damage);
            var towerBullet = Instantiate(bullet, attackPoint.transform.position, Quaternion.identity);
            towerBullet.transform.SetParent(null);
            towerBullet.GetComponent<TestBullet>().SetTarget(enemy.transform);
            //towerBullet.GetComponent<TestBullet>().SetDamage(this[EStat.ATK]);
            towerBullet.GetComponent<TestBullet>().SetDamage(dmg);
            towerBullet.GetComponent<TestBullet>().SetRotation(enemy.transform);

        }
    }
    #endregion
    #endregion

    #region For_Debug
#if UNITY_2022_1_OR_NEWER
    // 공격 범위를 시각적으로 보여주는 함수 (디버그용)
    void OnDrawGizmosSelected()
    {
        if (perception != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, perception.GetComponent<CircleCollider2D>().radius);
        }
    }
#endif
    #endregion
}
