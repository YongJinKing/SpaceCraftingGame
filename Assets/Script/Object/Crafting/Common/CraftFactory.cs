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
        Turret turret = obj.AddComponent<Turret>();
        turret.MaxHP = componentData.Component_Hp;
        //turret[EStat.MaxHP] = 100;
        turret[EStat.ATK] = abilityData.BuildingDetail_Value;
        turret[EStat.ATKDelay] = abilityData.BuildingDetail_Delay;
        turret[EStat.ATKSpeed] = 0;

        

        GameObject head = new GameObject();
        head.name = "head";
        head.transform.SetParent(obj.transform);
        head.transform.position = new Vector3(0, 0.5f, 0);

        GameObject attackPoint = new GameObject();
        attackPoint.name = "attackPoint";
        attackPoint.transform.SetParent(head.transform);

        GameObject body = new GameObject();
        body.name = "body";
        body.transform.SetParent(obj.transform);
        body.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        SpriteRenderer renderer = body.AddComponent<SpriteRenderer>();
        renderer.sprite = Resources.Load<Sprite>($"Component/Image/{imgData.ImageResource_Name}");


        GameObject perception = new GameObject();
        perception.name = "TurretPerception";
        TurretPerception turretPerception = perception.AddComponent<TurretPerception>();
        CircleCollider2D circleCollider = perception.AddComponent<CircleCollider2D>();
        circleCollider.isTrigger = true;
        circleCollider.radius = 5f;
        turretPerception.enemyMask = 1 << 19;
        turretPerception.detectEnemyEvents = new UnityEngine.Events.UnityEvent<GameObject>();
        turretPerception.lostEnemyEvents = new UnityEngine.Events.UnityEvent<GameObject>();
        turretPerception.destroyBulletEvents = new UnityEngine.Events.UnityEvent();
        perception.transform.SetParent(obj.transform);


        return obj;
    }
}
