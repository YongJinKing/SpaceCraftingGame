using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using static UnityEngine.GraphicsBuffer;

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
    public Transform headerImg;
    public List<GameObject> bulletList = new List<GameObject>();

    public float rotationSpeed = 5f; // 회전 속도
    public float minAngle = -60f; // 회전 가능 최소 각도
    public float maxAngle = 60f; // 회전 가능 최대 각도
    #endregion
    #region Events
    #endregion
    #endregion

    #region Constructor
    public Turret() : base()
    {
        AddStat(EStat.ATKSpeed, turretAttackTimer);
        AddStat(EStat.ATKDelay, turretAttackCooldown);
        AddStat(EStat.ATK, dmg);
    }
    #endregion

    #region OnEnable, Start, Update
    protected override void OnEnable()
    {
        base.OnEnable();
        /*turretAttackTimer = this[EStat.ATKSpeed];
        turretAttackCooldown = this[EStat.ATKDelay];*/
    }

    protected override void OnDead()
    {
        base.OnDead();
        headerImg.transform.gameObject.SetActive(false);
    }
    protected override void Initialize()
    {
        base.Initialize();

    }
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        dmg = this[EStat.ATK];
        turretAttackCooldown = this[EStat.ATKDelay];
        turretAttackTimer = this[EStat.ATKSpeed];
        MaxHP = this[EStat.MaxHP];
        perception = GetComponentInChildren<TurretPerception>();
        perception.detectEnemyEvents.AddListener(OnEnemyDetected);
        perception.lostEnemyEvents.AddListener(OnEnemyLost);
        this.DestroyEvent.AddListener(TileManager.Instance.DestoryObjectOnTile);

    }

    // Update is called once per frame
    void Update()
    {
        if (this[EStat.HP] <= 0f) return;

        turretAttackTimer -= Time.deltaTime;

        GameObject targetEnemy = FindClosestEnemy();

        if (targetEnemy != null)
        {
            // 타겟 방향 계산
            Vector2 dir = targetEnemy.transform.position - this.transform.position;
            float targetAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
/*
            // 현재 각도 계산
            float currentAngle = header.rotation.eulerAngles.z;

            // Convert angles to range [-180, 180]
            if (targetAngle > 180f) targetAngle -= 360f;
            if (currentAngle > 180f) currentAngle -= 360f;

            // Clamp targetAngle to the desired range [-60, 60] based on currentAngle
            float clampedAngle = Mathf.Clamp(targetAngle, -60f, 60f);

            // 부드럽게 회전
            float newAngle = Mathf.LerpAngle(currentAngle, clampedAngle, Time.deltaTime * 5f); // Adjust 5f to control rotation speed
            Debug.Log(currentAngle + ", " + clampedAngle + " " + newAngle);
*/
            // 회전 속도를 조절하면서 새로운 각도로 회전
            header.rotation = Quaternion.Euler(new Vector3(0,0,targetAngle));

            if (turretAttackTimer <= 0f)
            {
                Attack(targetEnemy);
                turretAttackTimer = turretAttackCooldown;
            }
        }
        else
        {
            headerImg.transform.gameObject.SetActive(false);
            animator.SetBool("Attack", false);
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


    // 적을 공격하는 함수
    // 무조건 맞는건데 투사체가 보였으면 좋겠음
    public void Attack(GameObject enemy)
    {
        IDamage targetEnemy = enemy.GetComponent<IDamage>();
        //if (targetEnemy != null && bullet != null)
        if (targetEnemy != null)
        {
            animator.SetBool("Attack", true);
            headerImg.transform.gameObject.SetActive(true);
            //targetEnemy.TakeDamage(damage * GetEfficiency()); // 현재 내구도 상태에 따라 데미지를 달리 줌
            //targetEnemy.TakeDamage(damage);

            /*for (int i = 0; i < bulletList.Count; i++)
            {
                if (!bulletList[i].gameObject.activeSelf)
                {
                    var towerBullet = bulletList[i];
                    towerBullet.SetActive(true);
                    towerBullet.transform.position = attackPoint.position;
                    //towerBullet.transform.SetParent(null);
                    towerBullet.GetComponent<TestBullet>().SetTarget(enemy.transform);
                    //towerBullet.GetComponent<TestBullet>().SetDamage(this[EStat.ATK]);
                    towerBullet.GetComponent<TestBullet>().SetDamage(dmg);
                    towerBullet.GetComponent<TestBullet>().SetRotation(enemy.transform);
                    break;
                }
            }*/
            //var towerBullet = Instantiate(bullet, attackPoint.transform.position, Quaternion.identity);
            var towerBullet = ObjectPool.Instance.GetObject<TestBullet>(bullet.gameObject, attackPoint);
            //towerBullet.SetActive(true);
            towerBullet.transform.position = attackPoint.position;
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
