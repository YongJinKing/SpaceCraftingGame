using Newtonsoft.Json;
using System.IO;
using UnityEngine;

public class ActionFactory
{
    private HitBoxFactory hitBoxFac;

    public ActionFactory()
    {
        hitBoxFac = new HitBoxFactory();
    }
    public ActionFactory(HitBoxFactory hitBoxFac)
    {
        this.hitBoxFac = hitBoxFac;
    }

    public GameObject Create(int index, Transform AttackStartPos = null)
    {
        GameObject gameObject = null;

        switch(index / 10000)
        {
            //WeaponAction
            case 1:
                {
                    WeaponActionStruct data = default;
                    string s = File.ReadAllText("Assets/Prefab/JongHyun/Action/Pexplorer_WeaponAction.json");
                    foreach (WeaponActionStruct temp in JsonConvert.DeserializeObject<WeaponActionStruct[]>(s))
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

                    WeaponAttackAction weapon = gameObject.AddComponent<WeaponAttackAction>();

                    weapon.priority = data.Priority;
                    weapon.coolTime = data.CoolTime;
                    weapon.activeRadius = data.ActiveRadius;
                    weapon.activeDuration = data.ActiveDuration;



                    for(int i = 0; i < data.LayerMask.Length; ++i)
                    {
                        weapon.targetMask = weapon.targetMask | (1 << data.LayerMask[i]);
                    }

                    weapon.hitBoxPrefabs = new HitBox[data.HitBox_Index.Length];
                    for (int i = 0; i < data.HitBox_Index.Length; ++i)
                    {
                        GameObject hitbox = hitBoxFac.Create(data.HitBox_Index[i]);
                        hitbox.transform.SetParent(gameObject.transform, false);
                        weapon.hitBoxPrefabs[i] = hitbox.GetComponent<HitBox>();
                        hitbox.SetActive(false);
                    }

                    if (AttackStartPos != null)
                    {
                        weapon.attackStartPos = new Transform[data.HitBox_Index.Length];
                        for(int i = 0; i < data.HitBox_Index.Length; ++i)
                        {
                            weapon.attackStartPos[i] = AttackStartPos;
                        }
                    }
                }
                break;
        }

        return gameObject;
    }
}
