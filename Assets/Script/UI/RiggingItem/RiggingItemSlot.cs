using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RiggingItemSlot : MonoBehaviour
{
    [SerializeField] private Image riggingItemImage;
    [SerializeField] private TMP_Text riggingItemName;
    [SerializeField] private TMP_Text riggingItemAttackPower;
    [SerializeField] private TMP_Text riggingItemAttackSpeed;
    private int id;

   

    public void Init(int id, int level)
    {
        var instance = RiggingItemStaticDataManager.GetInstance();
        instance.LoadRiggingItemDatas();
        foreach(var riggingItem in instance.dicRiggingItemData)
        {
            if(riggingItem.Value.RiggingItemType == id && riggingItem.Value.RiggingItemLevel == level)
            {
                UpdateSlot(riggingItem);   
            }
        }
    }
    public int GetSlotId()
    {
        return id;
    }
    private void UpdateSlot(KeyValuePair<int, RiggingItem_DataTable> riggingItem)
    {
        var instance = RiggingItemStaticDataManager.GetInstance();
        instance.LoadRiggingItemDatas();
        this.id = riggingItem.Key;
        var spriteData = instance.dicRiggingResouseTable[riggingItem.Value.RiggingItemImage].ImageResource_Name;
        var nameData = instance.dicRiggingStringTable[riggingItem.Value.RiggingItemName].String_Desc;
        var attackPowerData = riggingItem.Value.RiggingItem_AttackPower;
        var attackSpeedData = riggingItem.Value.RiggingItem_AttackSpeed;
        riggingItemImage.sprite = Resources.Load<Sprite>($"UI/RiggingItem/{spriteData}");
        riggingItemName.text = "이름 : " + nameData;
        riggingItemAttackPower.text = "공격력 : " + attackPowerData.ToString();
        riggingItemAttackSpeed.text = "공격 속도 : " + attackSpeedData.ToString();
    }
    /* public void LevelUpCheck(KeyValuePair<int, RiggingItem_DataTable> riggingItem)
    {
         var instance = RiggingItemStaticDataManager.GetInstance();
        instance.LoadRiggingItemDatas();
    } */
}
