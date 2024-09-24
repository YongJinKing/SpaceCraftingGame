using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Net.Http.Headers;


public class RiggingItemContents : MonoBehaviour
{
    [SerializeField] private Transform RiggingItemSlot;
    EquipInven EI;
    private RiggingItemSlot riggingItemSlot;
    private ItemUpgradePopup itemUpgradePopup;
    BuildingUIWarningSign warningSign;
    public event EventHandler OnLevelUpAct;
    GunManager weaponManager;
    int[] weaponIndexes = new int[3];

    private int[] riggingItemLevelData = {1, 1, 1, 1, 1};
    private int rifleLevel = 1;
    private int shotGunLevel = 1;
    private int sniperLevel = 1;
    private int pickLevel = 1;
    private int hammerLevel = 2;
    private int levelUpSlotNum = 0;

    private int riggingItemCount = 3;
    private void Start() 
    {
        EI = FindObjectOfType<EquipInven>();
        warningSign = FindObjectOfType<BuildingUIWarningSign>();

        var instance = RiggingItemStaticDataManager.GetInstance();
        instance.LoadRiggingItemDatas();
        weaponManager = FindObjectOfType<GunManager>();

        weaponIndexes = weaponManager.LoadGunIndexes();
        Debug.Log(weaponIndexes[0] + ", " + weaponIndexes[1] + ", " + weaponIndexes[2] + " <<<<<<<<<<<<<<<<<<<<<<<<<<<<");
        rifleLevel = instance.dicRiggingItemData[++weaponIndexes[0]].RiggingItemLevel;
        shotGunLevel = instance.dicRiggingItemData[++weaponIndexes[1]].RiggingItemLevel;
        sniperLevel = instance.dicRiggingItemData[++weaponIndexes[2]].RiggingItemLevel;

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
        levelUpSlotNum = index;
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
            
            if(checkInven)
            {
                for(int i = 0; i < AlarmContents.childCount; i++)
                {
                    Inventory.instance.UseItem
                    (AlarmContents.GetChild(i).GetComponent<ResourceSlot>().resourceId,
                    AlarmContents.GetChild(i).GetComponent<ResourceSlot>().resourceAmount);
                }
                SoundManager.Instance.PlaySFX(SoundManager.Instance.UISound.SuccesSFX);
                int lv = riggingItemLevelData[levelUpSlotNum];
                transform.GetChild(0).GetChild(0).GetChild(0).GetChild(levelUpSlotNum).GetComponent<RiggingItemSlot>().Init(levelUpSlotNum, ++riggingItemLevelData[levelUpSlotNum]);
                EI.Upgrade(levelUpSlotNum, lv);
            }
            else
            {
                warningSign.TurnOnNoMoreResources();
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
