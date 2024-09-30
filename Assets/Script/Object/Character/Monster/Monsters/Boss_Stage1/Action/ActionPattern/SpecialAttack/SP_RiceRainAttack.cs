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
    public Transform[] plusShapeLines; // 이 두개는 떨어질 위치
    public Transform[] xShapeWarningLines;
    public Transform[] plusShapeWarningLines; // 위 두개는 떨어질 위치에 경고 표시를 보여주는 것*/

    public List<Transform> xShapeLines;
    public List<Transform> plusShapeLines;
    public List<Transform> xShapeWarningLines;
    public List<Transform> plusShapeWarningLines;
    public GameObject Rice;
    public float tolerance = 0.1f; // 목표 위치에 도달했는지 확인할 때 사용할 오차 범위
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
        Vector3 spawnPos = pos + new Vector3(0f, Camera.main.orthographicSize + 15, 0f); // 메인 카메라의 사이즈보다 10칸 위로 보내서 거기서 떨어트린다.
        RiceSpawnPos.transform.position = spawnPos;
        //var obj = Instantiate(Rice, spawnPos, Quaternion.identity);
        var obj = ObjectPool.Instance.GetObject<MeteorRice>(Rice, RiceSpawnPos);
        obj.transform.SetParent(null);

        // Rigidbody2D 컴포넌트를 추가하여 중력을 적용
        Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = obj.AddComponent<Rigidbody2D>();
        }

        // 중력을 설정 (기본 중력 가속도를 사용)
        rb.gravityScale = 4f;

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
        yield return StartCoroutine(FindMortalBox()); // 주변 절구통 중 가장 떡을 많이 가지고 있는 것을 골라 그곳으로 이동하고 패턴에 필요한 떡 갯수만큼 차감시킨다.
        // 여기서부터 패턴 시작하면 됨
        if (!chk)
        {
            ActionEnd();
            yield break;
        }

        // 일단 절구통을 때리는 애니메이션을 여기서 실행한다.
        owner.animator.SetTrigger("JulguHit");
    }

    // 떡이 떨어질 위치, x,+자로 할지 체스판 모양으로 할지 어쩔지 모르겠는데 일단은 해당 위치를 깜빡깜빡 보여줌, 끝날 때 실제로 그 위치로 떨어지도록 코루틴을 시작하게 한다.
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

    // 실제로 떨어질 위치 위로 만든다. 떡 비를
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

    // 절구통을 치는 애니메이션 마지막에 이걸 실행
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
