using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;
using UnityEditorInternal.Profiling.Memory.Experimental;

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

    public List<SlotItemData> DisplayInven= new List<SlotItemData>();

    private int inventoryDatasMaxCount;
    private int slotMaxCount;
    public static Inventory instance;
    
    public UnityEvent updatePopup;
    public UnityEvent<int, int> buildUIChangeAct;
    
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
        DisplayInven.AddRange(InventoryDatas);
        
        
    }
    private void Start() 
    {
        ItemStaticDataManager.GetInstance().LoadItemDatas();//Json뿌리기
        
    }
    
    public void TestAddBtn()
    {
        int check = AddItem(Testid,TestAmout);
        Debug.Log(check);
        
    }
     public void TestUseBtn()
    {
        UseItem(Testid, TestAmout);
        
    }

    public bool CheckInvenLeft(int id, int amount)
    {
        int remainingAmout = amount;
        for (int i = 0; i < inventoryDatasMaxCount; i++)
        {
            if (InventoryDatas[i].id == id)//아이템이 같으면 실행
            {
                if (InventoryDatas[i].amount >= slotMaxCount) continue;//최대 계수 이상을 가진 아이템 컨티뉴

                if (remainingAmout >= slotMaxCount)//최대 갯수보다 잔여 갯수의 아이템 양이 많으면
                {
                    int spaceLeft = slotMaxCount - InventoryDatas[i].amount;// 최대 갯수에서 현재 인벤토리 아이템 갯수를 마이너스하여 잔여값을 서칭함
                    if (spaceLeft > 0)
                    {
                        remainingAmout -= spaceLeft;
                    }
                }
                else
                {
                    
                    int spaceLeft = slotMaxCount - InventoryDatas[i].amount;
                    if (spaceLeft >= remainingAmout)
                    {
                        return true;
                    }
                    else
                    {
                        remainingAmout -= spaceLeft;
                    }
                }

                if (remainingAmout <= 0)
                {
                    return true;
                }
            }
        }

        for (int i = 0; i < inventoryDatasMaxCount; i++)// 나머지값 빈자리에 정착
        {
            if (InventoryDatas[i].id == 0 && remainingAmout > 0)
            {
                return true;
            }
        }

        return false;
    }

    public int AddItem(int id, int amount)
    {
        int remainingAmout = amount;

        for(int i = 0; i < inventoryDatasMaxCount; i++)//똑같은 id를 가지고 있는 아이템 서칭
        {
            if(InventoryDatas[i].id == id)//아이템이 같으면 실행
            {
                if(InventoryDatas[i].amount >= slotMaxCount) continue;//최대 계수 이상을 가진 아이템 컨티뉴

                if(remainingAmout >= slotMaxCount)//최대 갯수보다 잔여 갯수의 아이템 양이 많으면
                {
                    int spaceLeft = slotMaxCount - InventoryDatas[i].amount;// 최대 갯수에서 현재 인벤토리 아이템 갯수를 마이너스하여 잔여값을 서칭함
                    if(spaceLeft > 0)
                    {
                        InventoryDatas[i].amount += spaceLeft;
                        remainingAmout -= spaceLeft;
                    }
                }

                else
                {
                    /*int spaceLeft = InventoryDatas[i].amount + remainingAmout - slotMaxCount;
                    if (spaceLeft > 0)
                    {
                        InventoryDatas[i].amount += spaceLeft;
                        remainingAmout -= spaceLeft;
                    }
                    else
                    {
                        InventoryDatas[i].amount += remainingAmout;
                        remainingAmout = 0;
                    }*/
                    int spaceLeft = slotMaxCount - InventoryDatas[i].amount;
                    if (spaceLeft >= remainingAmout)
                    {
                        InventoryDatas[i].amount += remainingAmout;
                        remainingAmout = 0;
                    }
                    else
                    {
                        InventoryDatas[i].amount += spaceLeft;
                        remainingAmout -= spaceLeft;
                    }
                }
                if(remainingAmout <= 0)
                {  
                    UpdateInventory();
                    buildUIChangeAct?.Invoke(id, GetItemAmount(id));
                    return 0;
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
                UpdateInventory();
                if(remainingAmout <= 0)
                {
                    buildUIChangeAct?.Invoke(id, GetItemAmount(id));
                    return 0;
                }
            }
        }
        UpdateInventory();

        
        Debug.Log(remainingAmout);//창고 채운뒤 남은 오브젝트 처리하면 좋을듯?
        return remainingAmout;

    }

    private void UpdateInventory()
    {
        SortInventoryDatas();
        ModeDisplay(InvenSoltType);
        updatePopup?.Invoke();
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
            updatePopup?.Invoke();
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
            updatePopup?.Invoke();
        }
    }

    

    public void UseItem(int id, int amount)// 안보셔도되요
    {
        if(GetItemCheck(id, amount))
        {
            int remainingAmout = amount;

            for(int i = inventoryDatasMaxCount - 1; i >= 0; i--)
            {
                if (InventoryDatas[i].id == id)
                {
                    if(remainingAmout >= InventoryDatas[i].amount)
                    {
                        remainingAmout -= InventoryDatas[i].amount;
                        InventoryDatas[i].id = 0;
                        InventoryDatas[i].amount = 0;
                    }

                    else
                    {
                        int spaceLeft = InventoryDatas[i].amount - remainingAmout;
                        InventoryDatas[i].amount = spaceLeft;
                        remainingAmout = 0;
                        if(InventoryDatas[i].amount <= 0)
                        {
                            InventoryDatas[i].id = 0;
                            InventoryDatas[i].amount = 0;
                        }
                    }
                }

                if (remainingAmout <= 0)
                {
                    UpdateInventory();
                    return;
                }
            }


        }
        else
        {
            Debug.Log("아이템 없음");
            return ;
        }
        
    }


    public bool GetItemCheck(int id, int amount)
    {
        int remainingAmout = amount;

        for(int i = inventoryDatasMaxCount - 1; i >= 0; i--)
        {
            if(InventoryDatas[i].id == id)
            {
                remainingAmout -= InventoryDatas[i].amount;
            }
        }
        if(remainingAmout <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public int GetItemAmount(int id)
    {
        int res = 0;
        for (int i = 0; i < inventoryDatasMaxCount; i++)//똑같은 id를 가지고 있는 아이템 서칭
        {
            if (InventoryDatas[i].id == id)
            {
                res += InventoryDatas[i].amount;
            }
        }
        Debug.Log(res + " <<<<<<<<<<<<<<<<");
        return res;
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
    private void ClearDisplayInven()
    {
        DisplayInven.Clear();
        for(int i = 0; i < inventoryDatasMaxCount; i++)
        {
            DisplayInven.Add(new SlotItemData());
        }
    }
    private void SortInventoryDatas()//아이디 순서대로 정렬하는 버블 정렬 알고리즘
    {
        /*for(int i = 0; i < GetInvenDataWithIdLength() - 1; i++)//ItemSort
        {
            for(int j = 0; j < GetInvenDataWithIdLength() - 1; j++)
            {
                if (InventoryDatas[j].id < InventoryDatas[j + 1].id)
                {
                    SlotItemData Temp = InventoryDatas[j];
                    InventoryDatas[j] = InventoryDatas[j + 1];
                    InventoryDatas[j + 1] = Temp;
                }
            }
        }*/

        for (int i = 0; i < InventoryDatas.Count - 1; i++)//ItemSort
        {
            for (int j = 1; j < InventoryDatas.Count - i; j++)
            {
                if(InventoryDatas[j].id > InventoryDatas[j - 1].id)
                {
                    SlotItemData Temp = InventoryDatas[j];
                    InventoryDatas[j] = InventoryDatas[j - 1];
                    InventoryDatas[j - 1] = Temp;
                }
            }
        }
    }

}
