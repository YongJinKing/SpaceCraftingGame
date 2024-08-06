using UnityEngine;
public struct MeleeHitBoxStruct
{
    public int Index;
    public int Effect_Index;
    public int Hit_Effect_Prefab_Index;
    public int Destroy_Effect_Prefab_Index;
    public int[] Affect_Index;
    public float HitBox_X_Size;
    public float HitBox_Y_Size;
    public int[] LayerMask;
    public float HitBox_Duration;
    public float Hit_Frequency;
    public bool IsFollowDir;
}

public struct PointHitBoxStruct
{
    public int Index;
    public int Effect_Index;
    public int Hit_Effect_Prefab_Index;
    public int Destroy_Effect_Prefab_Index;
    public int[] Affect_Index;
    public float HitBox_X_Size;
    public float HitBox_Y_Size;
    public int[] LayerMask;
    public float HitBox_Duration;
    public float Hit_Frequency;
}

public struct ProjectileHitBoxStruct
{
    public int Index;
    public int Effect_Index;
    public int Hit_Effect_Prefab_Index;
    public int Destroy_Effect_Prefab_Index;
    public int[] Affect_Index;
    public float HitBox_X_Size;
    public float HitBox_Y_Size;
    public int[] LayerMask;
    public float HitBox_Duration;
    public float Hit_Frequency;
    public float Move_Speed;
    public float Max_Dist;
    public bool Penetrable;
}
