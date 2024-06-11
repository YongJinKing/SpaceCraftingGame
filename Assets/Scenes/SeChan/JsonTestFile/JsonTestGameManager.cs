using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using UnityEngine.UI;
using Unity.VisualScripting;

[System.Serializable]
public class Item
{
    public Item(string _Type, string _Name, string _Explain, string _Number, bool _isUsing)
    {
        Type = _Type; Name = _Name; Explain = _Explain; Number = _Number;isUsing = _isUsing;
    }

    public string Type, Name, Explain, Number;
    public bool isUsing;
}


public class JsonTestGameManager : MonoBehaviour
{

    public TextAsset ItemDataBase;
    public List<Item> AllItemList, MyItemList, CurItemList;
    public string curType = "Character";
    public GameObject[] Slot, UsingImage;
    public Image[] TabImage, ItemImage;
    public Sprite TabIdleSprite, TabSelectSprite;
    public Sprite[] ItemSprite;
    public GameObject ExplainPannel;
    public RectTransform[] SlotPos;
    public RectTransform CanvasRect;
    IEnumerator PointerCouroutine;
    public Vector2 v;

    void Start()
    {
        string[] line = ItemDataBase.text.Substring(0, ItemDataBase.text.Length - 1).Split('\n');
        print(line.Length);
        for(int i = 0; i < line.Length; i++)
        {
            string[] row = line[i].Split('\t');

            AllItemList.Add(new Item(row[0], row[1], row[2], row[3], row[4] == "TRUE"));
        }

        // PlayerPrefs

        //Save();

        Load();
        //print(ItemDataBase.text);
    }

    private void Update()
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(CanvasRect, Input.mousePosition, Camera.main, out Vector2 anchoredPos);
        ExplainPannel.GetComponent<RectTransform>().anchoredPosition = anchoredPos + v;
    }


    public void SlotClick(int slotNum)
    {
        Item CurItem = CurItemList[slotNum];
        Item UsingItem = CurItemList.Find(x => x.isUsing == true);

        if(curType == "Character")
        {
            //무조건 하나만 선택
            if(UsingItem != null) UsingItem.isUsing = false;
            CurItem.isUsing = true;
        }
        else
        {
            //선택 해제가능
            CurItem.isUsing = !CurItem.isUsing;
            if (UsingItem != null) UsingItem.isUsing = false;   
        }
        Save();
     //   print(CurItemList[slotNum].Name);
    }

    public void TabClick(string tabName)
    {
        curType = tabName;
        // 특정 탭을 누르면 해당 필드의 탭이 바뀜
        CurItemList = MyItemList.FindAll(x => x.Type == tabName);
        
        // 슬롯과 텍스트 보이기
        for ( int i = 0; i < Slot.Length; i++)
        {
            bool isExist = i < CurItemList.Count;
            Slot[i].SetActive(i < CurItemList.Count);
            //텍스트 이름 가져오기
            Slot[i].GetComponentInChildren<Text>().text = i < CurItemList.Count ? CurItemList[i].Name  : "";

            if (isExist)
            {
                ItemImage[i].sprite = ItemSprite[AllItemList.FindIndex(x => x.Name == CurItemList[i].Name)];
                UsingImage[i].SetActive(CurItemList[i].isUsing);
            }
        }

        int tabNum = 0;
        //탭 이미지 변경
        switch(tabName)
        {
            case "Character": tabNum = 0; break;
            case "Balloon": tabNum = 1; break;
        }
        for (int i = 0; i < TabImage.Length; i++)
        {
            TabImage[i].sprite = i == tabNum ? TabSelectSprite : TabIdleSprite;
        }


    }

    IEnumerator PointerEnterDelay(int slotNum)
    {
        yield return new WaitForSeconds(0.5f);
        ExplainPannel.SetActive(true);
    }

    public void PointerEnter(int slotNum)
    {
        print(slotNum + "슬롯 들어옴");
        PointerCouroutine = PointerEnterDelay(slotNum);
        StartCoroutine(PointerCouroutine);

        ExplainPannel.GetComponentInChildren<Text>().text = CurItemList[slotNum].Name;
        ExplainPannel.transform.GetChild(1).GetComponentInChildren<Image>().sprite = Slot[slotNum].transform.GetChild(1).GetComponent<Image>().sprite;
        //ExplainPannel.GetComponentInChildren<Text>().text = CurItemList[slotNum].Name;

    }

    public void PointerExit(int slotNum)
    {
        print(slotNum + "슬롯 나감");
        StopCoroutine(PointerCouroutine);
        ExplainPannel.SetActive(false);
    }


    void Save()
    {
        string jdata = JsonConvert.SerializeObject(MyItemList);
        File.WriteAllText(Application.dataPath + "/Scenes/Sechan/JsonTestFile/MyItemText", jdata);

        TabClick(curType);
    }

    // Update is called once per frame

    void Load()
    {
        {
            string jdata = File.ReadAllText(Application.dataPath + "/Scenes/Sechan/JsonTestFile/MyItemText");
            MyItemList = JsonConvert.DeserializeObject<List<Item>>(jdata);

            TabClick(curType);
        }
    }
}
