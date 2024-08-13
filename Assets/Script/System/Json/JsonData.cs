
#region Item
public struct Item_DataTable
{
    public int ItemData_Index;
    public int ItemType;
    public int ItemName;
    public int ItemDesc;
    public int ItemRarity;
    public int ItemSponPosition;
    public int ItemImage;
}
public struct Item_StringTable
{
    public int ItemString_Index;
    public int String_Type;
    public string String_Desc;
}
public struct Item_ImageResourceTable
{
    public int ItemImage_Index;
    public string ImageResource_Name;
}
#endregion
#region RiggingItem
public struct RiggingItem_DataTable
{
    public int RiggingItemData_Index;
    public int RiggingItemType;
    public int RiggingItemName;
    public int RiggingItemDesc;
    public int RiggingItemLevel;
    public int[] Consume_IndexArr;
    public int[] Consume_CountArr;
    public int RiggingItem_AttackPower;
    public float RiggingItem_AttackSpeed;
    public float RiggingItem_Range;
    public int RiggingItem_BulletPrefab;
    public int RiggingItemImage;
}
public struct RiggingItem_StringTable
{   
    public int RiggingItemString_Index;
    public string String_Desc;
}
public struct RiggingItem_ImageResourceTable
{
    public int RiggingItemImage_Index;
    public string ImageResource_Name;
}
#endregion