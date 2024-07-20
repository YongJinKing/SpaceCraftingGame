using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourceSlot : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private TMP_Text text;
    public int resourceId{ get; private set; }
    public int resourceAmount{ get; private set; }
    public void DestroySelf()
    {
        Destroy(gameObject);
    }
    public void Init(int id, int Amout)
    {
        resourceId = id;
        resourceAmount = Amout;
        var itemInstance = ItemStaticDataManager.GetInstance();
        itemInstance.LoadItemDatas();
        var itemData = itemInstance.dicItemData[id];
        var spName = itemInstance.dicResouseTable[itemData.ItemImage].ImageResource_Name;
        this.image.sprite = Resources.Load<Sprite>($"UI/ItemResources/{spName}");
        this.text.text = itemInstance.dicStringTable[itemData.ItemName].String_Desc + " x" + Amout;
    }
    
    
}
