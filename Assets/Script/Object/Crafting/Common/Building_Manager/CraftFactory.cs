using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.U2D.Aseprite;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;
using static UnityEngine.GraphicsBuffer;

public class CraftFactory : Singleton<CraftFactory>
{
    StructureDataManager structureDataManger; //= StructureDataManager.GetInstance();
    CraftBuildingComponentTable componentData = default;
    CraftBuildingAbilityTable abilityData = default;
    CraftBuildImageTable imgData = default;

    public Transform constructionSite;
    SpaceShip spaceShip;
    void Awake()
    {
        structureDataManger = StructureDataManager.GetInstance();
        structureDataManger.LoadCraftInfo();
    }

    void OnDestroy()
    {
        structureDataManger = null;
    }

    private void Start()
    {
        spaceShip = FindObjectOfType<SpaceShip>();
    }

    public GameObject ReadyToCraftBuilding(int index, Vector3 pos, float Hp = 0f, int size = 0)
    {
        if (!spaceShip.IsDronReady()) return null;

        if (!CheckInventory(index)) // 인벤토리에 써야하는 그 재료들이 다 있는지 확인하고 있으면 저기서 소모하고 없으면 여기로 와서 null 리턴
        {
            return null;
        }

        Transform obj = Instantiate(constructionSite, pos, Quaternion.identity);
        obj.SetParent(null);
        // 건축에 필요한 설정들을 해줘야함
        obj.GetComponent<ConstructionSite>().SetIndex(index);
        obj.GetComponent<ConstructionSite>().SetHp(Hp);
        obj.GetComponent<ConstructionSite>().SetInPos(pos);
        obj.GetComponent<ConstructionSite>().SetSize(size);

        //obj.GetComponent<ConstructionSite>().StartBuilding(); // 건축 시작 << 이걸 드론이 해야됨

        // 드론을 건축현장으로 보내는 함수를 여기서 실행한다!, 이때 드론을 건축 현장으로 보내는 함수는 인자로 obj를 타겟으로 받는다 
        spaceShip.TakeOffDron(obj);

        return obj.gameObject;
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
            case 13:
                return CraftBarricade(index, pos, Hp, size);
            case 50:
                return CraftResources(index, pos, Hp, size);
            default:
                return null;
        }

    }
    bool CheckInventory(int index)
    {
        if (structureDataManger.dicCBAbilityTable.ContainsKey(index))
        {
            abilityData = structureDataManger.dicCBAbilityTable[index];
        }

        int chkCount1 = 0;
        int chkCount2 = 0;

        int consume_Index1 = abilityData.Consume_IndexArr[0]; 
        int consume_Index2 = abilityData.Consume_IndexArr[1];

        //int consume_Count1 = abilityData.Consume_CountArr[0];
        //int consume_Count2 = abilityData.Consume_CountArr[1];

        int consume_Count1 = 0;
        int consume_Count2 = 0;

        // InventoryDatas 리스트를 순회하면서 각 id의 개수를 셉니다.
        /*foreach (var item in Inventory.instance.InventoryDatas)
        {
            if (item.id == consume_Index1)
            {
                chkCount1 += item.Amount;
            }
            else if (item.id == consume_Index2)
            {
                chkCount2 += item.Amount;
            }
        }

        // 두 개의 id가 주어진 개수 이상 존재하는지 확인합니다.
        if (chkCount1 >= consume_Count1 && chkCount2 >= consume_Count2)
        {
            Inventory.instance.UseItem(consume_Index1, consume_Count1);
            Inventory.instance.UseItem(consume_Index2, consume_Count2);
            return true;
        }
*/
        //return false; // 지금 인벤이랑 연결이 안돼서 막아놨는데 나중에 다 주석 해제하고 저 위에 count가 0인것도 지워야함

        return true;
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
        /*factoryBuilding.mComponentName = componentData.Component_Name.ToString();
        factoryBuilding.consumeIndex1 = abilityData.Consume_IndexArr[0]; // 건물 건설 시 소모되는 자원1 인덱스
        factoryBuilding.consumeCount1 = abilityData.Consume_CountArr[0]; // 건물 건설 시 소모되는 자원1 갯수
        factoryBuilding.consumeIndex2 = abilityData.Consume_IndexArr[1]; // 건물에서 생산되는 자원2 인덱스
        factoryBuilding.consumeCount2 = abilityData.Consume_CountArr[1]; // 건물에서 생산 되는 자원2 갯수*/
        factoryBuilding.produceCount = abilityData.BuildingDetail_Value;
        factoryBuilding.DestroyEvent = new UnityEngine.Events.UnityEvent<Vector3>();
        //if (Hp != 0) factoryBuilding.MaxHP = componentData.Component_Hp; // 건물의 체력



        // 체력 관련은 수정 필요
        if (Hp == 0) factoryBuilding.MaxHP = componentData.Component_Hp; // 건물의 체력
        else factoryBuilding.MaxHP = Hp; // 건물의 체력

        factoryBuilding[EStat.Efficiency] = abilityData.BuildingDetail_Delay; // 건물의 생산 속도
        //factoryBuilding.maxAmount = abilityData.BuildingDetail_Value; // 건물이 보관할 수 있는 자원의 최대량

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

    GameObject CraftBarricade(int index, Vector3 pos, float Hp = 0f, int size = 1)
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

        GameObject tmp = Resources.Load($"Component/Image/{imgData.ImageResource_Name}") as GameObject;
        GameObject obj = PrefabUtility.InstantiatePrefab(tmp) as GameObject;
        size = abilityData.BuildingScale;
        //Collider, Rigidbody, Scale Setting
        obj.transform.localScale = Vector3.one;
        obj.name = "Barricade";
        obj.layer = 16;

        Barricade barricade = obj.GetComponent<Barricade>();

        if (Hp == 0) barricade.MaxHP = componentData.Component_Hp; // 건물의 체력
        else barricade.MaxHP = Hp; // 건물의 체력

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
