using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.Linq;

public class NaturalResourceDataManager
{
    private static NaturalResourceDataManager instance;
    public Dictionary<int, NaturalResource_DataTable> dicNaturalResource_DataTables;
    public Dictionary<int, NaturalResource_StringTable> dicStringTable;
    public Dictionary<int, NaturalResource_IamgeResourceTable> dicResouseTable;

    private NaturalResourceDataManager()
    {

    }
    
    public static NaturalResourceDataManager GetInstance()
    {
        if(NaturalResourceDataManager.instance == null)
            NaturalResourceDataManager.instance = new NaturalResourceDataManager();
        return NaturalResourceDataManager.instance;
    }
    public void ConditionLoadDatas()
    {
        var Mestiarii_InGame_NaturalResource_DataTableTable = Resources.Load<TextAsset>("UI/BuffAndDeBuff/Json/Mestiarii_InGame_NaturalResource_DataTableTable").text;
        var Mestiarii_InGame_StringTable = Resources.Load<TextAsset>("UI/BuffAndDeBuff/Json/Mestiarii_InGame_StringTable").text;
        var Mestiarii_InGame_ImageResourceTable = Resources.Load<TextAsset>("UI/BuffAndDeBuff/Json/Mestiarii_InGame_ImageResourceTable").text;
        
        var arrNaturalResource_DataTables = JsonConvert.DeserializeObject<NaturalResource_DataTable[]>(Mestiarii_InGame_NaturalResource_DataTableTable);
        var arrStringDatas = JsonConvert.DeserializeObject<NaturalResource_StringTable[]>(Mestiarii_InGame_StringTable);
        var arrResourceDatas = JsonConvert.DeserializeObject<NaturalResource_IamgeResourceTable[]>(Mestiarii_InGame_ImageResourceTable);
        /* foreach(var data in arrStringDatas)
        {
            Debug.LogFormat("{0}, {1}, {2} ",data.index, data.String_Type, data.String_Desc);
        } */
        this.dicNaturalResource_DataTables = arrNaturalResource_DataTables.ToDictionary(x => x.index);
        this.dicStringTable = arrStringDatas.ToDictionary(x => x.index);
        this.dicResouseTable = arrResourceDatas.ToDictionary(x => x.index);
    }
}
