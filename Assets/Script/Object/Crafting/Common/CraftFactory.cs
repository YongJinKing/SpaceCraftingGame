using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftFactory
{
    StructureDataManager structureDataManger = StructureDataManager.GetInstance();

    public CraftFactory()
    {
        structureDataManger.LoadCraftInfo();
    }

    ~CraftFactory()
    {
        structureDataManger = null;
    }

    public GameObject CraftBuilding(int index)
    {
        GameObject obj = new GameObject();
        CraftBuildingComponentTable componentData = default;
        CraftBuildingAbilityTable abilityData = default;
        CraftBuildImageTable imgData = default;

        if (structureDataManger.dicCBComponentTable.ContainsKey(index))
        {
            componentData = structureDataManger.dicCBComponentTable[index];
        }
        if (structureDataManger.dicCBAbilityTable.ContainsKey(index))
        {
            abilityData = structureDataManger.dicCBAbilityTable[index];
        }
        if (structureDataManger.dicCBImgTable.ContainsKey(index))
        {
            imgData = structureDataManger.dicCBImgTable[index];
        }

        //Collider, Rigidbody, Scale Setting
        obj.transform.localScale = Vector3.one;
        obj.name = "Turret";
        obj.AddComponent<BoxCollider2D>();
        obj.AddComponent<Turret>();


        GameObject head = new GameObject();
        head.transform.SetParent(obj.transform);

        GameObject body = new GameObject();
        body.transform.SetParent(obj.transform);

        return obj;
    }
}
