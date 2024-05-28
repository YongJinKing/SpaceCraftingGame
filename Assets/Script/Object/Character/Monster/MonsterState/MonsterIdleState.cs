using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterIdleState : MonsterState
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
    protected override void AddListeners()
    {
        base.AddListeners();
    }
    protected override void RemoveListeners()
    {
        base.RemoveListeners();
    }
    #endregion
    #region Public
    public override void Enter()
    {
        base.Enter();
        StartCoroutine(Roaming());
        StartCoroutine(Detecting());
    }
    public override void Exit()
    {
        base.Exit();
        StopAllCoroutines();
    }
    #endregion
    #endregion

    #region EventHandlers
    #endregion

    #region Coroutines
    protected IEnumerator Roaming()
    {
        float roamingRadius = 5.0f;
        float maxWaitTime = 3.0f;
        float randomDist = 0.0f;
        float randomAngle = 0.0f;
        float randomWaitTime = 0.0f;
        Vector2 randomPos = Vector2.right;
        while (true)
        {
            randomDist = Random.Range(0.0f, roamingRadius);
            randomAngle = Random.Range(0.0f, 360.0f);
            randomWaitTime = Random.Range(1.0f, maxWaitTime);

            randomPos = new Vector2(Mathf.Cos(randomAngle * Mathf.Deg2Rad), Mathf.Cos(randomAngle * Mathf.Deg2Rad));
            randomPos = randomPos * randomDist;

            owner.pointMove?.Activate((Vector2)owner.spawnPoint + randomPos);
            yield return new WaitForSeconds(randomWaitTime);
        }
    }
    protected IEnumerator Detecting()
    {
        if (owner.ai == null) yield break;

        GameObject target = null;
        //For Test, Hard Coding LayerMask
        LayerMask temp = (1 << 17);

        Debug.Log(temp.value);
        Debug.Log(LayerMask.NameToLayer("Mineral"));

        while(target == null)
        {
            target = owner.ai.TargetSelect(temp, owner[EStat.DetectRadius]);
            yield return null;
        }

        owner.target = target;
        owner.stateMachine.ChangeState<MonsterDetectState>();
    }
    #endregion

    #region MonoBehaviour
    #endregion
}