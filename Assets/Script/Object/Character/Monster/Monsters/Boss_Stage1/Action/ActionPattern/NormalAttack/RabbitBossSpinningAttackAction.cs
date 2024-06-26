using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitBossSpinningAttackAction : BossAction
{
    #region Properties
    #region Private
    [SerializeField] float moveSpeed = 2f;
    float moveTimer;

    Vector2 targetPos;
    #endregion
    #region Protected

    #endregion
    #region Public
    public Transform target; // �ϴ� �ϴµ� ���߿� ���� �����Ұ���. ������ �� ��ũ��Ʈ���� �÷��̾ �����ϱ� ���� �÷��̾ ���ε��� ������ �׳� �������..
    #endregion
    #region Events
    #endregion
    #endregion

    #region Constructor
    public RabbitBossSpinningAttackAction()
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

    IEnumerator SpinningAttack()
    {
        yield return StartCoroutine(HitBoxOn(transform.position));
        Transform boss = this.transform.parent.parent;
        //AsyncAnimation(0, false);
        //yield return new WaitForSeconds(1f);
        //AsyncAnimation(1, true);
        while (moveTimer >= 0.0f)
        {
            moveTimer -= Time.deltaTime;
            Vector2 dir = target.transform.position - boss.position;
            dir.Normalize();
            boss.Translate(dir * moveSpeed * Time.deltaTime, Space.World);
            yield return null;
        }
        

        yield return StartCoroutine(StopSpinningAttack());
    }

    IEnumerator StopSpinningAttack()
    {
        yield return new WaitForSeconds(3f);
    }
    #endregion
    #region Protected
    protected override void ActionEnd()
    {
        base.ActionEnd();
        //AsyncAnimation(2, false);
        //ownerAnim.SetBool("SpinningAttackEnd", false);
        ownerAnim.SetTrigger("SpinningAttackEnd");
        StopAllCoroutines();
    }
    #endregion
    #region Public
    public void StartSpinning()
    {
        StartCoroutine(SpinningAttack());
    }
    public override void Activate(Vector2 pos)
    {
        base.Activate(pos);
        // ���ۺ��� ���� �÷��̾ ���󰡴� �׼�
        // ���� �ִϸ��̼��� ���� ������ �̹����� �ø��ϸ� ���󰡰� �սô�
        moveTimer = 5f;
        ownerAnim.SetTrigger("SpinningAttack");
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
