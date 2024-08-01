using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


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

    void FindComponentsByIndex(int index)
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
    }
    public GameObject ReadyToCraftBuilding(int index, Vector3 pos, float Hp = 0f, int size = 0)
    {
        if (!spaceShip.IsDronReady()) return null;

        if (!CheckInventory(index)) // 인벤토리에 써야하는 그 재료들이 다 있는지 확인하고 있으면 저기서 소모하고 없으면 여기로 와서 null 리턴
        {
            return null;
        }
        FindComponentsByIndex(index);

        Transform obj = Instantiate(constructionSite, pos, Quaternion.identity);
        obj.SetParent(null);
        // 건축에 필요한 설정들을 해줘야함
        obj.GetComponent<ConstructionSite>().SetIndex(index);
        obj.GetComponent<ConstructionSite>().SetHp(Hp);
        obj.GetComponent<ConstructionSite>().SetInPos(pos);
        obj.GetComponent<ConstructionSite>().SetSize(size);
        obj.GetComponent<ConstructionSite>().SetCraftingTime(abilityData.BuildingSpeed);

        // 드론을 건축현장으로 보내는 함수를 여기서 실행한다!, 이때 드론을 건축 현장으로 보내는 함수는 인자로 obj를 타겟으로 받는다 
        spaceShip.TakeOffDron(obj);

        return obj.gameObject;
    }

    public GameObject CraftBuilding(int index, Vector3 pos , float Hp = 0f, int size = 0)
    {
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

        int consume_Index1 = abilityData.Consume_IndexArr[0]; 
        int consume_Index2 = abilityData.Consume_IndexArr[1];

        int consume_Count1 = abilityData.Consume_CountArr[0];
        int consume_Count2 = abilityData.Consume_CountArr[1];

        int chkCount1 = 0;
        int chkCount2 = 0;

        // InventoryDatas 리스트를 순회하면서 각 id의 개수를 셉니다.
        foreach (var item in Inventory.instance.InventoryDatas)
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

        return false; // 지금 인벤이랑 연결이 안돼서 막아놨는데 나중에 다 주석 해제하고 저 위에 count가 0인것도 지워야함

        //return true;
    }
    GameObject CraftTurret(int index, Vector3 pos, float Hp = 0f, int size = 0)
    {
        /*if (structureDataManger.dicCBComponentTable.ContainsKey(index))
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
        }*/
        FindComponentsByIndex(index);

        GameObject tmp = Resources.Load($"Component/Image/{imgData.ImageResource_Name}") as GameObject;
        GameObject obj = PrefabUtility.InstantiatePrefab(tmp) as GameObject;
        size = abilityData.BuildingScale;
        //Collider, Rigidbody, Scale Setting
        obj.transform.localScale = Vector3.one;
        obj.name = "Turret";
        obj.layer = 16;
        
        Turret turret = obj.GetComponent<Turret>();
        if (Hp == 0f) turret.MaxHP = componentData.Component_Hp;
        else turret.MaxHP = Hp;
        turret.mComponentName = componentData.Component_Name.ToString();
        turret[EStat.ATK] = abilityData.BuildingDetail_Value;
        turret[EStat.ATKDelay] = abilityData.BuildingDetail_Delay;
        turret[EStat.ATKSpeed] = 0;
        turret.DestroyEvent = new UnityEngine.Events.UnityEvent<Vector3>();
        
        obj.transform.localPosition = pos;
        obj.transform.localScale = Vector3.one;
        
        return obj;
    }

    GameObject CraftMiner(int index, Vector3 pos, float Hp = 0f, int size = 1)
    {
        /*if (structureDataManger.dicCBComponentTable.ContainsKey(index))
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
        }*/

        FindComponentsByIndex(index);

        GameObject tmp = Resources.Load($"Component/Image/{imgData.ImageResource_Name}") as GameObject;
        GameObject obj = PrefabUtility.InstantiatePrefab(tmp) as GameObject;


        FactoryBuilding factoryBuilding =  obj.GetComponent<FactoryBuilding>();
        
        factoryBuilding.mComponentName = componentData.Component_Name.ToString();
        factoryBuilding.produceCount = abilityData.BuildingDetail_Value;
        factoryBuilding.DestroyEvent = new UnityEngine.Events.UnityEvent<Vector3>();
        
        // 체력 관련은 수정 필요
        if (Hp == 0) factoryBuilding.MaxHP = componentData.Component_Hp; // 건물의 체력
        else factoryBuilding.MaxHP = Hp; // 건물의 체력

        factoryBuilding[EStat.Efficiency] = abilityData.BuildingDetail_Delay; // 건물의 생산 속도
        
        obj.transform.localPosition = pos;
        obj.transform.localScale = Vector3.one * size;
        
        return obj;
    }

    GameObject CraftResources(int index, Vector3 pos, float Hp = 0f, int size = 1)
    {
        /*if (structureDataManger.dicCBImgTable.ContainsKey(index))
        {
            imgData = structureDataManger.dicCBImgTable[index];
        }*/

        FindComponentsByIndex(index);

        GameObject tmp = Resources.Load($"Component/Image/{imgData.ImageResource_Name}") as GameObject;
        GameObject obj = PrefabUtility.InstantiatePrefab(tmp) as GameObject;

        obj.transform.localPosition = pos;
        obj.transform.localScale = Vector3.one * size;
        return obj;
    }

    GameObject CraftBarricade(int index, Vector3 pos, float Hp = 0f, int size = 1)
    {
        /*if (structureDataManger.dicCBComponentTable.ContainsKey(index))
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
        }*/

        FindComponentsByIndex(index);

        GameObject tmp = Resources.Load($"Component/Image/{imgData.ImageResource_Name}") as GameObject;
        GameObject obj = PrefabUtility.InstantiatePrefab(tmp) as GameObject;
        size = abilityData.BuildingScale;
        //Collider, Rigidbody, Scale Setting
        obj.transform.localScale = Vector3.one;
        obj.name = "Barricade";
        obj.layer = 16;

        Barricade barricade = obj.GetComponent<Barricade>();
        barricade.mComponentName = componentData.Component_Name.ToString();
        if (Hp == 0) barricade.MaxHP = componentData.Component_Hp; // 건물의 체력
        else barricade.MaxHP = Hp; // 건물의 체력

        obj.transform.localPosition = pos;
        obj.transform.localScale = Vector3.one * size;

        return obj;
    }

    public int GetBuildingSize(int index)
    {
        /*if (structureDataManger.dicCBAbilityTable.ContainsKey(index))
        {
            abilityData = structureDataManger.dicCBAbilityTable[index];
        }*/
        FindComponentsByIndex(index);

        return abilityData.BuildingScale;
    }
}
