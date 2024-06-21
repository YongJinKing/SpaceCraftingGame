using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitBossSpinningAttackAction : AttackAction
{
    #region Properties
    #region Private
    #endregion
    #region Protected
    #endregion
    #region Public
    #endregion
    #region Events
    #endregion
    #endregion

    #region Constructor
    public RabbitBossSpinningAttackAction()
    {
        fireAndForget = true;
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
        AsyncAnimation(0, false);
        yield return new WaitForSeconds(1f);
        AsyncAnimation(1, true);
    }
    #endregion
    #region Protected
    protected override void ActionEnd()
    {
        base.ActionEnd();
        AsyncAnimation(2, false);
        StopAllCoroutines();
    }
    #endregion
    #region Public
    public override void Activate(Vector2 pos)
    {
        base.Activate(pos);
        // ���ۺ��� ���� �÷��̾ ���󰡴� �׼�
        // ���� �ִϸ��̼��� ���� ������ �̹����� �ø��ϸ� ���󰡰� �սô�
        StartCoroutine(HitBoxOn(transform.position));
        StartCoroutine(SpinningAttack());
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
