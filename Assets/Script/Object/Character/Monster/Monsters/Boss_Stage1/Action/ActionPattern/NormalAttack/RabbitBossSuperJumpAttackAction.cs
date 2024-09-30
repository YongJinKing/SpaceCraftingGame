using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RabbitBossSuperJumpAttackAction : BossAction
{
    #region Properties
    #region Private
    Vector2 targetPos;
    #endregion
    #region Protected
    #endregion
    #region Public
    public GameObject fallPosImage;
    public Transform attackVFX;
    public float jumpHeight = 20f; // �䳢�� �����ϴ� ����
    public float delayBeforeFall = 2f; // �䳢�� �������� ���� ����ϴ� �ð�
    public float gravity = -9.8f; // �߷� ���ӵ�
    [SerializeField] [Header("�������� �ӵ�"), Tooltip("�󸶳� ������ ������ �� ���� ����"), Space(.5f)]float fallPower;
    #endregion
    #region Events
    #endregion
    #endregion

    #region Constructor
    public RabbitBossSuperJumpAttackAction()
    {
        fireAndForget = false;
        
    }
    #endregion

    #region Methods
    #region Private
    IEnumerator HitBoxOn(Vector2 pos)
    {
        for (int i = 0; i < hitBoxes.Length; ++i)
        {
            hitBoxes[i].Activate(pos);
        }

        attackVFX.GetComponent<ParticleSystem>().Play();
        camController.StartCameraShake(5f, 0.7f);
        
        yield return null;
    }

    IEnumerator SuperJumpingAttack()
    {
        Rigidbody2D rb = owner.GetComponent<Rigidbody2D>();
        Transform rabbit = owner.transform;

        // �ʱ� �ӵ� ����
        float verticalSpeed = jumpHeight;

        // ȭ�� ������ �ö󰡱�
        while (rb.position.y < Camera.main.orthographicSize + 10)
        {
            verticalSpeed += gravity * Time.deltaTime; // �߷� ���ӵ� ����

            rb.velocity = new Vector2(rb.velocity.x, verticalSpeed);
            rb.position = new Vector2(rb.position.x, rb.position.y + rb.velocity.y * Time.deltaTime);
            yield return null;
        }

        rb.velocity = Vector2.zero; // �ӵ� �ʱ�ȭ

        // �÷��̾� ��ġ ǥ��
        targetPos = FindFirstObjectByType<Player>().gameObject.transform.position;

        rabbit.position = new Vector2(targetPos.x, Camera.main.orthographicSize + 10);
        GameObject obj = Instantiate(fallPosImage, targetPos, Quaternion.identity);
        // ���� �̹����� ������ n�� �ڿ� ������ �������� �����ϰ� ��ũ��Ʈ�� §��.

        // ��� �ð�
        yield return new WaitForSeconds(delayBeforeFall);

        // �߷� ���ӵ��� �÷��̾� ��ġ�� ��������
        ownerAnim.SetBool("SuperJumping", true);
        verticalSpeed = 0f; // �ӵ� �ʱ�ȭ
        while (rabbit.position.y > targetPos.y)
        {
            verticalSpeed += gravity * Time.deltaTime * fallPower; // �߷� ���ӵ� ����
            rabbit.position = new Vector2(rabbit.position.x, rabbit.position.y + verticalSpeed * Time.deltaTime);
            yield return null;
        }
        
        // �䳢 ��ġ�� �÷��̾� ��ġ�� ����
        rabbit.position = targetPos;
        ownerAnim.SetBool("SuperJumping", false);
        rb.velocity = Vector2.zero; // ������ �ӵ� �ʱ�ȭ
        StartCoroutine(HitBoxOn(targetPos));
    }

    #endregion
    #region Protected
    protected override void ActionEnd()
    {
        base.ActionEnd();
        if (ownerAnim.GetBool("SuperJumping")) ownerAnim.SetBool("SuperJumping", false);
        StopAllCoroutines();
        if (owner.GetComponent<Rigidbody2D>().velocity != Vector2.zero) owner.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }
    #endregion
    #region Public
    public override void Activate(Vector2 pos)
    {
        base.Activate(pos);
        fallPower = 4f;
        ownerAnim.SetTrigger("SuperJumpAttack");
    }

    public void StartSuperJump()
    {
        StartCoroutine(SuperJumpingAttack());
    }
    public override void Deactivate()
    {
        for (int i = 0; i < hitBoxes.Length; ++i)
        {
            hitBoxes[i].Deactivate();
        }
        
        ActionEnd();
    }


    #endregion
    #endregion

    #region EventHandlers
    #endregion

    #region Coroutines
    #endregion

    #region MonoBehaviour
    #endregion
}
