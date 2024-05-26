using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour
{
    
    
    
    [Serializable]
    public class SlotItemData
    {
        public int id;
        public int Amount;
    }
    SelelctType InvenSoltType;
    public List<SlotItemData> InventoryDatas = new List<SlotItemData>();
    public static Inventory instance;
    
    public UnityEvent UpdatePopup;

    public List<SlotItemData> DisplayInven;
    public int Testid;
    public int TestAmout;


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
    private void Start() 
    {
        InvenSoltType = SelelctType.All;
    }
    
    public void Testbtn()
    {
        AddItem(Testid,TestAmout);
    }
    public void AddItem(int id, int Amount)
    {
        SlotItemData naturalResourceData = new SlotItemData();
        for(int i = 0; i < InventoryDatas.Count; i++)
        {
            if(InventoryDatas[i].id == id)
            {
                InventoryDatas[i].Amount += Amount;
                UpdatePopup?.Invoke();
                return;
            }
        }
        naturalResourceData.id = id;
        naturalResourceData.Amount = Amount;
        InventoryDatas.Add(naturalResourceData);

        SortInventoryDatas();
        ModeDisplay(InvenSoltType);
        UpdatePopup?.Invoke();
    }
    public void ChangeMode(int index)
    {
        InvenSoltType = (SelelctType)index;
        ModeDisplay(InvenSoltType);
    }

    void ModeDisplay(SelelctType Type)
    {
        DisplayInven = InventoryDatas;
        if(Type == SelelctType.All)
            return;
        else
        {
            
        }
    }
    void SortInventoryDatas()
    {
        for(int i = 0; i < InventoryDatas.Count - 1; i++)//ItemSort
        {
            for(int j = 0; j < InventoryDatas.Count - 1; j++)
            {
                if(InventoryDatas[j].id > InventoryDatas[j + 1].id)
                {
                    SlotItemData Temp = InventoryDatas[j];
                    InventoryDatas[j] = InventoryDatas[j + 1];
                    InventoryDatas[j + 1] = Temp;
                }
            }
        }
    }
    public void UseItem(int id, int Amount)
    {
        for(int i = 0; i <InventoryDatas.Count; i++)
        {
            if(InventoryDatas[i].id == id)
            {
                if(InventoryDatas[i].Amount >= Amount)
                {
                    InventoryDatas[i].Amount -= Amount;
                }
                else
                {
                    Debug.Log("재료 부족");
                }
            }
            else
            {
                Debug.Log("재료 없음");
            }
        }
    }

}
