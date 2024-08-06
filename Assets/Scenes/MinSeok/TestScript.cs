using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public GameObject tthise;
    public GameObject prefab; // ��ϵ� ������
    private GameObject spawnedPrefab; // ��ȯ�� ������
    private bool isOriginalActive = true; // ���� ������Ʈ Ȱ��ȭ ���� ����

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) // 1�� Ű �Է�
        {
            if (isOriginalActive)
            {
                // ���� ������Ʈ ��Ȱ��ȭ
                tthise.gameObject.SetActive(false);
                // ���� ��ġ�� ������ ��ȯ
                spawnedPrefab = Instantiate(prefab, transform.position, transform.rotation);
                isOriginalActive = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha2)) // 2�� Ű �Է�
        {
            if (!isOriginalActive)
            {
                // ��Ȱ��ȭ �ߴ� ������Ʈ �ٽ� Ȱ��ȭ
                tthise.gameObject.SetActive(true);
                isOriginalActive = true;
            }
        }
    }
}
