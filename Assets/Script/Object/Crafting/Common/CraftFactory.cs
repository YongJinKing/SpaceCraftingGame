using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
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

    public GameObject CraftBuilding(int index, Vector3 pos , float Hp = 0f, int size = 0)
    {
        /*GameObject obj = new GameObject();
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
        turret.mComponentName = componentData.Component_Name.ToString();
        //turret[EStat.MaxHP] = 100;
        turret[EStat.ATK] = abilityData.BuildingDetail_Value;
        turret[EStat.ATKDelay] = abilityData.BuildingDetail_Delay;
        turret[EStat.ATKSpeed] = 0;



        GameObject head = new GameObject();
        head.name = "head";
        head.transform.SetParent(obj.transform);
        head.transform.position = new Vector3(0, 0.5f, 0);
        turret.header = head.transform;

        GameObject attackPoint = new GameObject();
        attackPoint.name = "attackPoint";
        attackPoint.transform.SetParent(head.transform);
        attackPoint.transform.localPosition = new Vector3(0, 0, 0);
        turret.attackPoint = attackPoint.transform;

        GameObject body = new GameObject();
        body.name = "body";
        body.transform.SetParent(obj.transform);
        body.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        SpriteRenderer renderer = body.AddComponent<SpriteRenderer>();
        renderer.sprite = Resources.Load<Sprite>($"Component/Image/{imgData.ImageResource_Name}");


        GameObject bullet = new GameObject();
        bullet.name = "bullet";
        bullet.AddComponent<SpriteRenderer>();
        TestBullet testBullet = bullet.AddComponent<TestBullet>();
        testBullet.layerMask = 1 << 19;
        BoxCollider2D boxCollider2D = bullet.AddComponent<BoxCollider2D>();
        boxCollider2D.size = new Vector2(1f, 0.5f);

        turret.bullet = bullet.transform;
        for (int i = 0; i < 5; i++)
        {
            GameObject bullet = new GameObject();
            bullet.name = "bullet" + i.ToString();
            bullet.transform.SetParent(bulletPool.transform);
            bullet.AddComponent<SpriteRenderer>();
            TestBullet testBullet = bullet.AddComponent<TestBullet>();
            testBullet.layerMask = 1 << 19;

            BoxCollider2D boxCollider2D = bullet.AddComponent<BoxCollider2D>();
            boxCollider2D.size = new Vector2(1f, 0.5f);
            turret.bulletList.Add(bullet);

            bullet.gameObject.SetActive(false);
        }


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
        perception.transform.SetParent(obj.transform);*/
        int idx = index / 10000;
        Debug.Log("Craft : " + idx);
        switch(idx)
        {
            case 10:
                return CraftMiner(index, pos, Hp, size);
            case 11:
                return CraftTurret(index, pos,Hp, size);
            default:
                return null;
        }

    }

    GameObject CraftTurret(int index, Vector3 pos, float Hp = 0f, int size = 0)
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
        if (Hp == 0f) turret.MaxHP = componentData.Component_Hp;
        else turret.MaxHP = Hp;
        turret.mComponentName = componentData.Component_Name.ToString();
        //turret[EStat.MaxHP] = 100;
        turret[EStat.ATK] = abilityData.BuildingDetail_Value;
        turret[EStat.ATKDelay] = abilityData.BuildingDetail_Delay;
        turret[EStat.ATKSpeed] = 0;



        GameObject head = new GameObject();
        head.name = "head";
        head.transform.SetParent(obj.transform);
        head.transform.localPosition = new Vector3(0, 0.5f, 0);
        turret.header = head.transform;

        GameObject attackPoint = new GameObject();
        attackPoint.name = "attackPoint";
        attackPoint.transform.SetParent(head.transform);
        attackPoint.transform.localPosition = new Vector3(0, 0, 0);
        turret.attackPoint = attackPoint.transform;

        GameObject body = new GameObject();
        body.name = "body";
        body.transform.SetParent(obj.transform);
        body.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        body.transform.localPosition = new Vector3(0, 0, 0);
        SpriteRenderer renderer = body.AddComponent<SpriteRenderer>();
        renderer.sprite = Resources.Load<Sprite>($"Component/Image/{imgData.ImageResource_Name}");


        GameObject bullet = new GameObject();
        bullet.name = "bullet";
        bullet.AddComponent<SpriteRenderer>();
        TestBullet testBullet = bullet.AddComponent<TestBullet>();
        testBullet.layerMask = 1 << 19;
        BoxCollider2D boxCollider2D = bullet.AddComponent<BoxCollider2D>();
        boxCollider2D.size = new Vector2(1f, 0.5f);

        turret.bullet = bullet.transform;
        
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
        perception.transform.localPosition = new Vector3(0, 0, 0);

        obj.transform.localPosition = pos;
        obj.transform.localScale = Vector3.one * size;
        return obj;
    }

    GameObject CraftMiner(int index, Vector3 pos, float Hp = 0f, int size = 1)
    {
        return null;
    }
}
