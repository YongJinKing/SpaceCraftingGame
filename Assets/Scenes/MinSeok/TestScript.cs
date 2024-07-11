using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public float forceAmount = 10f;  // 힘의 크기
    public float destroytime = 10f;

    private Rigidbody2D rb2D;

    void Start()
    {
        // Rigidbody 컴포넌트를 가져옴
        rb2D = GetComponent<Rigidbody2D>();

        // Rigidbody가 있는지 확인
        if (rb2D != null)
        {
            // 오브젝트를 앞으로 힘을 가함
            rb2D.AddForce(transform.right * forceAmount, ForceMode2D.Impulse);
            Destroy(gameObject, destroytime);
        }
    }
}
