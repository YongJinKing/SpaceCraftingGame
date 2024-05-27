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
    
    public UnityEvent<int> UpdatePopup;

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
        ItemStaticDataManager.GetInstance().LoadItemDatas();
        InvenSoltType = SelelctType.All;
    }
    
    public void Testbtn()
    {
        AddItem(Testid,TestAmout);
    }
    public void AddItem(int id, int Amount)
    {
        SlotItemData SlotData = new SlotItemData();
        for(int i = 0; i < InventoryDatas.Count; i++)
        {
            if(InventoryDatas[i].id == id)
            {
                InventoryDatas[i].Amount += Amount;
                UpdatePopup?.Invoke(0);
                return;
            }
        }
        SlotData.id = id;
        SlotData.Amount = Amount;
        InventoryDatas.Add(SlotData);

        SortInventoryDatas();
        ModeDisplay(InvenSoltType);
        UpdatePopup?.Invoke(0);
    }
    public void ChangeMode(int index)
    {
        
        InvenSoltType = (SelelctType)index;
        ModeDisplay(InvenSoltType);
        UpdatePopup?.Invoke(1);
        //Debug.Log($"모드체인지{InvenSoltType}");
        
        
    }

    void ModeDisplay(SelelctType Type)
    {
        DisplayInven = InventoryDatas;
        if(Type == SelelctType.All)
            return;
        else
        {
            DisplayInven = new List<SlotItemData>();
            SlotItemData SlotData = new SlotItemData();
            for(int i = 0; i < InventoryDatas.Count; i++)
            {
                var ItemData = ItemStaticDataManager.GetInstance().dicItemData[InventoryDatas[i].id];
                //Debug.Log($"{ItemData.ItemType}아이템 타입, {Type}모드 타입");
                if(ItemData.ItemType + 1 == (int)Type)
                {
                    SlotData.id = InventoryDatas[i].id;
                    SlotData.Amount = InventoryDatas[i].Amount;
                    DisplayInven.Add(SlotData);
                    
                }
            }
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
