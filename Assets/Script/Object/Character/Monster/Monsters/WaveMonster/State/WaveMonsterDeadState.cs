using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveMonsterDeadState : MonsterState
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
    #endregion

    #region Methods
    #region Private
    #endregion
    #region Protected
    #endregion
    #region Public
    public override void Enter()
    {
        base.Enter();
        StartCoroutine(Dying());
    }
    #endregion
    #endregion

    #region EventHandlers
    #endregion

    #region Coroutines
    protected IEnumerator Dying()
    {
        yield return null;
        float time = 5.0f;

        //Dying animation here
        owner.animator.SetTrigger("T_Dead");

        GetComponent<Collider2D>().enabled = false;

        while(time > 0)
        {
            time -= Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }
    #endregion

    #region MonoBehaviour
    #endregion
}
