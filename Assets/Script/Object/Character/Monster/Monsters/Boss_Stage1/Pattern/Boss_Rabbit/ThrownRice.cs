using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrownRice : MonoBehaviour
{
    public GameObject slowingGround;
    public LayerMask layerMask;
    [SerializeField] float throwSpeed;
    Transform throwingPos;
    Vector2 targetPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        //StartCoroutine(ThrowingRice());
        StartCoroutine(ThrowingRiceWithLine());
    }

    public void SetThrowingPos(Transform pos)
    {
        throwingPos = pos;
    }
    public void SetTarget(Vector2 pos)
    {
        targetPos = pos;
    }
    // 이건 포물선을 그리며 날아가는 코드
    IEnumerator ThrowingRiceWithArc()
    {
        yield return new WaitForEndOfFrame();
        float throwingTime = 0;
        while (throwingTime < 2f)
        {
            throwingTime += Time.deltaTime;
            float t = throwingTime / 2f;
            float x = Mathf.Lerp(throwingPos.position.x, targetPos.x, t);
            float y = Mathf.Lerp(throwingPos.position.y, targetPos.y, t) + 2f * Mathf.Sin(Mathf.PI * t);
            this.transform.position = new Vector2(x, y);
            yield return null;
        }
        this.transform.position = targetPos; // 정확한 위치 보정

        Destroy(this.gameObject);
        yield return null;
    }

    // 이건 그냥 직선으로 날아가는 코드
    IEnumerator ThrowingRiceWithLine()
    {
        yield return new WaitForEndOfFrame();
        Vector2 dir = targetPos - (Vector2)this.transform.position;
        dir.Normalize();
        float currentSpeed = throwSpeed;
        while (true)
        {
            currentSpeed += throwSpeed * Time.deltaTime; // 가속도를 이용해 속도 증가
            this.transform.Translate(dir * currentSpeed * Time.deltaTime, Space.World);
            //this.transform.Translate(dir * throwSpeed * Time.deltaTime, Space.World);
            yield return null;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StopAllCoroutines();
        Destroy(gameObject);
        Debug.Log(collision.name);
        if(((1 << collision.gameObject.layer) & layerMask) != 0)
        {
            Instantiate(slowingGround, this.transform.position, Quaternion.identity);
        }
    }
}
