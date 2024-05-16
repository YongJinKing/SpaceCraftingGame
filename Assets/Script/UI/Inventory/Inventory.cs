using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    
    
    public class NaturalResourceData
    {
        public int id;
        public int Amount;
    }
    public static Inventory instance;
    List<NaturalResourceData> InventoryDatas = new List<NaturalResourceData>();
    public List<NaturalResourceData> DisplayInven;

    private void Awake()    //싱글톤
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(instance.gameObject);
        }
    }
    public void AddItem(int id, int Amount)
    {
        NaturalResourceData naturalResourceData = new NaturalResourceData();
        for(int i = 0; i < InventoryDatas.Count; i++)
        {
            if(InventoryDatas[i].id == id)
            {
                InventoryDatas[i].Amount += Amount;
                return;
            }
        }
        naturalResourceData.id = id;
        naturalResourceData.Amount = Amount;
        InventoryDatas.Add(naturalResourceData);
    }
    public void SoltList(int SoltNum)
    {
        DisplayInven = new List<NaturalResourceData>();
        if(SoltNum == 0)
        {
            DisplayInven = InventoryDatas;
        }
    }
}
