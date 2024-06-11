using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuildingUIStructure
{
    private static BuildingUIStructure instance;

    public Dictionary<int, CraftBuildingComponentTable> dicBUIComponentTable;
    public Dictionary<int, CraftBuildingAbilityTable> dicBUIAbilityTable;
    public Dictionary<int, CraftBuildImageTable> dicBUIImgTable;

    public static BuildingUIStructure GetInstance()
    {
        if (BuildingUIStructure.instance == null)
        {
            BuildingUIStructure.instance = new BuildingUIStructure();
        }
        return BuildingUIStructure.instance;
    }

    public void LoadBuildingInfo()
    {
        var Pexplorer_CBComponentTable = Resources.Load<TextAsset>("Component/Pexplorer_Component_DataTable").text;
        var Pexplorer_CBAbilityTable = Resources.Load<TextAsset>("Component/Building/Pexplorer_Building_Ability").text;
        var Pexplorer_CBCImgTable = Resources.Load<TextAsset>("Component/Pexplorer_Component_ImageResourceTable").text;

        var arrCBComponentDatas = JsonConvert.DeserializeObject<CraftBuildingComponentTable[]>(Pexplorer_CBComponentTable);
        var arrCBAbilityDatas = JsonConvert.DeserializeObject<CraftBuildingAbilityTable[]>(Pexplorer_CBAbilityTable);
        var arrCBImgDatas = JsonConvert.DeserializeObject<CraftBuildImageTable[]>(Pexplorer_CBCImgTable);

        this.dicBUIComponentTable = arrCBComponentDatas.ToDictionary(x => x.ComponentDataTable_Index);
        this.dicBUIAbilityTable = arrCBAbilityDatas.ToDictionary(x => x.BuildingAbility_Index);
        this.dicBUIImgTable = arrCBImgDatas.ToDictionary(x => x.ComponentImage_Index);
    }
}
