using Newtonsoft.Json;
using System.IO;
using UnityEngine;

public class HitBoxFactory
{
    public HitBox CreateHitBox(int index)
    {
        GameObject hitBoxObject = null;

        switch(index / 10000)
        {
            //MeleeHitBox
            case 1:
                {
                    //MeleeHitBoxStruct data = datamanager.gethitboxstruct(index);
                    MeleeHitBoxStruct data = new MeleeHitBoxStruct();

                    string s = File.ReadAllText("Assets/Prefab/JongHyun/HitBox/Pexploer_Melee_Hit_Box.json");
                    foreach (MeleeHitBoxStruct temp in JsonConvert.DeserializeObject<MeleeHitBoxStruct[]>(s))
                    {
                        if (temp.Index == index)
                        {
                            data = temp;
                            break;
                        }
                    }

                    hitBoxObject = new GameObject(data.Index.ToString());

                    MeleeHitBox melee = hitBoxObject.AddComponent<MeleeHitBox>();

                    //GameObject EffectPrefab = Resources.Load<GameObject>(data.Effect_Index);
                    //EffectPrefab.SetParent(melee, false);
                    //melee.hitEffectPrefab = Resources.Load<GameObject>(data.Hit_Effect_Prefab_Index);
                    //melee.destroyEffectPrefab = Resources.Load<GameObject>(data.Destroy_Effect_Prefab_Index);

                    /*
                    for(int i = 0;i < data.Affect_Index.Length ; ++i)
                    {
                        AffectFactory.CreateAffect(data.Affect_Index[i]).transform.SetParent(melee, false);
                    }
                    */

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
            case 2:
                {

                }
                break;
            case 3:
                {

                }
                break;
        }

        return hitBoxObject.GetComponent<HitBox>();
    }
}
