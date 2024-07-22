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
        Debug.Log("������");
        //���� ������ �����̷� �����ϰ� �ִµ� �̺�Ʈ�� ���� ���� �˾ƺ����ϴµ� �� �𸣰���.,.
        //AsyncAnimation(0, false);
        
        //yield return new WaitForSeconds(1);
        //AsyncAnimation(1, false);
        
        GameObject obj = Instantiate(riceCakes,ThrowingPos.position, Quaternion.identity);
        obj.transform.parent = null;
        targetPos = FindFirstObjectByType<Player>().transform.position;
        obj.GetComponent<ThrownRice>().SetTarget(targetPos);
        // ���⼭ obj, �� ���� Ÿ���� pos�� �Ѱ��ִ� �۾��� �ϰ� �Ʒ� ������ �ڵ�� �ϴ� �� ������ �ű����
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
        obj.transform.position = pos; // ��Ȯ�� ��ġ ����*/

        //Destroy(obj); // �ϴ��� �ٴٸ��� ���� >> �ش� ��ġ �ֺ����� ���ο� ������ �����, ���࿡ �÷��̾ ���� ���� ���� �׳� ������ ���ο� ���� �Ȼ���
        //StartCoroutine(SpawnSlowSpot(pos)); // ���ο� ���� ����
        yield return new WaitForSeconds(1f);

       // ActionEnd();
    }

    IEnumerator SpawnSlowSpot(Vector2 pos)
    {
        GameObject obj = Instantiate(riceFloor, pos, Quaternion.identity);
        obj.transform.parent = null;
        yield return new WaitForSeconds(5f); // ���� �� 5�ʵ� ����, ������ ���ο� ������ �ο�? �ϴ����� ���� ������ ��ü������ �Ұ���
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
        // �÷��̾�� ��(��?)�� ����, �ش� ��ġ�� ���� ���ο� ������ ��ġ�ϴ°ű��� ������
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
