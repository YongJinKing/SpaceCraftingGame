using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitBossSpinningAttackAction : BossAction
{
    #region Properties
    #region Private
    [Header("이동 속도"), Space(.5f)][SerializeField] float moveSpeed = 1f;
    [Header("회전 시간"), Tooltip("몇초 동안 회전할지, 히트박스의 Duration과 맞춰야함"), Space(.5f)] [SerializeField] float moveTimer;
    //Transform boss;
    Vector2 targetPos;
    #endregion
    #region Protected

    #endregion
    #region Public
    public Transform target;
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
        //yield return 
            StartCoroutine(HitBoxOn(transform.position));
        

        while (moveTimer >= 0.0f)
        {
            moveTimer -= Time.deltaTime;

            Vector2 dir = (Vector2)target.transform.position - (Vector2)owner.transform.position;
            dir.Normalize();
            
            owner.transform.Translate(dir * moveSpeed * Time.deltaTime, Space.World);
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
        SoundManager.Instance.StopSFX(SoundManager.Instance.soundData.bossSpinAttack);
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
        //boss = this.transform.parent.parent;
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
