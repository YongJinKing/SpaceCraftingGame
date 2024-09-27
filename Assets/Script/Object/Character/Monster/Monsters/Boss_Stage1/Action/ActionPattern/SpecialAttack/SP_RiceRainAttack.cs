using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class SP_RiceRainAttack : SPAttackAction
{
    #region Properties
    #region Private
    List<int> randomPattern = new List<int>();
    #endregion
    #region Protected

    #endregion
    #region Public
    public Transform julguHitVFX;
    /*public Transform[] xShapeLines;
    public Transform[] plusShapeLines; // �� �ΰ��� ������ ��ġ
    public Transform[] xShapeWarningLines;
    public Transform[] plusShapeWarningLines; // �� �ΰ��� ������ ��ġ�� ��� ǥ�ø� �����ִ� ��*/

    public List<Transform> xShapeLines;
    public List<Transform> plusShapeLines;
    public List<Transform> xShapeWarningLines;
    public List<Transform> plusShapeWarningLines;
    public GameObject Rice;
    public float tolerance = 0.1f; // ��ǥ ��ġ�� �����ߴ��� Ȯ���� �� ����� ���� ����
    public Transform RiceSpawnPos;
    BoxCollider2D rabbitCollider;
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
    /*void ShowPatternLines(Transform[] list, bool toggle)
    {
        for (int i = 0; i < list.Length; i++)
        {
            list[i].gameObject.SetActive(toggle);
        }
    }*/

    void ShowPatternLines(List<Transform> list, bool toggle)
    {
        for (int i = 0; i < list.Count; i++)
        {
            list[i].gameObject.SetActive(toggle);
        }
    }

    void SpawnRiceAtPosition(Vector3 pos)
    {
        Vector3 targetPos = pos;
        Vector3 spawnPos = pos + new Vector3(0f, Camera.main.orthographicSize + 15, 0f); // ���� ī�޶��� ������� 10ĭ ���� ������ �ű⼭ ����Ʈ����.
        RiceSpawnPos.transform.position = spawnPos;
        //var obj = Instantiate(Rice, spawnPos, Quaternion.identity);
        var obj = ObjectPool.Instance.GetObject<MeteorRice>(Rice, RiceSpawnPos);
        obj.transform.SetParent(null);

        // Rigidbody2D ������Ʈ�� �߰��Ͽ� �߷��� ����
        Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = obj.AddComponent<Rigidbody2D>();
        }

        // �߷��� ���� (�⺻ �߷� ���ӵ��� ���)
        rb.gravityScale = 2f;

        obj.GetComponent<MeteorRice>().Initialize(targetPos);
    }

    /*void SpawnRiceWithList(Transform[] list)
    {
        for (int i = 0; i < list.Length; i++)
        {
            SpawnRiceAtPosition(list[i].position);
        }
    }*/

    void SpawnRiceWithList(List<Transform> list)
    {
        for (int i = 0; i < list.Count; i++)
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
            int rnd = Random.Range(0, 10);
            randomPattern.Add(rnd);
            if ((rnd % 2) == 0)
            {
                ShowPatternLines(xShapeLines, true);
            }
            else
            {
                ShowPatternLines(plusShapeLines, true);
            }

            yield return new WaitForSeconds(0.5f);

            if ((rnd % 2) == 0)
            {
                ShowPatternLines(xShapeLines, false);
            }
            else
            {
                ShowPatternLines(plusShapeLines, false);
            }

            yield return new WaitForSeconds(0.5f);
            cnt++;
        }

        yield return StartCoroutine(SpawnRiceRain());
    }

    // ������ ������ ��ġ ���� �����. �� ��
    IEnumerator SpawnRiceRain()
    {
        rabbitCollider.enabled = false;
        ownerAnim.SetBool("Teabagging", true);
        int cnt = 0;
        while (cnt < 4)
        {
            int idx = randomPattern[0];
            if ((idx % 2) == 0)
            {
                ShowPatternLines(xShapeWarningLines, true);
                SpawnRiceWithList(xShapeLines);
            }
            else
            {
                ShowPatternLines(plusShapeWarningLines, true);
                SpawnRiceWithList(plusShapeLines);
            }

            yield return new WaitForSeconds(2f);
            if ((idx % 2) == 0) ShowPatternLines(xShapeWarningLines, false);
            else ShowPatternLines(plusShapeWarningLines, false);

            randomPattern.RemoveAt(0);
            cnt++;

        }


        yield return null;
        ownerAnim.SetBool("Teabagging", false);
        rabbitCollider.enabled = true;
        ActionEnd();
    }

    #endregion
    #region Protected
    protected override void ActionEnd()
    {
        base.ActionEnd();
        ShowPatternLines(xShapeLines, false);
        ShowPatternLines(xShapeWarningLines, false);
        ShowPatternLines(plusShapeLines, false);
        ShowPatternLines(plusShapeWarningLines, false);

        randomPattern.Clear();
        StopAllCoroutines();
    }
    #endregion
    #region Public
    public void SetLists(List<Transform> xShapes, List<Transform> xWarnings, List<Transform> crossShapes, List<Transform> crossWarnings)
    {
        xShapeLines = xShapes;
        plusShapeLines = crossShapes;
        xShapeWarningLines = xWarnings;
        plusShapeWarningLines = crossWarnings;
    }

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
        rabbitCollider = this.transform.parent.parent.GetComponent<BoxCollider2D>();
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
