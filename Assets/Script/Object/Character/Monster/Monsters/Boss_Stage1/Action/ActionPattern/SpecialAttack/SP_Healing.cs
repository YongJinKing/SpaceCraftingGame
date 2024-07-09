using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SP_Healing : SPAttackAction
{
    #region Properties
    #region Private
    
    [SerializeField] float healAmount = 10f;
    
    #endregion
    #region Protected
    
    #endregion
    #region Public
    
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

        // 떡을 먹는 애니메이션을 여기서 출력해야함~
        owner.animator.SetTrigger("Eating");
        if (chk) transform.parent.parent.GetComponent<Unit>()[EStat.HP] += healAmount; // 여긴 힐하는 곳이니깐 힐을 한다.

        yield return new WaitForSeconds(2f);

        ActionEnd();
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
    #endregion
    #endregion

    #region EventHandlers
    #endregion

    #region Coroutines
    #endregion

    #region MonoBehaviour
    #endregion
}
