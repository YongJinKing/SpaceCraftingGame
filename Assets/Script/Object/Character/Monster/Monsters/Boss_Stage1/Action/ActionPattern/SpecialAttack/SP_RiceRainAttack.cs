using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SP_RiceRainAttack : SPAttackAction
{
    #region Properties
    #region Private

    #endregion
    #region Protected

    #endregion
    #region Public
    public Transform julguHitVFX;
    public Transform[] xShapeLines;
    public Transform[] plusShapeLines; // �� �ΰ��� ������ ��ġ
    public Transform[] xShapeWarningLines;
    public Transform[] plusShapeWarningLines; // �� �ΰ��� ������ ��ġ�� ��� ǥ�ø� �����ִ� ��
    public GameObject Rice;
    public float tolerance = 0.1f; // ��ǥ ��ġ�� �����ߴ��� Ȯ���� �� ����� ���� ����
    #endregion
    #region Events
    #endregion
    #endregion

    #region Constructor
    public SP_RiceRainAttack()
    {
        fireAndForget = false;
    }
    #endregion

    #region Methods
    #region Private
    void ShowPatternLines(Transform[] list, bool toggle)
    {
        for (int i = 0; i < list.Length; i++)
        {
            list[i].gameObject.SetActive(toggle);
        }
    }

    void SpawnRiceAtPosition(Vector3 pos)
    {
        Vector3 targetPos = pos;
        Vector3 spawnPos = pos + new Vector3(0f, Camera.main.orthographicSize + 10, 0f); // ���� ī�޶��� ������� 10ĭ ���� ������ �ű⼭ ����Ʈ����.
        var obj = Instantiate(Rice, spawnPos, Quaternion.identity);
        obj.transform.SetParent(null);

        // Rigidbody2D ������Ʈ�� �߰��Ͽ� �߷��� ����
        Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = obj.AddComponent<Rigidbody2D>();
        }

        // �߷��� ���� (�⺻ �߷� ���ӵ��� ���)
        rb.gravityScale = 1f;

        obj.GetComponent<MeteorRice>().Initialize(targetPos);
    }

    void SpawnRiceWithList(Transform[] list)
    {
        for (int i = 0; i < list.Length; i++)
        {
            SpawnRiceAtPosition(list[i].position);
        }
    }
    IEnumerator StartRiceRainPattern()
    {
        yield return StartCoroutine(FindMortalBox()); // �ֺ� ������ �� ���� ���� ���� ������ �ִ� ���� ��� �װ����� �̵��ϰ� ���Ͽ� �ʿ��� �� ������ŭ ������Ų��.
        // ���⼭���� ���� �����ϸ� ��
        if (!chk)
        {
            ActionEnd();
            yield break;
        }

        // �ϴ� �������� ������ �ִϸ��̼��� ���⼭ �����Ѵ�.
        owner.animator.SetTrigger("JulguHit");
    }

    // ���� ������ ��ġ, x,+�ڷ� ���� ü���� ������� ���� ��¿�� �𸣰ڴµ� �ϴ��� �ش� ��ġ�� �������� ������, ���� �� ������ �� ��ġ�� ���������� �ڷ�ƾ�� �����ϰ� �Ѵ�.
    IEnumerator ShowPatternLines()
    {
        mortalBox.anim.SetTrigger("RiceAttack");
        yield return new WaitForSeconds(2f);
        int cnt = 0;
        while (cnt < 4)
        {
            if (cnt % 2 == 0)
            {
                ShowPatternLines(xShapeLines, true);
            }
            else
            {
                ShowPatternLines(plusShapeLines, true);
            }

            yield return new WaitForSeconds(0.5f);

            if (cnt % 2 == 0)
            {
                ShowPatternLines(xShapeLines, false);
            }
            else
            {
                ShowPatternLines(plusShapeLines, false);
            }
            cnt++;
        }

        yield return StartCoroutine(SpawnRiceRain());
    }

    // ������ ������ ��ġ ���� �����. �� ��
    IEnumerator SpawnRiceRain()
    {
        ownerAnim.SetBool("Teabagging", true);
        int cnt = 0;
        while (cnt < 4)
        {
            if (cnt % 2 == 0)
            {
                ShowPatternLines(xShapeWarningLines, true);
                SpawnRiceWithList(xShapeLines);
            }
            else
            {
                ShowPatternLines(plusShapeWarningLines, true);
                SpawnRiceWithList(plusShapeLines);
            }

            yield return new WaitForSeconds(3f);
            if (cnt % 2 == 0) ShowPatternLines(xShapeWarningLines, false);
            else ShowPatternLines(plusShapeWarningLines, false);
            cnt++;

        }


        yield return null;
        ownerAnim.SetBool("Teabagging", false);
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
    // �������� ġ�� �ִϸ��̼� �������� �̰� ����
    public void StartShowPattern()
    {
        StartCoroutine(ShowPatternLines());
    }

    public void StartWarningPattern()
    {
        Instantiate(julguHitVFX, mortalBox.transform.position, Quaternion.identity);
        StartCoroutine(ShowPatternLines());
    }
    public override void Activate(Vector2 pos)
    {
        base.Activate(pos);
        mortalBoxesList = perception.GetList();
        chk = false;
        StartCoroutine(StartRiceRainPattern());

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
