using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ConstructionSite : MonoBehaviour
{
    public Transform[] buildingDrons;

    public UnityEvent finishWorkEvent;

    [SerializeField] int index;
    [SerializeField] Vector3 pos;
    [SerializeField] float Hp;
    [SerializeField] int size;
    [SerializeField] float craftingTime = 4f;
    
    public void SetIndex(int _index)
    {
        index = _index;
    }
    public void SetInPos(Vector3 _pos)
    {
        pos = _pos;
    }
    public void SetHp(float _hp)
    {
        Hp = _hp;
    }
    public void SetSize(int _size)
    {
        size = _size;
    }

    public void SetCraftingTime(float _time)
    {
        craftingTime = _time;
    }

    public void StartBuilding()
    {
        StartCoroutine(CraftBuildingCoroutine());
    }

    IEnumerator CraftBuildingCoroutine()
    {
        yield return new WaitForSeconds(10f);

        finishWorkEvent?.Invoke();
        GameObject obj = CraftFactory.Instance.CraftBuilding(index, pos, Hp, size);
        Vector3Int tmpPos = new Vector3Int((int)(pos.x - TileManager.Instance.tileMap.tileAnchor.x), (int)(pos.y - TileManager.Instance.tileMap.tileAnchor.y), (int)pos.z);
        TileManager.Instance.RemopvePlace(tmpPos, obj, size);
        Destroy(this.gameObject);
    }

}
