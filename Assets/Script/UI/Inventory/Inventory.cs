using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;

public class Inventory : MonoBehaviour
{
    
    
    
    [Serializable]
    public class SlotItemData//슬롯에 필요한 데이터 설정 세찬님은 필요없음
    {
        public int id;
        public int amount;
        // 복사 생성자 추가
        public SlotItemData(SlotItemData other)
        {
            id = other.id;
            amount = other.amount;
        }
        public SlotItemData()
        {

        }
    }
    InvenSelelctType InvenSoltType;
    public List<SlotItemData> InventoryDatas = new List<SlotItemData>();

    private int inventoryDatasMaxCount;
    private int slotMaxCount;
    public static Inventory instance;
    
    public UnityEvent UpdatePopup;

    public List<SlotItemData> DisplayInven= new List<SlotItemData>();
    public int Testid;
    public int TestAmout;


    private void Awake()    //싱글톤
    {
        
        instance = this;

        InvenSoltType = InvenSelelctType.all;

        inventoryDatasMaxCount = 25;

        slotMaxCount = 99;

        

        for(int i = 0; i < inventoryDatasMaxCount; i++)
        {
            SlotItemData slotItemData = new SlotItemData();
            InventoryDatas.Add(slotItemData);
        }
        foreach(SlotItemData slotItemData in InventoryDatas)
        {
            DisplayInven.Add(slotItemData);
        }
        
        
    }
    private void Start() 
    {
        ItemStaticDataManager.GetInstance().LoadItemDatas();//Json뿌리기
        
    }
    
    public void Testbtn()
    {
        AddItem(Testid,TestAmout);
    }

    

    public bool AddItem(int id, int amount)
    {
        int remainingAmout = amount;

        for(int i = 0; i < inventoryDatasMaxCount; i++)
        {
            if(InventoryDatas[i].id == id)
            {
                int spaceLeft = slotMaxCount - InventoryDatas[i].amount;
                if(spaceLeft > 0)
                {
                    InventoryDatas[i].amount += spaceLeft;
                    remainingAmout -= spaceLeft;
                    SortInventoryDatas();
                    ModeDisplay(InvenSoltType);
                    UpdatePopup?.Invoke();
                    if(remainingAmout <= 0)
                    {   
                        return true;
                    }
                }
            }
        }
        for(int i = 0; i < inventoryDatasMaxCount; i++)// 나머지값 빈자리에 정착
        {
            if(InventoryDatas[i].id == 0 && remainingAmout > 0)
            {
                int addAmount = Mathf.Min(remainingAmout, slotMaxCount);
                InventoryDatas[i].id = id;
                InventoryDatas[i].amount = addAmount;
                remainingAmout -= addAmount;
                SortInventoryDatas();
                ModeDisplay(InvenSoltType);
                UpdatePopup?.Invoke();
                if(remainingAmout <= 0)
                {   
                    return true;
                }
            }
        }
        return false;

    }
    public void ChangeMode(int index)//이부분 보셔야됨
    {
        
        InvenSoltType = (InvenSelelctType)index;//인벤토리 타입 어떤것으로 할지 Int값 전송
        ModeDisplay(InvenSoltType);//Mode에 알맞는 디스플레이 시작
    }

    void ModeDisplay(InvenSelelctType Type)
    {
        DisplayInven.Clear();
        foreach(SlotItemData slotItemData in InventoryDatas)//전체 보여줄 땐 다 보여줘야 되니까 invendata그대로 display데이터로 송신
        {
            DisplayInven.Add(new SlotItemData(slotItemData));
        }
        if(Type == InvenSelelctType.all)//all일땐 리턴
        {
            UpdatePopup?.Invoke();
            return;
        }
            
        else
        {   
            ClearDisplayInven();
            int j = 0;
            for(int i = 0; i < GetInvenDataWithIdLength(); i++)//인벤토리에 있는 아이템 만큼 포문돌리기 세찬님은 TypeID만큼 돌리세요
            {
                var ItemData = ItemStaticDataManager.GetInstance().dicItemData[InventoryDatas[i].id];//해당 아이디의 타입 불러오기
                if(ItemData.ItemType + 1 == (int)Type)// InvenSoltType의 타입과 불러온 아이디의 타입이 일치할 시
                {
                    int id = InventoryDatas[i].id;
                    int amount = InventoryDatas[i].amount;
                    DisplayInven[j].id = id;
                    DisplayInven[j].amount = amount; 
                    j++;
                }
            }
            UpdatePopup?.Invoke();
        }
    }

    private void ClearDisplayInven()
    {
        DisplayInven.Clear();
        for(int i = 0; i < 25; i++)
        {
            DisplayInven.Add(new SlotItemData());
        }
    }
    void SortInventoryDatas()//아이디 순서대로 정렬하는 버블 정렬 알고리즘
    {
        for(int i = 0; i < GetInvenDataWithIdLength() - 1; i++)//ItemSort
        {
            for(int j = 0; j < GetInvenDataWithIdLength() - 1; j++)
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
    public void UseItem(int id, int Amount)// 안보셔도되요
    {
        /* for(int i = 0; i < InventoryDatas.Count; i++)
        {
            if(InventoryDatas[i].id == id)
            {
                if(InventoryDatas[i].amount >= Amount)
                {
                    InventoryDatas[i].amount -= Amount;
                    if(InventoryDatas[i].amount <= 0)
                    {
                        InventoryDatas.RemoveAt(i); 
                    }
                    UpdatePopup?.Invoke();
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
        } */
    }
    public bool GetItemCheck(int id, int Amount)
    {
        for(int i = 0; i < InventoryDatas.Count; i++)
        {
            if(InventoryDatas[i].id == id)
            {
                if(InventoryDatas[i].amount >= Amount)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        return false;
    }




    public int GetInvenDataWithIdLength()
    {
        int count = 0;
        for(int i = 0; i < inventoryDatasMaxCount; i++)
        {
            if(InventoryDatas[i].id > 0)
            {
                count++;
            }
        }
        return count;
    }
    public int GetDisplayInvenDataWithIdLength()
    {
        int count = 0;
        for(int i = 0; i < GetInvenDataWithIdLength(); i++)
        {
            if(DisplayInven[i].id > 0)
            {
                count++;
            }
        }
        return count;
    }

}
