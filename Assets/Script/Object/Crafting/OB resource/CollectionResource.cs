using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionResource : MonoBehaviour
{
    private MineralSpawner spawner;

    public void Initialize(MineralSpawner spawner)
    {
        this.spawner = spawner;
    }

    void OnMouseDown()
    {
        // �̳׶��� ä��Ǿ��� �� ���ο� �̳׶��� ����
        spawner.PlaceMinerals();

        // �̳׶� ������Ʈ ����
        Destroy(gameObject);
    }
}
