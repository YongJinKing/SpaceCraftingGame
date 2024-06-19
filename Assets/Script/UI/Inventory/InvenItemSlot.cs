using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InvenItemSlot : MonoBehaviour
{
    public Image image;
    public TMP_Text AmountTxt;

    int id;
    string ItemName;
    string ItemDesc;
    int ItemValue;
    string spName;
    
    private void Start() 
    {
        ItemStaticDataManager.GetInstance().LoadItemDatas();
    }
    public void Display(int index)//설명생략
    {
        var ItemData = ItemStaticDataManager.GetInstance().dicItemData[Inventory.instance.DisplayInven[transform.GetSiblingIndex() + (index * 25)].id];
        var SpriteData = ItemStaticDataManager.GetInstance().dicResouseTable[ItemData.ItemImage];
        AmountTxt.text = "x" + UnitCalculate.GetInstance().Calculate(Inventory.instance.DisplayInven[transform.GetSiblingIndex() + (index * 25)].Amount);
        spName = SpriteData.ImageResource_Name;
        Sprite sp = Resources.Load<Sprite>($"UI/ItemResources/{spName}");
        image.sprite = sp;
    }
    public void init()
    {
        AmountTxt.text = "";
    }

}
