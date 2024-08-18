using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class RabbitBossThrowAttackAction : BossAction
{
    #region Properties
    #region Private
    Vector2 targetPos;
    #endregion
    #region Protected
    #endregion
    #region Public
    public GameObject riceCakes;
    public Transform ThrowingPos;
    #endregion
    #region Events
    #endregion
    #endregion

    #region Constructor
    public RabbitBossThrowAttackAction()
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

    IEnumerator ThrowingAttack()
    {
        GameObject obj = Instantiate(riceCakes,ThrowingPos.position, Quaternion.identity);
        obj.transform.parent = null;
        targetPos = FindFirstObjectByType<Player>().transform.position;
        obj.GetComponent<ThrownRice>().SetTarget(targetPos);
        
        yield return new WaitForSeconds(1f);

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
        // 플레이어에게 떡(돌?)을 던짐, 해당 위치에 광역 슬로우 장판을 설치하는거까지 생각중
        SetRabbitLookPlayer(pos);
        ownerAnim.SetTrigger("ThrowAttack");
        targetPos = pos;
    }

    public void ThrowStoneEvent()
    {
        StartCoroutine(ThrowingAttack());
    }

    public void ThrowEndEvent()
    {
        ActionEnd();
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
