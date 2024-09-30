using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class RabbitBossJumpAttackAction : BossAction
{
    #region Properties
    #region Private
    private Rigidbody2D rb;
    private Vector2 startPos;

    #endregion
    #region Protected
    #endregion
    #region Public
    public Vector2 targetPosition; // ��ǥ ������ ��ġ
    public float jumpHeight = 2.5f; // ���� ����
    public float gravity = -9.81f; // �߷�
    public Transform attackEffect;
    #endregion
    #region Events
    #endregion
    #endregion

    #region Constructor
    public RabbitBossJumpAttackAction()
    {
        fireAndForget = false;
    }
    #endregion

    #region Methods
    #region Private
    IEnumerator HitBoxOn(Vector2 pos)
    {
        attackEffect.gameObject.SetActive(true);

        for (int i = 0; i < hitBoxes.Length; ++i)
        {
            hitBoxes[i].Activate(pos);

            Quaternion rot = hitBoxes[i].transform.rotation;

            Vector3 eulerRotation = rot.eulerAngles;
            eulerRotation.z += 180f;
            attackEffect.gameObject.transform.rotation = Quaternion.Euler(eulerRotation);
        }

        attackEffect.GetComponent<ParticleSystem>().Play();
        camController.StartCameraShake(3f,0.5f);
        yield return null;
    }
    
    // ���� �䳢�� ��ġ�κ��� Ÿ���� ��ġ���� height�� ���̷� duration�� �ð���ŭ ������ �̵��� ����
    // Ÿ���� ��ġ�� �ٴٸ��� ��Ʈ�ڽ��� Ų��.
    IEnumerator JumpToTarget(Vector2 start, Vector2 target, float height, float duration)
    {
        SetRabbitLookPlayer(target);
        float jumpTime = 0;

        // �÷��̾��� ��ġ�� �����Ͽ� Ÿ�ٺ��� �������� �������� ��
        Vector2 direction = (target - start).normalized;  // Ÿ�� ���� ����
        float offsetDistance = 2f;  // ��Ʈ�ڽ��� �÷��̾ �µ��� ������ �Ÿ�
        Vector2 adjustedTarget = target - direction * offsetDistance;

        // �ִϸ��̼� Ʈ����
        ownerAnim.SetTrigger("JumpAttack");

        while (jumpTime < duration)
        {
            jumpTime += Time.deltaTime;
            float t = jumpTime / duration;

            // ������ �̵� ���
            float x = Mathf.Lerp(start.x, adjustedTarget.x, t);
            float y = Mathf.Lerp(start.y, adjustedTarget.y, t) + height * Mathf.Sin(Mathf.PI * t);
            rb.position = new Vector2(x, y);
            yield return null;
        }

        // ��Ȯ�� ��ġ ����
        rb.position = adjustedTarget;

        // ���� �� ��Ʈ�ڽ� Ȱ��ȭ
        yield return StartCoroutine(HitBoxOn(target));
    }

    #endregion
    #region Protected
    protected override void ActionEnd()
    {
        base.ActionEnd();
        StopAllCoroutines();
        if (owner.GetComponent<Rigidbody2D>().velocity != Vector2.zero) owner.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }
    #endregion
    #region Public
    public override void Activate(Vector2 pos)
    {
        base.Activate(pos);
        //rb = transform.parent.GetComponentInParent<Rigidbody2D>();
        rb = owner.transform.GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        StartCoroutine(JumpToTarget(transform.position, pos, 2.5f, 1f));
        // �������� �׸��鼭 ������ �ϴ� �Լ��� �����
        // �� �Լ��� ���� ���� ��Ʈ�ڽ��� Ų��.
        
    }
    public override void Deactivate()
    {
        attackEffect.GetComponent<ParticleSystem>().Stop();
        attackEffect.gameObject.SetActive(false);
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
