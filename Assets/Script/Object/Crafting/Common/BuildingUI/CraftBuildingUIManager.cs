using Spine;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.Events;
using static Unity.VisualScripting.Dependencies.Sqlite.SQLite3;
using UnityEngine.UI;
using System;
using System.Reflection;
/*
public class Building
{
    public Building(string _Type, string _Name, string _Explain, string _Number, bool _isUsing)
    {
        Type = _Type; Name = _Name; Explain = _Explain; Number = _Number; isUsing = _isUsing;
    }

    public string Type, Name, Explain, Number;
    public bool isUsing;
}*/

public class CraftBuildingUIManager : MonoBehaviour
{
    public GameObject BuildingButton, InventoryButton;
    public int ButtonClick, BuildButtonStatus, InventoryButtonStatus;


    public string curType = "Resource";
    //public List<Building> AllItemList, MyItemList,CurItemList;
    public GameObject[] Slot, UsingImage;
    public Image[] TabImage,ItemImage;
    public Sprite TabSelectSprite, TabDeselectSprite;
    public Sprite[] ItemSprite;

    [SerializeField] int BuildUIIndex;
    CraftingUI BuildSet;
    // Start is called before the first frame update
    void Start()
    {
        ButtonClick = 0;
        BuildButtonStatus = 0;
        InventoryButtonStatus = 0;
        //BuildingButton.SetActive(false);
        //InventoryButton.SetActive(false);
        BuildSet = new CraftingUI();
        BuildUIIndex = 110000;
        Debug.Log("�ܼ�â ����");
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetKey(KeyCode.B))
        {
            if (ButtonClick == 0)
            {
                Debug.Log("�Ǽ� â ����");
                BuildUIOpen();
            }
            if (ButtonClick == 1)
            {
                Debug.Log("�Ǽ� â Ŭ����");
                BuildUIClose();
            }
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (ButtonClick == 0)
            {
                Debug.Log("�Ǽ� â ����");
                InventoryUIOpen();
            }
            if (ButtonClick == 1)
            {
                Debug.Log("�Ǽ� â Ŭ����");
                InventoryUIClose();
            }
        }

        
    }
    /*
    public void SlotClick(int slotNum)
    {
        Building CurItem = CurItemList[slotNum];
        Building UsingItem = CurItemList.Find(x => x.isUsing == true);

        if (curType == "Resource")
        {
            //������ �ϳ��� ����
            if (UsingItem != null) UsingItem.isUsing = false;
            CurItem.isUsing = true;
        }
        else
        {
            //������ �ϳ��� ����
            if (UsingItem != null) UsingItem.isUsing = false;
            CurItem.isUsing = true;
            
            //���� ��������
            CurItem.isUsing = !CurItem.isUsing;
            if (UsingItem != null) UsingItem.isUsing = false;
            
        }
        
        //   print(CurItemList[slotNum].Name);
    }*/

    // �� ��ư ����
    public void TabClick(string tabName)
    {
        curType = tabName;
        //CurItemList = MyItemList.FindAll(x => x.Type == tabName);
        /*
        for (int i = 0; i < Slot.Length; i++)
        {
            bool isExist = i < CurItemList.Count;
            Slot[i].SetActive(i < CurItemList.Count);
            //�ؽ�Ʈ �̸� ��������
            Slot[i].GetComponentInChildren<Text>().text = i < CurItemList.Count ? CurItemList[i].Name : "";

            if (isExist)
            {
                ItemImage[i].sprite = ItemSprite[AllItemList.FindIndex(x => x.Name == CurItemList[i].Name)];
                UsingImage[i].SetActive(CurItemList[i].isUsing);
            }
        }*/


        int tabNum = 0;
        //�� �̹��� ����
        switch (tabName)
        {
            case "Resource": tabNum = 0; break;
            case "Defense": tabNum = 1; break;
        }
        for (int i = 0; i < TabImage.Length; i++)
        {
            TabImage[i].sprite = i == tabNum ? TabSelectSprite : TabDeselectSprite;
        }


    }

    public void ResourceButtonClick()
    {
        

        BuildUIIndex = 100000;
        Debug.Log("���� �Ǽ� ���õ� �ε��� : " + BuildUIIndex);
        BringBuildData(BuildUIIndex); 
       
    }

    public void TurretButtonClick()
    {

        BuildUIIndex = 110000;
        Debug.Log("���� �Ǽ� ���õ� �ε��� : " + BuildUIIndex);
        BringBuildData(BuildUIIndex);

    }

    public void BringBuildData(int index)
    {
        GameObject ResourceBuilding = BuildSet.CraftBuildUI(index);
        Debug.Log(ResourceBuilding);
    }

    public void BuildUIOpen()
    {
        BuildingButton.SetActive(true);
        ButtonClick = 1;
    }
    public void BuildUIClose()
    {
        BuildingButton.SetActive(false);
        ButtonClick = 0;
    }

    public void InventoryUIOpen()
    {
        InventoryButton.SetActive(true);
        ButtonClick = 1;
    }
    public void InventoryUIClose()
    {
        InventoryButton.SetActive(false);
        ButtonClick = 0;
    }
}
