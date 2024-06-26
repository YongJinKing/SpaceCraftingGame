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
        for (int i = 0; i < hitBoxes.Length; ++i)
        {
            hitBoxes[i].Activate(pos);
        }
        yield return null;
    }
    
    // ���� �䳢�� ��ġ�κ��� Ÿ���� ��ġ���� height�� ���̷� duration�� �ð���ŭ ������ �̵��� ����
    // Ÿ���� ��ġ�� �ٴٸ��� ��Ʈ�ڽ��� Ų��.
    IEnumerator JumpToTarget(Vector2 start, Vector2 target, float height, float duration)
    {
        float jumpTime = 0;
        //AsyncAnimation(0,false);
        ownerAnim.SetTrigger("JumpAttack");
        while (jumpTime < duration)
        {
            jumpTime += Time.deltaTime;
            float t = jumpTime / duration;
            float x = Mathf.Lerp(start.x, target.x, t);
            float y = Mathf.Lerp(start.y, target.y, t) + height * Mathf.Sin(Mathf.PI * t);
            rb.position = new Vector2(x, y);
            yield return null;
        }
        rb.position = target; // ��Ȯ�� ��ġ ����

        yield return StartCoroutine(HitBoxOn(target));
        //ActionEnd();
    }

    // �������� �׸��� ������ ��, �־��� ���̿� ��ǥ ��ġ�� �����ϱ� ���� �ʱ� �ӵ��� ����ϴ� �Լ� >> Rigidbody2D gravityScale�� ���
    // >> ���װ� ��� �ϴ� ����
    Vector2 CalculateJumpVelocity(Vector2 start, Vector2 end, float height)
    {
        float displacementX = end.x - start.x; // ��ǥ ��ġ�� ���� ��ġ ������ ���� �Ÿ�
        float displacementY = end.y - start.y; // ��ǥ ��ġ�� ���� ��ġ ������ ���� �Ÿ�

        // �ִ� ���̿� �����ϴ� �� �ɸ��� �ð� ���
        float sqrtTerm1 = Mathf.Sqrt(-2 * height / gravity);
        // ��ǥ ��ġ���� �������� �� �ɸ��� �ð� ���
        float sqrtTerm2 = Mathf.Sqrt(2 * (displacementY - height) / gravity);

        // sqrtTerm1�� sqrtTerm2�� ��ȿ���� Ȯ��
        if (float.IsNaN(sqrtTerm1) || float.IsNaN(sqrtTerm2))
        {
            return Vector2.zero;
        }

        float time = sqrtTerm1 + sqrtTerm2;

        // time�� ��ȿ���� Ȯ��
        if (time <= 0)
        {
            return Vector2.zero;
        }

        float velocityX = displacementX / time; // ���� ������ ��ü ���� �ð����� ���� �� >> ���� �ӵ�
        float velocityY = Mathf.Sqrt(-2 * gravity * height); // �ִ� ���̿� �����ϱ� ���� �ʱ� �ӵ� >> �����ӵ�

        // velocityY�� ��ȿ���� Ȯ��
        if (float.IsNaN(velocityY))
        {
            return Vector2.zero;
        }

        return new Vector2(velocityX, velocityY); // ���� ���� �ӵ� (velocityX)�� ���� �ӵ� (velocityY)�� �����ϴ� Vector2�� ��ȯ
    }

    #endregion
    #region Protected
    protected override void ActionEnd()
    {
        base.ActionEnd();
        StopAllCoroutines();
    }
    #endregion
    #region Public
    public override void Activate(Vector2 pos)
    {
        base.Activate(pos);
        rb = transform.parent.GetComponentInParent<Rigidbody2D>();
        rb.gravityScale = 0;
        //StartCoroutine(JumpToTarget(pos));
        StartCoroutine(JumpToTarget(transform.position, pos, 2.5f, 1f));
        // �������� �׸��鼭 ������ �ϴ� �Լ��� �����
        // �� �Լ��� ���� ���� ��Ʈ�ڽ��� Ų��.
        
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
