using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public GameObject tthise;
    public GameObject prefab; // 등록된 프리팹
    private GameObject spawnedPrefab; // 소환된 프리팹
    private bool isOriginalActive = true; // 원래 오브젝트 활성화 상태 추적

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) // 1번 키 입력
        {
            if (isOriginalActive)
            {
                // 현재 오브젝트 비활성화
                tthise.gameObject.SetActive(false);
                // 같은 위치에 프리팹 소환
                spawnedPrefab = Instantiate(prefab, transform.position, transform.rotation);
                isOriginalActive = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha2)) // 2번 키 입력
        {
            if (!isOriginalActive)
            {
                // 비활성화 했던 오브젝트 다시 활성화
                tthise.gameObject.SetActive(true);
                isOriginalActive = true;
            }
        }
    }
}
