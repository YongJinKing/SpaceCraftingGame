using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPAttackAction : BossAction
{
    #region Properties
    #region Private
    
    #endregion
    #region Protected
    [SerializeField] protected List<GameObject> mortalBoxesList = new List<GameObject>();
    [SerializeField] protected int idx = -1;
    [SerializeField] protected float moveSpeed = 4f;
    [Header("���� ���࿡ �ʿ��� �� ��"), Space(.5f)][SerializeField] protected int riceCost = 5;
    [SerializeField] protected bool chk = false;
    [SerializeField] protected MortalBox mortalBox;
    #endregion
    #region Public
    public RabbitBossPerception perception;
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
    public override void Deactivate()
    {
        for (int i = 0; i < hitBoxes.Length; ++i)
        {
            hitBoxes[i].Deactivate();
        }
        ActionEnd();
    }

    public void FininshAction()
    {
        ActionEnd();
    }
    #endregion
    #endregion

    #region EventHandlers
    #endregion

    #region Coroutines
    protected IEnumerator FindMortalBox()
    {
        yield return StartCoroutine(SetPatternMortalBox()); // ���� �ֺ��� �ִ� ������� �� ���� ���� ���� ������ �ִ� �������� ����

        if (idx == -1) yield break; // ���࿡ �������� ã�� ���Ѵٸ� �׳� break

        mortalBox = mortalBoxesList[idx].GetComponent<MortalBox>(); // ���� ���� ���� �ִ� ������

        if(mortalBox.GetRice() < riceCost) yield break;

        yield return StartCoroutine(MoveToMortalBox(mortalBox)); // �ش� ���������� �̵��ϰ�

        chk = true;
        mortalBox.ReduceCake(riceCost); // ���� ���Ͽ� �ʿ��� ������ŭ �����ϰ�
    }

    protected IEnumerator SetPatternMortalBox()
    {
        float maxValue = -1f;
        for (int i = 0; i < mortalBoxesList.Count; i++)
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

    protected IEnumerator MoveToMortalBox(MortalBox mortalBox)
    {
        ownerAnim.SetBool("Move", true);
        //Transform rabbit = transform.parent.parent;
        SetRabbitLookPlayer(mortalBox.transform);
        Vector2 dir = mortalBox.transform.position - owner.transform.position;
        float dist = dir.magnitude;
        dir.Normalize();

        while (dist >= 1f)
        {
            float delta = moveSpeed * Time.deltaTime;
            dist -= delta;
            owner.transform.Translate(dir * delta, Space.World);
            yield return null;
        }
        yield return null;
        ownerAnim.SetBool("Move", false);
    }

    #endregion

    #region MonoBehaviour
    #endregion
}
