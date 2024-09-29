using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SP_Healing : SPAttackAction
{
    #region Properties
    #region Private
    
    [Header("힐량"), Space(.5f)] [SerializeField] float healAmount;
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
        if (!chk)
        {
            ActionEnd();
        }

        // 떡을 먹는 애니메이션을 여기서 출력해야함~
        if (chk)
        {
            owner.animator.SetTrigger("Eating");
            owner[EStat.HP] += healAmount;
            //owner[EStat.ATK] = 2f;
            owner.SetStatTwiceAndReset(EStat.ATK, 10f);
            owner[EStat.MoveSpeed] *= 1.1f;
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
