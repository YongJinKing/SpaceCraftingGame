using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class RabbitBossThrowAttackAction : BossAction
{
    #region Properties
    #region Private
    Vector2 targetPos;
    #endregion
    #region Protected
    #endregion
    #region Public
    public GameObject riceCakes;
    public GameObject riceFloor;
    public Transform ThrowingPos;
    #endregion
    #region Events
    #endregion
    #endregion

    #region Constructor
    public RabbitBossThrowAttackAction()
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

    IEnumerator ThrowingAttack()
    {
        Debug.Log("스로잉");
        //지금 당장은 딜레이로 조절하고 있는데 이벤트를 쓰는 법을 알아봐야하는데 잘 모르겠음.,.
        //AsyncAnimation(0, false);
        
        //yield return new WaitForSeconds(1);
        //AsyncAnimation(1, false);
        
        GameObject obj = Instantiate(riceCakes,ThrowingPos.position, Quaternion.identity);
        obj.transform.parent = null;
        targetPos = FindFirstObjectByType<Player>().transform.position;
        obj.GetComponent<ThrownRice>().SetTarget(targetPos);
        // 여기서 obj, 즉 떡의 타겟을 pos로 넘겨주는 작업을 하고 아래 포물선 코드는 싹다 떡 쪽으로 옮길거임
        /*float throwingTime = 0;
        while (throwingTime < 2f)
        {
            throwingTime += Time.deltaTime;
            float t = throwingTime / 2f;
            float x = Mathf.Lerp(ThrowingPos.position.x, pos.x, t);
            float y = Mathf.Lerp(ThrowingPos.position.y, pos.y, t) + 2.5f * Mathf.Sin(Mathf.PI * t);
            obj.transform.position = new Vector2(x, y);
            yield return null;
        }
        obj.transform.position = pos; // 정확한 위치 보정*/

        //Destroy(obj); // 일단은 다다르면 삭제 >> 해당 위치 주변으로 슬로우 장판을 깔거임, 만약에 플레이어가 적이 던진 떡을 그냥 맞으면 슬로우 장판 안생김
        //StartCoroutine(SpawnSlowSpot(pos)); // 슬로우 장판 생성
        yield return new WaitForSeconds(1f);

       // ActionEnd();
    }

    IEnumerator SpawnSlowSpot(Vector2 pos)
    {
        GameObject obj = Instantiate(riceFloor, pos, Quaternion.identity);
        obj.transform.parent = null;
        yield return new WaitForSeconds(5f); // 생성 후 5초뒤 삭제, 실제로 슬로우 판정을 부여? 하는쪽은 저기 장판이 자체적으로 할거임
        Destroy(obj);
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
        // 플레이어에게 떡(돌?)을 던짐, 해당 위치에 광역 슬로우 장판을 설치하는거까지 생각중
        ownerAnim.SetTrigger("ThrowAttack");
        targetPos = pos;
    }

    public void ThrowStoneEvent()
    {
        StartCoroutine(ThrowingAttack());
    }

    public void ThrowEndEvent()
    {
        ActionEnd();
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
