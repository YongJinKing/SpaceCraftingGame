using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class NaturalResources : Stat
{
    public int indexNum; // 자원의 인덱스 넘버
    public int outputIndexNum; // 자원을 캤을 때 산출물의 인덱스 넘버
    public float hp; // 자원의 hp = 몇번 뚜들겨야 캐질지
    public int minAmount; // 최소 생산량
    public int maxAmount; // 최대 생산량
    public int size;
    public GameObject DestroyVFX; // 파괴시 나타날 VFX
    public GameObject dropItem; // 드랍 아이템 프리펩

    int amount; // 실제 생산량
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
