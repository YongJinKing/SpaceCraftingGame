public struct CraftBuildingComponentTable
{
    public int ComponentDataTable_Index;
    public int Component_Type;
    public int Component_Name;
    public int Component_Description;
    public int Component_ImgIdx;
    public int Component_Hp;
    public int Component_Detail;
}

public struct CraftBuildingAbilityTable
{
    public int ComponentDataTable_Index;
    public int[] Consume_Index;
    public int[] Consume_Count;
    public int BuildingSpeed;
    public int BuildingScale;
    public int BuidlingDetale_Value;
    public int BuidlingDetale_Delay;
    public int BuildingDetail_Range;
}

public struct CraftBuildImageTable
{
    public int ComponentDataTable_Index;
    public string ImageResource_Name;
}