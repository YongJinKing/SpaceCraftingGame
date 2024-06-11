using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CraftBuildingUI
{
    StructureDataManager BUIStructureDataManager = StructureDataManager.GetInstance();
    CraftBuildingComponentTable BUIcomponentData = default;
    CraftBuildingAbilityTable BUIabilityData = default;
    CraftBuildImageTable BUIimgData = default;

    public CraftBuildingUI()
    {
        BUIStructureDataManager.LoadCraftInfo();
    }
    ~CraftBuildingUI()
    {
        BUIStructureDataManager = null;
    }

}
public class CraftingUI : MonoBehaviour
{

}
