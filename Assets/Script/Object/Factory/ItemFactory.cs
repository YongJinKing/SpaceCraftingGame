using Newtonsoft.Json;
using System.IO;
using UnityEngine;

public class ItemFactory
{
    private ActionFactory actionFac;
    private PrefabLoader prefabLoader;

    public ItemFactory()
    {
        actionFac = new ActionFactory();
        prefabLoader = new PrefabLoader();
    }
    public ItemFactory(ActionFactory actionFac)
    {
        this.actionFac = actionFac;
        prefabLoader = new PrefabLoader();
    }

    public ItemFactory(ActionFactory actionFac, PrefabLoader prefabLoader)
    {
        this.actionFac = actionFac;
        this.prefabLoader = prefabLoader;
    }

    public GameObject Create(int index)
    {
        GameObject gameObject = null;
        //일단 Equipment만 만든다.

        switch (index / 10000)
        {
            //Weapon
            case 1:
                {
                    WeaponDataStruct data = default;
                    string s = File.ReadAllText("Assets/Prefab/JongHyun/Equipment/Pexplorer_Weapon.json");
                    foreach (WeaponDataStruct temp in JsonConvert.DeserializeObject<WeaponDataStruct[]>(s))
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

                    Weapon weapon = gameObject.AddComponent<Weapon>();

                    weapon.itemType = EEquipmentType.Weapon;
                    weapon.animType = data.Anim_Type;
                    
                    if(data.Stat_Modify_Values.Length > 0)
                    {
                        GameObject statModifier = new GameObject("StatModifier");

                        statModifier.AddComponent<StatModifier>();
                        for (int i = 0; i < data.Stat_Modify_Values.Length; ++i)
                        {
                            GameObject FeatureObject = new GameObject(data.Stat_Modify_Values[i].stattype.ToString());
                            StatModifyFeature Feature = FeatureObject.AddComponent<StatModifyFeature>();

                            Feature.statType = data.Stat_Modify_Values[i].stattype;
                            Feature.modifierType = data.Stat_Modify_Values[i].modiType;
                            Feature.sortOrder = data.Stat_Modify_Values[i].sortOrder;
                            Feature.value = data.Stat_Modify_Values[i].value;

                            FeatureObject.transform.SetParent(statModifier.transform, false);
                        }

                        statModifier.transform.SetParent(gameObject.transform, false);
                    }

                    bool isStartPos = false;
                    if (data.Weapon_Prefab_Index > 0)
                    {
                        weapon.graphic = GameObject.Instantiate(prefabLoader.Load(data.Weapon_Prefab_Index)).transform;
                        
                        weapon.graphic.SetParent(gameObject.transform, false);

                        if(weapon.graphic.childCount > 0)
                        {
                            isStartPos = true;
                        }
                    }

                    if (data.Main_Action_Index > 0)
                    {
                        if (isStartPos)
                        {
                            weapon.mainAction = actionFac.Create(data.Main_Action_Index, weapon.graphic.GetChild(0)).GetComponent<Action>();
                        }
                        else
                        {
                            weapon.mainAction = actionFac.Create(data.Main_Action_Index).GetComponent<Action>();
                        }
                        weapon.mainAction.transform.SetParent(gameObject.transform, false);
                    }
                    if(data.Second_Action_Index > 0)
                    {
                        if (isStartPos)
                        {
                            weapon.subAction = actionFac.Create(data.Second_Action_Index, weapon.graphic.GetChild(0)).GetComponent<Action>();
                        }
                        else
                        {
                            weapon.subAction = actionFac.Create(data.Second_Action_Index).GetComponent<Action>();
                        }
                        weapon.mainAction.transform.SetParent(gameObject.transform, false);
                    }

                }
                break;
            case 2:
                { }
                break;
        }


        return gameObject;
    }
}
