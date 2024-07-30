using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class NaturalResources : MonoBehaviour
{
    public int indexNum; // 자원의 인덱스 넘버
    public int outputIndexNum; // 자원을 캤을 때 산출물의 인덱스 넘버
    public int hp; // 자원의 hp = 몇번 뚜들겨야 캐질지
    public int minAmount; // 최소 생산량
    public int maxAmount; // 최대 생산량
    public int size;
    int amount; // 실제 생산량
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
