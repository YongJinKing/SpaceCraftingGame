using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.Linq;

public class ItemStaticDataManager
{
    private static ItemStaticDataManager instance;
    public Dictionary<int, Item_DataTable> dicItemData;
    public Dictionary<int, Item_StringTable> dicStringTable;
    public Dictionary<int, Item_ImageResourceTable> dicResouseTable;
    
    public static ItemStaticDataManager GetInstance()
    {
        if(ItemStaticDataManager.instance == null)
            ItemStaticDataManager.instance = new ItemStaticDataManager();
        return ItemStaticDataManager.instance;
    }
    public void LoadItemDatas()
    {
        var Item_DataTable = Resources.Load<TextAsset>("UI/Json/Pexplorer_Item_DataTable").text;
        var Item_StringTable = Resources.Load<TextAsset>("UI/Json/Pexplorer_Item_StringTable").text;
        var Item_IamgeResourceTable = Resources.Load<TextAsset>("UI/Json/Pexplorer_Item_ImageResourceTable").text;
        
        var arrItem_DataTables = JsonConvert.DeserializeObject<Item_DataTable[]>(Item_DataTable);
        var arrStringDatas = JsonConvert.DeserializeObject<Item_StringTable[]>(Item_StringTable);
        var arrResourceDatas = JsonConvert.DeserializeObject<Item_ImageResourceTable[]>(Item_IamgeResourceTable);
        /* foreach(var data in arrStringDatas)
        {
            Debug.LogFormat("{0}, {1}, {2} ",data.index, data.String_Type, data.String_Desc);
        } */
        this.dicItemData = arrItem_DataTables.ToDictionary(x => x.ItemData_Index);
        this.dicStringTable = arrStringDatas.ToDictionary(x => x.ItemString_Index);
        this.dicResouseTable = arrResourceDatas.ToDictionary(x => x.ItemImage_Index);
    }
}
public class RiggingItemStaticDataManager
{
    private static RiggingItemStaticDataManager instance;
    public Dictionary<int, RiggingItem_DataTable> dicRiggingItemData;
    public Dictionary<int, RiggingItem_StringTable> dicRiggingStringTable;
    public Dictionary<int, RiggingItem_ImageResourceTable> dicRiggingResouseTable;
    public static RiggingItemStaticDataManager GetInstance()
    {
        if(RiggingItemStaticDataManager.instance == null)
            RiggingItemStaticDataManager.instance = new RiggingItemStaticDataManager();
        return RiggingItemStaticDataManager.instance;
    }
    public void LoadRiggingItemDatas()
    {
        var RiggingItem_DataTable = Resources.Load<TextAsset>("UI/Json/Pexplorer_RiggingItem_DataTable").text;
        var RiggingItem_StringTable = Resources.Load<TextAsset>("UI/Json/Pexplorer_RiggingItem_StringTable").text;
        var RiggingItem_ImageResourceTable = Resources.Load<TextAsset>("UI/Json/Pexplorer_RiggingItem_ImageResourceTable").text;
        
        var arrRiggingItem_DataTables = JsonConvert.DeserializeObject<RiggingItem_DataTable[]>(RiggingItem_DataTable);
        var arrRiggingStringDatas = JsonConvert.DeserializeObject<RiggingItem_StringTable[]>(RiggingItem_StringTable);
        var arrRiggingResourceDatas = JsonConvert.DeserializeObject<RiggingItem_ImageResourceTable[]>(RiggingItem_ImageResourceTable);
        /* foreach(var data in arrStringDatas)
        {
            Debug.LogFormat("{0}, {1}, {2} ",data.index, data.String_Type, data.String_Desc);
        } */
        this.dicRiggingItemData = arrRiggingItem_DataTables.ToDictionary(x => x.RiggingItemData_Index);
        this.dicRiggingStringTable = arrRiggingStringDatas.ToDictionary(x => x.RiggingItemString_Index);
        this.dicRiggingResouseTable = arrRiggingResourceDatas.ToDictionary(x => x.RiggingItemImage_Index);
    }
}
