public struct WeaponDataStruct
{
    public int Index;
    public int Item_Type;    //나중에 Enum으로 바꿔도 될것같다.
    public int Weapon_Type;  //나중에 Enum으로 바꿔도 될것같다.

    public int Main_Action_Index;   //ActionFactory 경유
    public int Second_Action_Index; //ActionFactory 경유
    public int Weapon_Prefab_Index; //모델과 bulletStartPos
    //public int 
}
