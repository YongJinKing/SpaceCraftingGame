using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.Linq;

public class ItemStaticDataManager
{
    private static ItemStaticDataManager instance;
    public Dictionary<int, Item_DataTable> dicItemData;
    public Dictionary<int, Item_StringTable> dicStringTable;
    public Dictionary<int, Item_IamgeResourceTable> dicResouseTable;
    
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
        var arrResourceDatas = JsonConvert.DeserializeObject<Item_IamgeResourceTable[]>(Item_IamgeResourceTable);
        /* foreach(var data in arrStringDatas)
        {
            Debug.LogFormat("{0}, {1}, {2} ",data.index, data.String_Type, data.String_Desc);
        } */
        this.dicItemData = arrItem_DataTables.ToDictionary(x => x.ItemData_Index);
        this.dicStringTable = arrStringDatas.ToDictionary(x => x.ItemString_Index);
        this.dicResouseTable = arrResourceDatas.ToDictionary(x => x.ItemImage_Index);
    }
}
