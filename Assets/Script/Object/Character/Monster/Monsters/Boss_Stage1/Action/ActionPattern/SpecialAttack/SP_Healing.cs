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
        yield return StartCoroutine(SetPatternMortalBox()); // ���� �ֺ��� �ִ� ������� �� ���� ���� ���� ������ �ִ� �������� ����

        if (idx == -1) yield break; // ���࿡ �������� ã�� ���Ѵٸ� �׳� break

        MortalBox mortalBox = mortalBoxesList[idx].GetComponent<MortalBox>(); // ���� ���� ���� �ִ� ������
        
        yield return StartCoroutine(MoveToMortalBox(mortalBox)); // �ش� ���������� �̵��ϰ�
        // ���� �Դ� �ִϸ��̼��� ���⼭ ����ؾ���~
        mortalBox.ReduceCake(riceCost); // ���� ���Ͽ� �ʿ��� ������ŭ �����ϰ�
        // ���� �� �ڵ���� ��� Ư�����Ͽ��� ���������� ����ϴ� �Ŵϱ� �Ƹ� �� ��ũ��Ʈ�� ��� �ٽ� ���� �װ� ��ӹ޾Ƽ� ���� ������ ����
        transform.parent.parent.GetComponent<Unit>()[EStat.HP] += healAmount; // ���� ���ϴ� ���̴ϱ� ���� �Ѵ�.


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
