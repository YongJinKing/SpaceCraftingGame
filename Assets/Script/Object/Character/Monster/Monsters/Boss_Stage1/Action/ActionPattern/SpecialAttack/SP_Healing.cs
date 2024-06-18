using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SP_Healing : AttackAction
{
    #region Properties
    #region Private
    [SerializeField] int riceCost = 5;
    [SerializeField] float healAmount = 10f;
    [SerializeField] float moveSpeed = 4f;
    #endregion
    #region Protected
    [SerializeField] protected List<GameObject> mortalBoxesList = new List<GameObject>();
    [SerializeField] protected int idx = -1;
    #endregion
    #region Public
    public RabbitBossPerception perception;
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
    IEnumerator HitBoxOn(Vector2 pos)
    {
        for (int i = 0; i < hitBoxes.Length; ++i)
        {
            hitBoxes[i].Activate(pos);
        }
        yield return null;
    }

    IEnumerator FindMortalBox()
    {
        yield return StartCoroutine(SetPatternMortalBox()); // 현재 주변에 있는 절구통들 중 가장 떡을 많이 가지고 있는 절구통을 고른다

        if (idx == -1) yield break; // 만약에 절구통을 찾지 못한다면 그냥 break

        MortalBox mortalBox = mortalBoxesList[idx].GetComponent<MortalBox>(); // 가장 떡이 많이 있는 절구통
        
        yield return StartCoroutine(MoveToMortalBox(mortalBox)); // 해당 절구통으로 이동하고
        // 떡을 먹는 애니메이션을 여기서 출력해야함~
        mortalBox.ReduceCake(riceCost); // 떡을 패턴에 필요한 갯수만큼 차감하고
        // 여기 위 코드들은 모두 특수패턴에서 공통적으로 사용하는 거니깐 아마 한 스크립트로 묶어서 다시 쓰고 그걸 상속받아서 쓰지 않을까 생각
        transform.parent.parent.GetComponent<Unit>()[EStat.HP] += healAmount; // 여긴 힐하는 곳이니깐 힐을 한다.


        ActionEnd();
    }

    IEnumerator SetPatternMortalBox()
    {
        float maxValue = -1f;
        for(int i = 0; i < mortalBoxesList.Count; i++)
        {
            int rice = mortalBoxesList[i].GetComponent<MortalBox>().GetRice();
            if (maxValue < rice)
            {
                maxValue = rice;
                idx = i;
            }
            yield return null;
        }
    }

    IEnumerator MoveToMortalBox(MortalBox mortalBox)
    {
        Transform rabbit = transform.parent.parent;
        Vector2 dir = mortalBox.transform.position - rabbit.position;
        float dist = dir.magnitude;
        dir.Normalize();

        while(dist >= 1f)
        {
            float delta = moveSpeed * Time.deltaTime;
            dist -= delta;
            rabbit.Translate(dir * delta, Space.World);
            yield return null;
        }
        yield return null;
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
        mortalBoxesList = perception.GetList();
        StartCoroutine(FindMortalBox());

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
