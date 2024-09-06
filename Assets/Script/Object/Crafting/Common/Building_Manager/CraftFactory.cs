using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;


public class CraftFactory : Singleton<CraftFactory>
{
    StructureDataManager structureDataManger; //= StructureDataManager.GetInstance();
    CraftBuildingComponentTable componentData = default;
    CraftBuildingAbilityTable abilityData = default;
    CraftBuildImageTable imgData = default;

    public Transform constructionSite;
    public UnityEvent ActiveNoMoreResources;
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

        if (!CheckInventory(index)) // 인벤토리에 해당 index에 해당하는 건물의 재료들이 있는지 확인하고 있으면 거기서 차감까지 해버림
        {
            ActiveNoMoreResources?.Invoke();
            return null;
        }
        FindComponentsByIndex(index);

        Transform obj = Instantiate(constructionSite, pos, Quaternion.identity);
        obj.SetParent(null);
        obj.GetComponent<ConstructionSite>().SetIndex(index);
        obj.GetComponent<ConstructionSite>().SetHp(Hp);
        obj.GetComponent<ConstructionSite>().SetInPos(pos);
        obj.GetComponent<ConstructionSite>().SetSize(size);
        obj.GetComponent<ConstructionSite>().SetCraftingTime(abilityData.BuildingSpeed);

        // 우주선에서 드론을 보내고 그 드론이 건물을 짓는다
        spaceShip.TakeOffDron(obj);

        return obj.gameObject;
    }

    public GameObject CraftBuilding(int index, Vector3 pos , float Hp = 0f, int size = 0, int producedAmount = 0)
    {
        int idx = index / 10000;
        Debug.Log("Craft : " + idx);
        switch(idx)
        {
            case 10:
                return CraftMiner(index, pos, Hp, size, producedAmount);
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

        if (Inventory.instance.GetItemCheck(consume_Index1, consume_Count1) && Inventory.instance.GetItemCheck(consume_Index2, consume_Count2))
        {
            Inventory.instance.UseItem(consume_Index1, consume_Count1);
            Inventory.instance.UseItem(consume_Index2, consume_Count2);
            return true;
        }
        else return false; // 없으면 false 리턴

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

        //GameObject obj = Resources.Load<GameObject>($"Component/Image/{imgData.ImageResource_Name}");
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

    GameObject CraftMiner(int index, Vector3 pos, float Hp = 0f, int size = 1, int producedAmount = 0)
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
        factoryBuilding.produceResourceIndex = abilityData.BuildingDetail_GeneratedItem;
        factoryBuilding.DestroyEvent = new UnityEngine.Events.UnityEvent<Vector3>();
        factoryBuilding.produceAmount = producedAmount;

        if (Hp == 0) factoryBuilding.MaxHP = componentData.Component_Hp; 
        else factoryBuilding.MaxHP = Hp; 

        factoryBuilding[EStat.Efficiency] = abilityData.BuildingDetail_Delay; 
        
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
        //GameObject obj = Resources.Load<GameObject>($"Component/Image/{imgData.ImageResource_Name}");

        //NaturalResources naturalResources = obj.GetComponent<NaturalResources>();
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
        if (Hp == 0) barricade.MaxHP = componentData.Component_Hp; 
        else barricade.MaxHP = Hp; 

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
