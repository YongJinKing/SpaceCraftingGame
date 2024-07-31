using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SP_Healing : SPAttackAction
{
    #region Properties
    #region Private
    
    [SerializeField] float healAmount = 10f;
    [SerializeField] float atkAmount = 5f;
    #endregion
    #region Protected

    #endregion
    #region Public
    public ParticleSystem buffVFX;
    #endregion
    #region Events
    #endregion
    #endregion

    #region Constructor
    public SP_Healing()
    {
        fireAndForget = false;
    }
    #endregion

    #region Methods
    #region Private
    
    IEnumerator HealingPattern()
    {
        yield return StartCoroutine(FindMortalBox());

        // ���� �Դ� �ִϸ��̼��� ���⼭ ����ؾ���~
        if (chk)
        {
            owner.animator.SetTrigger("Eating");
            transform.parent.parent.GetComponent<Unit>()[EStat.HP] += healAmount; // ���� ���ϴ� ���̴ϱ� ���� �Ѵ�.
            transform.parent.parent.GetComponent<Unit>()[EStat.ATK] *= 1.2f;
            if (!buffVFX.isPlaying) buffVFX.Play();
        }

        yield return new WaitForSeconds(2f);

        //ActionEnd();
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
        chk = false;
        mortalBoxesList = perception.GetList();
        StartCoroutine(HealingPattern());

    }
    public override void Deactivate()
    {
        for (int i = 0; i < hitBoxes.Length; ++i)
        {
            hitBoxes[i].Deactivate();
        }
        ActionEnd();
    }

    public void ActEndForAnim()
    {
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
