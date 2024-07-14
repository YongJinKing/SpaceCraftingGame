using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public GameObject objectToInstantiate;  // ������ ������Ʈ ������
    public Transform spawnPoint;            // ������Ʈ�� ������ ��ġ
    public float forceAmount = 10f;         // �߻��� �� ���� ���� ũ��

    void Update()
    {
        // �����̽��ٰ� ������ ������Ʈ�� �����ϰ� �߻�
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // ������Ʈ�� ����
            GameObject instantiatedObject = Instantiate(objectToInstantiate, spawnPoint.position, spawnPoint.rotation);

            // ������ ������Ʈ�� Rigidbody2D�� ������
            Rigidbody2D rb = instantiatedObject.GetComponent<Rigidbody2D>();

            // Rigidbody2D�� �ִ��� Ȯ��
            if (rb != null)
            {
                // ������Ʈ�� ������ �߻� (2D������ transform.right ���)
                rb.AddForce(spawnPoint.right * forceAmount, ForceMode2D.Impulse);
            }
            else
            {
                Debug.LogWarning("������ ������Ʈ�� Rigidbody2D�� �����ϴ�. Rigidbody2D ������Ʈ�� �߰����ּ���.");
            }
        }
    }
}
