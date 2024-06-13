using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RabbitBossJumpAttackAction : AttackAction
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
    IEnumerator JumpToTarget(Vector2 pos)
    {
        startPos = transform.position;
        Vector2 velocity = CalculateJumpVelocity(startPos, pos, jumpHeight); // �ʿ��� �ӵ� ���
        rb.velocity = velocity;
        rb.gravityScale = 1; // ���� ���� �� �߷� Ȱ��ȭ

        // ��ǥ ������ ������ ������ ���
        while (true)
        {
            if (rb.velocity.y < 0)
            {
                Vector2 currentPosition = transform.position;
                if (currentPosition.y <= pos.y && currentPosition.x >= pos.x) // �ش� ��ġ�� �ٴٸ���
                {
                    rb.velocity = Vector2.zero;
                    rb.gravityScale = 0;
                    break;
                }
            }
            yield return null; // ���� �����ӱ��� ���
        }

        yield return StartCoroutine(HitBoxOn(pos));
    }
    // �������� �׸��� ������ ��, �־��� ���̿� ��ǥ ��ġ�� �����ϱ� ���� �ʱ� �ӵ��� ����ϴ� �Լ�
    Vector2 CalculateJumpVelocity(Vector2 start, Vector2 end, float height)
    {
        float displacementX = end.x - start.x; // ��ǥ ��ġ�� ���� ��ġ ������ ���� �Ÿ�
        float displacementY = end.y - start.y; // ��ǥ ��ġ�� ���� ��ġ ������ ���� �Ÿ�
        float time = Mathf.Sqrt(-2 * height / gravity) + Mathf.Sqrt(2 * (displacementY - height) / gravity);
        // ������ �ִ� ���̿� �����ϴ� �� �ɸ��� �ð� + �� ���̿��� ��ǥ ��ġ���� �������� �� �ɸ��� �ð� <<< sqrt(2h / g) + sqrt(2(y - h) / g) �߷��� ������ �����Ǿ� -�� ����
        // 

        float velocityX = displacementX / time; // ���� ������ ��ü ���� �ð����� ���� �� >> ���� �ӵ�
        float velocityY = Mathf.Sqrt(-2 * gravity * height); //  �ִ� ���̿� �����ϱ� ���� �ʱ� �ӵ� >> �����ӵ�, ���⼭�� �߷��� ������ ���� �Ǿ� - �� ����

        return new Vector2(velocityX, velocityY); // ���� ���� �ӵ� (velocityX)�� ���� �ӵ� (velocityY)�� �����ϴ� Vector2�� ��ȯ
    }

    #endregion
    #region Protected
    protected override void ActionEnd()
    {
        base.ActionEnd();
    }
    #endregion
    #region Public
    public override void Activate(Vector2 pos)
    {
        base.Activate(pos);
        rb = transform.parent.GetComponentInParent<Rigidbody2D>();
        rb.gravityScale = 0;
        StartCoroutine(JumpToTarget(pos));
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
