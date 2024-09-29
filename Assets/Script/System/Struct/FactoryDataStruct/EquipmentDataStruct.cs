using System;

public struct WeaponDataStruct
{
    [Serializable]
    public struct StatModifyValue
    {
        public EStat stattype;              //스텟 타입
        public ValueModifierType modiType;  //증가 방식 (0이면 +, 1이면 *)
        public int sortOrder;               //정렬 방식(0에 가까울수록 먼저 계산됨)
        public float value;
    }

    public int Index;
    public int Anim_Type;  //나중에 Enum으로 바꿔도 될것같다., -1일경우 변경 없음으로

    public StatModifyValue[] Stat_Modify_Values;    //증가하는 스텟 종류와 증가 방식과 값
        
    public int Main_Action_Index;   //ActionFactory 경유
    public int Second_Action_Index; //ActionFactory 경유
    public int Weapon_Prefab_Index; //모델과 bulletStartPos, 일단 모든 공격은 BulletStartPos에서만 하는걸로
}
