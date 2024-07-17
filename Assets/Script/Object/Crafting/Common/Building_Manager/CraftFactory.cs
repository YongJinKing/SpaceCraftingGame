using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.U2D.Aseprite;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CraftFactory : Singleton<CraftFactory>
{
    StructureDataManager structureDataManger; //= StructureDataManager.GetInstance();
    CraftBuildingComponentTable componentData = default;
    CraftBuildingAbilityTable abilityData = default;
    CraftBuildImageTable imgData = default;

    void Awake()
    {
        structureDataManger = StructureDataManager.GetInstance();
        structureDataManger.LoadCraftInfo();
    }

    void OnDestroy()
    {
        structureDataManger = null;
    }

    public List<int> CheckResourcesCount(int index) // 해당 index에 맞는 건물을 지을 때 필요한 재료들(0번, 1번)과 그 갯수(2번,3번)을 담은 리스트를 리턴, 받는 쪽에서 이걸 가지고 인벤과 연결해서 비교
    {
        List<int> resourcesList = new List<int>();

        
        if (structureDataManger.dicCBAbilityTable.ContainsKey(index))
        {
            abilityData = structureDataManger.dicCBAbilityTable[index];
        }

        resourcesList.Add(abilityData.Consume_IndexArr[0]);
        resourcesList.Add(abilityData.Consume_IndexArr[1]);
        resourcesList.Add(abilityData.Consume_CountArr[0]);
        resourcesList.Add(abilityData.Consume_CountArr[1]);

        return resourcesList;
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
            case 50:
                return CraftResources(index, pos, Hp, size);
            default:
                return null;
        }

    }

    GameObject CraftTurret(int index, Vector3 pos, float Hp = 0f, int size = 0)
    {
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

        
        //GameObject obj = new GameObject();
        GameObject tmp = Resources.Load($"Component/Image/{imgData.ImageResource_Name}") as GameObject;
        GameObject obj = PrefabUtility.InstantiatePrefab(tmp) as GameObject;
        size = abilityData.BuildingScale;
        //Collider, Rigidbody, Scale Setting
        obj.transform.localScale = Vector3.one;
        obj.name = "Turret";
        obj.layer = 16;
        /*
                BoxCollider2D mainBoxCollider2D = obj.AddComponent<BoxCollider2D>();
                mainBoxCollider2D.size = new Vector2(1f * size, 1f* size);
                mainBoxCollider2D.offset = new Vector2(0.5f * (size - 1), 0.5f * (size - 1));*/

        //Turret turret = obj.AddComponent<Turret>();
        Turret turret = obj.GetComponent<Turret>();
        if (Hp == 0f) turret.MaxHP = componentData.Component_Hp;
        else turret.MaxHP = Hp;
        turret.mComponentName = componentData.Component_Name.ToString();
        //turret[EStat.MaxHP] = 100;
        turret[EStat.ATK] = abilityData.BuildingDetail_Value;
        turret[EStat.ATKDelay] = abilityData.BuildingDetail_Delay;
        turret[EStat.ATKSpeed] = 0;
        turret.DestroyEvent = new UnityEngine.Events.UnityEvent<Vector3>();
        
        /*GameObject head = new GameObject();
        head.name = "head";
        head.transform.SetParent(obj.transform);
        head.transform.localPosition = new Vector3(0, 0.5f, 0);
        turret.header = head.transform;

        GameObject attackPoint = new GameObject();
        attackPoint.name = "attackPoint";
        attackPoint.transform.SetParent(head.transform);
        attackPoint.transform.localPosition = new Vector3(0, 0, 0);
        turret.attackPoint = attackPoint.transform;*/

        // 향후 이미지의 크기에 따라 아래 컴포넌트 수치들은 변경될 수 있음
        /*GameObject image = new GameObject();
        image.name = "image";
        image.transform.SetParent(obj.transform);
        image.transform.localScale = new Vector3(0.5f * size, 0.5f * size, 0.5f * size);
        image.transform.localPosition = new Vector3(0.5f * (size - 1), 0.5f * (size-1), 0.5f * (size - 1));
        SpriteRenderer renderer = image.AddComponent<SpriteRenderer>();
        renderer.sprite = Resources.Load<Sprite>($"Component/Image/{imgData.ImageResource_Name}");*/

        /*GameObject bullet = new GameObject();
        bullet.name = "bullet";
        bullet.AddComponent<SpriteRenderer>();
        TestBullet testBullet = bullet.AddComponent<TestBullet>();
        testBullet.layerMask = 1 << 19;
        BoxCollider2D boxCollider2D = bullet.AddComponent<BoxCollider2D>();
        boxCollider2D.size = new Vector2(1f, 0.5f);

        turret.bullet = bullet.transform;*/
        
        /*GameObject perception = new GameObject();
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
        perception.transform.localPosition = new Vector3(0, 0, 0);*/

        obj.transform.localPosition = pos;
        obj.transform.localScale = Vector3.one;
        
        return obj;
    }

    GameObject CraftMiner(int index, Vector3 pos, float Hp = 0f, int size = 1)
    {
        Debug.Log("생산 건물 건설 시작");
        

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
        GameObject tmp = Resources.Load($"Component/Image/{imgData.ImageResource_Name}") as GameObject;
        GameObject obj = PrefabUtility.InstantiatePrefab(tmp) as GameObject;

        /*obj.transform.localScale = Vector3.one;
        obj.layer = 16;
        obj.name = index.ToString();
        obj.AddComponent<BoxCollider2D>();*/

        FactoryBuilding factoryBuilding =  obj.GetComponent<FactoryBuilding>();
        factoryBuilding.mComponentName = componentData.Component_Name.ToString();
        factoryBuilding.consumeIndex = abilityData.Consume_IndexArr[0]; // 건물 건설 시 소모되는 자원 인덱스
        factoryBuilding.consumeCount = abilityData.Consume_CountArr[0]; // 건물 건설 시 소모되는 자원량
        factoryBuilding.produceIndex = abilityData.Consume_IndexArr[1]; // 건물에서 생산되는 자원 인덱스
        factoryBuilding.produceCount = abilityData.Consume_CountArr[1]; // 건물에서 생산 되는 자원량
        factoryBuilding.DestroyEvent = new UnityEngine.Events.UnityEvent<Vector3>();
        //if (Hp != 0) factoryBuilding.MaxHP = componentData.Component_Hp; // 건물의 체력



        // 체력 관련은 수정 필요
        if (Hp == 0) factoryBuilding.MaxHP = componentData.Component_Hp; // 건물의 체력
        else factoryBuilding.MaxHP = Hp; // 건물의 체력

        factoryBuilding[EStat.Efficiency] = abilityData.BuildingDetail_Delay; // 건물의 생산 속도
        factoryBuilding.maxAmount = abilityData.BuildingDetail_Value; // 건물이 보관할 수 있는 자원의 최대량

        /*GameObject objImg = new GameObject();
        objImg.name = "Image";
        objImg.transform.SetParent(obj.transform);
        objImg.transform.localScale = Vector3.one * 0.25f;

        SpriteRenderer renderer = objImg.AddComponent<SpriteRenderer>();
        renderer.sprite = Resources.Load<Sprite>($"Component/Image/{imgData.ImageResource_Name}");*/
        

        obj.transform.localPosition = pos;
        obj.transform.localScale = Vector3.one * size;
        
        Debug.Log("생산 건물 건설 끝");
        Debug.Log(obj);
        return obj;
    }

    GameObject CraftResources(int index, Vector3 pos, float Hp = 0f, int size = 1)
    {
        if (structureDataManger.dicCBImgTable.ContainsKey(index))
        {
            imgData = structureDataManger.dicCBImgTable[index];
        }

        GameObject tmp = Resources.Load($"Component/Image/{imgData.ImageResource_Name}") as GameObject;
        GameObject obj = PrefabUtility.InstantiatePrefab(tmp) as GameObject;

        obj.transform.localPosition = pos;
        obj.transform.localScale = Vector3.one * size;
        return obj;
    }

    public int GetBuildingSize(int index)
    {
        if (structureDataManger.dicCBAbilityTable.ContainsKey(index))
        {
            abilityData = structureDataManger.dicCBAbilityTable[index];
        }

        return abilityData.BuildingScale;
    }
}
