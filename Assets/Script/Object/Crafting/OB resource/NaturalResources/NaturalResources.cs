using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class NaturalResources : MonoBehaviour
{
    public int indexNum; // �ڿ��� �ε��� �ѹ�
    public int outputIndexNum; // �ڿ��� ĺ�� �� ���⹰�� �ε��� �ѹ�
    public int hp; // �ڿ��� hp = ��� �ѵ�ܾ� ĳ����
    public int minAmount; // �ּ� ���귮
    public int maxAmount; // �ִ� ���귮
    public int size;
    int amount; // ���� ���귮
    Inventory inventory;

    private void OnEnable()
    {
        inventory = FindObjectOfType<Inventory>();
    }

    public void TakeMining()
    {
        hp--;
        if(hp <= 0)
        {
            if(inventory != null)
            {
                amount = Random.Range(minAmount, maxAmount+1);
                inventory.AddItem(outputIndexNum, amount);
            }
            RevokeTile();
            Destroy(this.gameObject);
        }
    }

    void RevokeTile()
    {
        Vector3 pos = this.transform.position;
        if (pos.x < 0)
        {
            pos.x--;
        }
        if (pos.y < 0)
        {
            pos.y--;
        }

        Vector3Int resourcePos = new Vector3Int((int)pos.x, (int)pos.y, 0);
        TileManager.Instance.RevokePlace(resourcePos);
    }
}
