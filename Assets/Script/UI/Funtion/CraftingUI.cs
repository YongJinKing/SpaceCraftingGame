using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.UI;



    public class CraftingUI
    {
        BuildingUIStructure BuildingUIManger = BuildingUIStructure.GetInstance();
        CraftBuildingComponentTable componentData = default;
        CraftBuildingAbilityTable abilityData = default;
        CraftBuildImageTable imgData = default;

        public CraftingUI()
        {
            BuildingUIManger.LoadBuildingInfo();
        }
        ~CraftingUI()
        {
            BuildingUIManger = null;
        }


        public GameObject CraftBuildUI(int index, float Hp = 0f, int size = 0)
        {
            int idx = index / 10000;
            Debug.Log("Craft : " + idx);
            switch (idx)
            {
                case 10:
                    return BuildUIMiner(index, Hp, size);
                case 11:
                    return BuildUITurret(index, Hp, size);
                default:
                    return null;
            }
        }

        GameObject BuildUIMiner(int index, float Hp = 0f, int size = 1)
        {
            Debug.Log("생산 건물 탭");
            GameObject obj = new GameObject();

            if (BuildingUIManger.dicBUIComponentTable.ContainsKey(index))
            {
                componentData = BuildingUIManger.dicBUIComponentTable[index];
            }
            if (BuildingUIManger.dicBUIAbilityTable.ContainsKey(index))
            {
                abilityData = BuildingUIManger.dicBUIAbilityTable[index];
            }
            if (BuildingUIManger.dicBUIImgTable.ContainsKey(index))
            {
                imgData = BuildingUIManger.dicBUIImgTable[index];
            }
            obj.transform.localScale = Vector3.one;
            obj.layer = 16;
            obj.name = index.ToString();
            obj.AddComponent<BoxCollider2D>();

            FactoryBuilding factoryBuilding = obj.AddComponent<FactoryBuilding>();
            factoryBuilding.mComponentName = componentData.Component_Name.ToString();
            factoryBuilding.consumeIndex1 = abilityData.Consume_IndexArr[0]; // 건물 건설 시 소모되는 자원 인덱스
            factoryBuilding.consumeCount1 = abilityData.Consume_CountArr; // 건물 건설 시 소모되는 자원량
            factoryBuilding.consumeIndex2 = abilityData.Consume_IndexArr[1]; // 건물에서 생산되는 자원 인덱스
            factoryBuilding.consumeCount2 = abilityData.Consume_CountArr; // 건물에서 생산 되는 자원량
            return obj;
        }

        GameObject BuildUITurret(int index, float Hp = 0f, int size = 0)
        {
            Debug.Log("터렛 건물 탭");
            GameObject obj = new GameObject();

            if (BuildingUIManger.dicBUIComponentTable.ContainsKey(index))
            {
                componentData = BuildingUIManger.dicBUIComponentTable[index];
            }
            if (BuildingUIManger.dicBUIAbilityTable.ContainsKey(index))
            {
                abilityData = BuildingUIManger.dicBUIAbilityTable[index];
            }
            if (BuildingUIManger.dicBUIImgTable.ContainsKey(index))
            {
                imgData = BuildingUIManger.dicBUIImgTable[index];
            }

            obj.transform.localScale = Vector3.one;
            obj.name = "Turret";
            obj.layer = 16;

            obj.AddComponent<BoxCollider2D>();
            Turret turret = obj.AddComponent<Turret>();
            if (Hp == 0f) turret.MaxHP = componentData.Component_Hp;
            else turret.MaxHP = Hp;
            turret.mComponentName = componentData.Component_Name.ToString();
            //turret[EStat.MaxHP] = 100;
            turret[EStat.ATK] = abilityData.BuildingDetail_Value;
            turret[EStat.ATKDelay] = abilityData.BuildingDetail_Delay;
            turret[EStat.ATKSpeed] = 0;
            turret.DestroyEvent = new UnityEngine.Events.UnityEvent<Vector3>();
            return obj;
        }

    }


