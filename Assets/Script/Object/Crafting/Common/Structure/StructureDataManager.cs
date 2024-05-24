using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StructureDataManager
{
    private static StructureDataManager instance;

    public Dictionary<int, CraftBuildingComponentTable> dicCBComponentTable;
    public Dictionary<int, CraftBuildingAbilityTable> dicCBAbilityTable;
    public Dictionary<int, CraftBuildImageTable> dicCBImgTable;

    public static StructureDataManager GetInstance()
    {
        if(StructureDataManager.instance == null)
        {
            StructureDataManager.instance = new StructureDataManager();
        }
        return StructureDataManager.instance;
    }

    public void LoadCraftInfo()
    {
        var Pexplorer_CBComponentTable = Resources.Load<TextAsset>("Component/Pexplorer_Component_DataTable").text;
        var Pexplorer_CBAbilityTable = Resources.Load<TextAsset>("Component/Building/Pexplorer_Building_Ability").text;
        var Pexplorer_CBCImgTable = Resources.Load<TextAsset>("Component/Pexplorer_Component_ImageResourceTable").text;

        var arrCBComponentDatas = JsonConvert.DeserializeObject<CraftBuildingComponentTable[]>(Pexplorer_CBComponentTable);
        var arrCBAbilityDatas = JsonConvert.DeserializeObject<CraftBuildingAbilityTable[]>(Pexplorer_CBAbilityTable);
        var arrCBImgDatas = JsonConvert.DeserializeObject<CraftBuildImageTable[]>(Pexplorer_CBCImgTable);

        this.dicCBComponentTable = arrCBComponentDatas.ToDictionary(x => x.ComponentDataTable_Index);
        this.dicCBAbilityTable = arrCBAbilityDatas.ToDictionary(x => x.BuildingAbility_Index);
        this.dicCBImgTable = arrCBImgDatas.ToDictionary(x => x.ComponentImage_Index);
    }
}
