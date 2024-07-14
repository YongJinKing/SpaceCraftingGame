using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public GameObject objectToInstantiate;  // 생성할 오브젝트 프리팹
    public Transform spawnPoint;            // 오브젝트가 생성될 위치
    public float forceAmount = 10f;         // 발사할 때 가할 힘의 크기

    void Update()
    {
        // 스페이스바가 눌리면 오브젝트를 생성하고 발사
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // 오브젝트를 생성
            GameObject instantiatedObject = Instantiate(objectToInstantiate, spawnPoint.position, spawnPoint.rotation);

            // 생성된 오브젝트의 Rigidbody2D를 가져옴
            Rigidbody2D rb = instantiatedObject.GetComponent<Rigidbody2D>();

            // Rigidbody2D가 있는지 확인
            if (rb != null)
            {
                // 오브젝트를 앞으로 발사 (2D에서는 transform.right 사용)
                rb.AddForce(spawnPoint.right * forceAmount, ForceMode2D.Impulse);
            }
            else
            {
                Debug.LogWarning("생성된 오브젝트에 Rigidbody2D가 없습니다. Rigidbody2D 컴포넌트를 추가해주세요.");
            }
        }
    }
}
