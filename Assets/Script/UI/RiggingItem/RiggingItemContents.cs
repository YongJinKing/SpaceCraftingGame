using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Net.Http.Headers;


public class RiggingItemContents : MonoBehaviour
{
    [SerializeField] private Transform RiggingItemSlot;

    private RiggingItemSlot riggingItemSlot;
    private ItemUpgradePopup itemUpgradePopup;
    public event EventHandler OnLevelUpAct;

    private int[] riggingItemLevelData = {1, 1, 1, 1, 1};
    private int rifleLevel = 1;
    private int shotGunLevel = 1;
    private int sniperLevel = 1;
    private int pickLevel = 1;
    private int hammerLevel = 2;

    private int riggingItemCount = 3;
    private void Start() 
    { 
        riggingItemLevelData[0] = rifleLevel;
        riggingItemLevelData[1] = shotGunLevel;
        riggingItemLevelData[2] = sniperLevel;
        riggingItemLevelData[3] = pickLevel;
        riggingItemLevelData[4] = hammerLevel;
        for(int i = 0; i < riggingItemCount; i++)
        {
            Instantiate(RiggingItemSlot, transform.GetChild(0).GetChild(0).GetChild(0));
            transform.GetChild(0).GetChild(0).GetChild(0).GetChild(i).GetComponent<RiggingItemSlot>().Init(i, riggingItemLevelData[i]);
            
        }
        Button[] LevelUpBtnList = transform.GetChild(0).GetChild(0).GetChild(0).GetComponentsInChildren<Button>();
        for(int i = 0; i < LevelUpBtnList.Length; i++)
        {
            int index = i;
            LevelUpBtnList[i].onClick.AddListener(() => PressedLevelUpBtn(index));
        } 
    }
    private void PressedLevelUpBtn(int index)
    {
        transform.GetChild(1).gameObject.SetActive(true);
        transform.GetChild(1).GetComponent<AlarmPopup>().
        UpdatePopup(transform.GetChild(0).GetChild(0).GetChild(0).GetChild(index).GetComponent<RiggingItemSlot>().GetSlotId());
        //
    }
    public void AlarmPopupBtnAct(int index)
    {
        var AlarmContents = transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).GetChild(0);
        bool checkInven = false;
        if(index == 0)
        {
            for(int i = 0; i < AlarmContents.childCount; i++)
            {
                if(!Inventory.instance.GetItemCheck
                (AlarmContents.GetChild(i).GetComponent<ResourceSlot>().resourceId, 
                AlarmContents.GetChild(i).GetComponent<ResourceSlot>().resourceAmount))
                {
                    checkInven = false;
                    break;
                }
                checkInven = true;
            }
            Debug.Log(checkInven);
            if(checkInven)
            {
                for(int i = 0; i < AlarmContents.childCount; i++)
                {
                    Inventory.instance.UseItem
                    (AlarmContents.GetChild(i).GetComponent<ResourceSlot>().resourceId,
                    AlarmContents.GetChild(i).GetComponent<ResourceSlot>().resourceAmount);
                }
                transform.GetChild(0).GetChild(0).GetChild(0).GetChild(index).GetComponent<RiggingItemSlot>().Init(index, ++riggingItemLevelData[index]);
            }
            else
            {
                Debug.Log("아이템 없음");
            }
            for(int i = 0; i < AlarmContents.childCount; i++)
            {
                AlarmContents.GetChild(i).GetComponent<ResourceSlot>().DestroySelf();
            }
            
        }
        if(index == 1)
        {
            for(int i = 0; i < AlarmContents.childCount; i++)
            {
                AlarmContents.GetChild(i).GetComponent<ResourceSlot>().DestroySelf();
            }
        }
        transform.GetChild(1).gameObject.SetActive(false);
        
    }
    
    
    
}
