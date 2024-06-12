[System.Serializable]
public struct Resource_JsonData 
{
    public int GameResources_Index; // 자원 인덱스
    public int GameResources_Type; // 자원 타입
    public int[] GameResources_SponItem; // 자원 아이템
    public int[] GameResources_ItemCount; // 자원 아이템 수량
    public int[] GameResources_HasRercentage; // 확률 여부
    public float GameResources_PercentageMinimum; // 최소 확률
}
[System.Serializable]
public struct PlusResource_JsonData
{
    public int GameResources_Index; 
    public int GameResources_Type; 
    public int[] GameResources_SponItem;
    public int[] GameResources_ItemCount; 
    public int[] GameResources_HasRercentage; 
    public float GameResources_PercentageMinimum; 
}


