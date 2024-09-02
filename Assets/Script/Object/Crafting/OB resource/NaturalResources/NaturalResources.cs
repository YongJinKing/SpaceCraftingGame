using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class NaturalResources : Stat
{
    public int indexNum; // �ڿ��� �ε��� �ѹ�
    public int outputIndexNum; // �ڿ��� ĺ�� �� ���⹰�� �ε��� �ѹ�
    public float hp; // �ڿ��� hp = ��� �ѵ�ܾ� ĳ����
    public int minAmount; // �ּ� ���귮
    public int maxAmount; // �ִ� ���귮
    public int size;
    public GameObject DestroyVFX; // �ı��� ��Ÿ�� VFX
    public GameObject dropItem; // ��� ������ ������

    int amount; // ���� ���귮
    Inventory inventory;

    protected override void Initialize()
    {
        this[EStat.MaxHP] = hp;
        this[EStat.HP] = this[EStat.MaxHP];
    }
    private void OnEnable()
    {
        //inventory = FindObjectOfType<Inventory>();
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

    public override void TakeDamage(float damage)
    {
        Debug.Log($"NaturalResources.TakeDamage, now HP is {this[EStat.HP]}");
        this[EStat.HP] -= damage;
        if (this[EStat.HP] <= 0)
        {
            /*if (inventory != null)
            {
                amount = Random.Range(minAmount, maxAmount + 1);
                inventory.AddItem(outputIndexNum, amount);
            }*/
            amount = Random.Range(minAmount, maxAmount + 1);
            SpawnDropItem(amount);
            Instantiate(DestroyVFX, this.transform.position, Quaternion.identity, null);
            RevokeTile();
            Destroy(this.gameObject);
        }
    }

    void SpawnDropItem(int cnt)
    {
        for(int i = 0; i < cnt; i++)
        {
            Instantiate(dropItem, this.transform.position,Quaternion.identity,null);
        }
    }
}
