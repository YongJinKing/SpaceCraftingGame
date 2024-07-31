using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour
{
    
    
    
    [Serializable]
    public class SlotItemData//슬롯에 필요한 데이터 설정 세찬님은 필요없음
    {
        public int id;
        public int Amount;
    }
    InvenSelelctType InvenSoltType;
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
        ItemStaticDataManager.GetInstance().LoadItemDatas();//Json뿌리기
        InvenSoltType = InvenSelelctType.all;
    }
    
    public void Testbtn()
    {
        AddItem(Testid,TestAmout);
    }

    public void AddItem(int id, int Amount)//이부분도 안보셔도되요
    {
        SlotItemData SlotData = new SlotItemData();
        for(int i = 0; i < InventoryDatas.Count; i++)
        {
            if(InventoryDatas[i].id == id)
            {
                InventoryDatas[i].Amount += Amount;
                UpdatePopup?.Invoke(0);
                ModeDisplay(InvenSoltType);
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

    public void ChangeMode(int index)//이부분 보셔야됨
    {
        
        InvenSoltType = (InvenSelelctType)index;//인벤토리 타입 어떤것으로 할지 Int값 전송
        ModeDisplay(InvenSoltType);//Mode에 알맞는 디스플레이 시작
        UpdatePopup?.Invoke(1);//디스플레이 완료 후 팝업창 업데이트
    }

    void ModeDisplay(InvenSelelctType Type)
    {
        DisplayInven = InventoryDatas;//전체 보여줄 땐 다 보여줘야 되니까 invendata그대로 display데이터로 송신
        if(Type == InvenSelelctType.all)//all일땐 리턴
            return;
        else
        {
            DisplayInven = new List<SlotItemData>();//Display데이터 초기화
            for(int i = 0; i < InventoryDatas.Count; i++)//인벤토리에 있는 아이템 만큼 포문돌리기 세찬님은 TypeID만큼 돌리세요
            {
                var ItemData = ItemStaticDataManager.GetInstance().dicItemData[InventoryDatas[i].id];//해당 아이디의 타입 불러오기
                //Debug.Log($"{ItemData.ItemType}아이템 타입, {Type}모드 타입");
                if(ItemData.ItemType + 1 == (int)Type)// InvenSoltType의 타입과 불러온 아이디의 타입이 일치할 시
                {
                    //Debug.Log("실행 채크");
                    SlotItemData SlotData = new SlotItemData();
                    SlotData.id = InventoryDatas[i].id;
                    SlotData.Amount = InventoryDatas[i].Amount;
                    DisplayInven.Add(SlotData);//DisplayInven에 해당 아이디 추가
                }
            }
        }
    }
    void SortInventoryDatas()//아이디 순서대로 정렬하는 버블 정렬 알고리즘
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
    public void UseItem(int id, int Amount)// 안보셔도되요
    {
        for(int i = 0; i < InventoryDatas.Count; i++)
        {
            if(InventoryDatas[i].id == id)
            {
                if(InventoryDatas[i].Amount >= Amount)
                {
                    InventoryDatas[i].Amount -= Amount;
                    if(InventoryDatas[i].Amount <= 0)
                    {
                        InventoryDatas.RemoveAt(i); 
                    }
                    UpdatePopup?.Invoke(0);
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
    public bool GetItemCheck(int id, int Amount)
    {
        for(int i = 0; i < InventoryDatas.Count; i++)
        {
            if(InventoryDatas[i].id == id)
            {
                if(InventoryDatas[i].Amount >= Amount)
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

}
