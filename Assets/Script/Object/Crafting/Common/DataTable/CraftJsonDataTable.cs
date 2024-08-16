public struct CraftBuildingComponentTable
{
    public int ComponentDataTable_Index;
    public int Component_Type;
    public int Component_Name;
    public int Component_Desc;
    public int Component_Image;
    public int Component_Hp;
    public int Component_Detail;
}

public struct CraftBuildingAbilityTable
{
    public int BuildingAbility_Index;
    public int BuildingDetailType;
    public int[] Consume_IndexArr;
    public int[] Consume_CountArr;
    public int BuildingSpeed;
    public int BuildingScale;
    public int BuildingDetail_Value;
    public int BuildingDetail_Delay;
    public int BuildingDetail_Range;
    public int BuildingDetail_GeneratedItem;
}

public struct CraftBuildImageTable
{
    public int ComponentImage_Index;
    public string ImageResource_Name;
}