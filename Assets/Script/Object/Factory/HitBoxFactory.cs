using Newtonsoft.Json;
using System.IO;
using UnityEngine;

public class HitBoxFactory
{
    private AffectFactory affectFac; 
    private PrefabLoader prefabLoader;

    public HitBoxFactory() 
    { 
        affectFac = new AffectFactory();
        prefabLoader = new PrefabLoader();
    }
    public HitBoxFactory(AffectFactory affect)
    {
        affectFac = affect;
        prefabLoader = new PrefabLoader();
    }

    public HitBoxFactory(AffectFactory affectFac, PrefabLoader prefabLoader)
    {
        this.affectFac = affectFac;
        this.prefabLoader = prefabLoader;
    }

    public GameObject Create(int index)
    {
        GameObject gameObject = null;

        switch(index / 10000)
        {
            //MeleeHitBox
            case 1:
                {
                    MeleeHitBoxStruct data = default;

                    string s = File.ReadAllText("Assets/Prefab/JongHyun/HitBox/Pexplorer_Melee_Hit_Box.json");
                    foreach (MeleeHitBoxStruct temp in JsonConvert.DeserializeObject<MeleeHitBoxStruct[]>(s))
                    {
                        if (temp.Index == index)
                        {
                            data = temp;
                            break;
                        }
                    }
                    if (data.Equals(default))
                    {
                        break;
                    }

                    gameObject = new GameObject(index.ToString());

                    MeleeHitBox melee = gameObject.AddComponent<MeleeHitBox>();

                    if (data.Effect_Index > 0)
                    {
                        GameObject EffectPrefab = GameObject.Instantiate(prefabLoader.Load(data.Effect_Index));
                        EffectPrefab.transform.SetParent(melee.transform, false);
                    }

                    if (data.Hit_Effect_Prefab_Index > 0)
                    {
                        melee.hitEffectPrefab = prefabLoader.Load(data.Hit_Effect_Prefab_Index);
                    }

                    if (data.Destroy_Effect_Prefab_Index > 0)
                    {
                        melee.destroyEffectPrefab = prefabLoader.Load(data.Destroy_Effect_Prefab_Index);
                    }


                    for (int i = 0;i < data.Affect_Index.Length ; ++i)
                    {
                        GameObject temp = affectFac.Create(data.Affect_Index[i]);
                        if(temp != null)
                            temp.transform.SetParent(melee.transform, false);
                    }
                    

                    melee.hitBoxSize = new Vector2(data.HitBox_X_Size, data.HitBox_Y_Size);

                    for (int i = 0; i < data.LayerMask.Length; ++i)
                    {
                        melee.targetMask = melee.targetMask | (1 << data.LayerMask[i]);
                    }

                    melee.duration = data.HitBox_Duration;
                    melee.hitFrequency = data.Hit_Frequency;
                    melee.isFollowDir = data.IsFollowDir;
                }   
                break;
            //ProjectileHitBox
            case 2:
                {
                    ProjectileHitBoxStruct data = default;

                    string s = File.ReadAllText("Assets/Prefab/JongHyun/HitBox/Pexplorer_Projectile_Hit_Box.json");
                    foreach (ProjectileHitBoxStruct temp in JsonConvert.DeserializeObject<ProjectileHitBoxStruct[]>(s))
                    {
                        if (temp.Index == index)
                        {
                            data = temp;
                            break;
                        }
                    }
                    if (data.Equals(default))
                    {
                        break;
                    }

                    gameObject = new GameObject(index.ToString());

                    ProjectileHitBox projectile = gameObject.AddComponent<ProjectileHitBox>();

                    if(data.Effect_Index > 0)
                    {
                        GameObject EffectPrefab = GameObject.Instantiate(prefabLoader.Load(data.Effect_Index));
                        EffectPrefab.transform.SetParent(projectile.transform, false);
                    }

                    if (data.Hit_Effect_Prefab_Index > 0) 
                    {
                        projectile.hitEffectPrefab = prefabLoader.Load(data.Hit_Effect_Prefab_Index);
                    }

                    if(data.Destroy_Effect_Prefab_Index > 0)
                    {
                        projectile.destroyEffectPrefab = prefabLoader.Load(data.Destroy_Effect_Prefab_Index);
                    }

                    for (int i = 0; i < data.Affect_Index.Length; ++i)
                    {
                        GameObject temp = affectFac.Create(data.Affect_Index[i]);
                        if (temp != null)
                            temp.transform.SetParent(projectile.transform, false);
                    }

                    projectile.hitBoxSize = new Vector2(data.HitBox_X_Size, data.HitBox_Y_Size);
                    for (int i = 0; i < data.LayerMask.Length; ++i)
                    {
                        projectile.targetMask = projectile.targetMask | (1 << data.LayerMask[i]);
                    }

                    projectile.duration = data.HitBox_Duration;
                    projectile.hitFrequency = data.Hit_Frequency;
                    projectile.moveSpeed = data.Move_Speed;
                    projectile.maxDist = data.Max_Dist;
                    projectile.penetrable = data.Penetrable;
                }
                break;
            case 3:
                {
                    ShotGunHitBoxStruct data = default;

                    string s = File.ReadAllText("Assets/Prefab/JongHyun/HitBox/Pexplorer_ShotGun_Hit_Box.json");
                    foreach (ShotGunHitBoxStruct temp in JsonConvert.DeserializeObject<ShotGunHitBoxStruct[]>(s))
                    {
                        if (temp.Index == index)
                        {
                            data = temp;
                            break;
                        }
                    }
                    if (data.Equals(default))
                    {
                        break;
                    }

                    gameObject = new GameObject(index.ToString());

                    ShotGunHitBox shotGun = gameObject.AddComponent<ShotGunHitBox>();

                    if (data.Effect_Index > 0)
                    {
                        GameObject EffectPrefab = GameObject.Instantiate(prefabLoader.Load(data.Effect_Index));
                        EffectPrefab.transform.SetParent(shotGun.transform, false);
                    }

                    if (data.Hit_Effect_Prefab_Index > 0)
                    {
                        shotGun.hitEffectPrefab = prefabLoader.Load(data.Hit_Effect_Prefab_Index);
                    }

                    if (data.Destroy_Effect_Prefab_Index > 0)
                    {
                        shotGun.destroyEffectPrefab = prefabLoader.Load(data.Destroy_Effect_Prefab_Index);
                    }


                    for (int i = 0; i < data.Affect_Index.Length; ++i)
                    {
                        GameObject temp = affectFac.Create(data.Affect_Index[i]);
                        if (temp != null)
                            temp.transform.SetParent(shotGun.transform, false);
                    }


                    shotGun.hitBoxSize = new Vector2(data.HitBox_X_Size, data.HitBox_Y_Size);

                    for (int i = 0; i < data.LayerMask.Length; ++i)
                    {
                        shotGun.targetMask = shotGun.targetMask | (1 << data.LayerMask[i]);
                    }

                    shotGun.duration = data.HitBox_Duration;
                    shotGun.hitFrequency = data.Hit_Frequency;
                }
                break;
        }

        return gameObject;
    }
}
