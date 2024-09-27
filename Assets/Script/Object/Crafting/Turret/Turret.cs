using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using static UnityEngine.GraphicsBuffer;

public class Turret : Structure
{
    #region Properties
    #region Private
    [SerializeField] private float turretAttackTimer; // ���� Ÿ�̸�
    [SerializeField] private float turretAttackCooldown; // ���� ��Ÿ��
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

    public float rotationSpeed = 5f; // ȸ�� �ӵ�
    public float minAngle = -60f; // ȸ�� ���� �ּ� ����
    public float maxAngle = 60f; // ȸ�� ���� �ִ� ����
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
            // Ÿ�� ���� ���
            Vector2 dir = targetEnemy.transform.position - this.transform.position;
            float targetAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
/*
            // ���� ���� ���
            float currentAngle = header.rotation.eulerAngles.z;

            // Convert angles to range [-180, 180]
            if (targetAngle > 180f) targetAngle -= 360f;
            if (currentAngle > 180f) currentAngle -= 360f;

            // Clamp targetAngle to the desired range [-60, 60] based on currentAngle
            float clampedAngle = Mathf.Clamp(targetAngle, -60f, 60f);

            // �ε巴�� ȸ��
            float newAngle = Mathf.LerpAngle(currentAngle, clampedAngle, Time.deltaTime * 5f); // Adjust 5f to control rotation speed
            Debug.Log(currentAngle + ", " + clampedAngle + " " + newAngle);
*/
            // ȸ�� �ӵ��� �����ϸ鼭 ���ο� ������ ȸ��
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

    // ���� ����� ���� ã�� �Լ�
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


    // ���� �����ϴ� �Լ�
    // ������ �´°ǵ� ����ü�� �������� ������
    public void Attack(GameObject enemy)
    {
        IDamage targetEnemy = enemy.GetComponent<IDamage>();
        //if (targetEnemy != null && bullet != null)
        if (targetEnemy != null)
        {
            animator.SetBool("Attack", true);
            headerImg.transform.gameObject.SetActive(true);
            //targetEnemy.TakeDamage(damage * GetEfficiency()); // ���� ������ ���¿� ���� �������� �޸� ��
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
    // ���� ������ �ð������� �����ִ� �Լ� (����׿�)
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
