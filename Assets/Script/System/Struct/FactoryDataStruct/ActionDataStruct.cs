using UnityEngine;

public struct WeaponActionStruct
{
    public int Index;
    public float Priority;
    public float CoolTime;
    public float ActiveRadius;

    public int[] HitBox_Index;
    public int[] LayerMask;
    
    public float ActiveDuration;
}
