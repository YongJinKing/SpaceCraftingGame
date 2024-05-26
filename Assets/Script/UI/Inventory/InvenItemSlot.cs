using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InvenItemSlot : MonoBehaviour
{
    public Image image;
    public TMP_Text AmountTxt;

    public int id;
    public string ItemName;
    public string ItemDesc;
    public int ItemValue;
    public string spName;
    
    private void Start() 
    {
        ItemStaticDataManager.GetInstance().LoadItemDatas();
    }
    public void Display()
    {
        var ItemData = ItemStaticDataManager.GetInstance().dicItemData[Inventory.instance.DisplayInven[transform.GetSiblingIndex()].id];
        var SpriteData = ItemStaticDataManager.GetInstance().dicResouseTable[ItemData.ItemImage];
        AmountTxt.text = Inventory.instance.DisplayInven[transform.GetSiblingIndex()].Amount.ToString();
        spName = SpriteData.ImageResource_Name;
        Sprite sp = Resources.Load<Sprite>($"UI/ItemResources/{spName}");
        image.sprite = sp;
        transform.gameObject.SetActive(true);
    }
}
