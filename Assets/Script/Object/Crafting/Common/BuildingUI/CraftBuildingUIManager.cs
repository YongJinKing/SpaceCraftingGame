
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;



public class CraftBuildingUIManager : MonoBehaviour
{
    
  
    public string curType = "Resource";
    //public List<Building> AllItemList, MyItemList,CurItemList;
   
    public Image[] TabImage;
    public Sprite TabSelectSprite, TabDeselectSprite;
   
    public static CraftBuildingUIManager instance;

    public UnityEvent UpdateBuildPopup;

    BuildingUISelectType BuildingType;
 
    public List<int> TypeID;
    public List<int> ApplyTypeID;

    //public Image[] ExplainIMG;

    // Start is called before the first frame update

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if (instance == this)
        {
            Destroy(instance.gameObject);
        }
    }
    void Start()
    {        
        BuildingUIStructure.GetInstance().LoadBuildingInfo();    
        BuildingType = BuildingUISelectType.Resource;
        foreach (var data in BuildingUIStructure.GetInstance().dicBUIComponentTable)
        {
            if (data.Value.ComponentDataTable_Index < 200000)
            { 
                //Debug.Log(data.Value.ComponentDataTable_Index);
                //json Index값을 가져와 새로운 리스트에 삽입
                TypeID.Add(data.Value.ComponentDataTable_Index);
            }
           
        }

        BuildingTabFunction(BuildingType);
        
    }

    // Update is called once per frame
    void Update()
    {


    }


    // 탭 버튼 변경
    public void TabClick(string tabName)
    {
        curType = tabName;

        int tabNum = 0;
        //탭 이미지 변경
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
 
    public void StartMode()
    {
        ChangeMode(0);
    }
    
    public void ChangeMode(int index)
    {
        BuildingType = (BuildingUISelectType)index;
        BuildingTabFunction(BuildingType);
        UpdateBuildPopup?.Invoke();
    }

    public void BuildingTabFunction(BuildingUISelectType Type)
    {
        /*
         * 리스트 하나 더 만들기
         * 1. 리스트 초기화

         * */
        ApplyTypeID = new List<int>();
       
        if (Type == BuildingUISelectType.Resource)
        {
            Debug.Log(Type);
            Debug.Log(TypeID.Count);
            for (int i = 0; i < TypeID.Count; i++)
            {
                var BuildingData = BuildingUIStructure.GetInstance().dicBUIComponentTable[TypeID[i]];
                if (((BuildingData.ComponentDataTable_Index / 10000) - 10) == (int)Type || ((BuildingData.ComponentDataTable_Index / 10000) -13) == (int)Type)
                {
                    //Debug.Log(i);
                    ApplyTypeID.Add(BuildingData.ComponentDataTable_Index);

                }
            }
            
            
        }
        else
        {
            Debug.Log(Type);
            // 10만 : 자원생산건물   11만 : 공성건물    12만 : 아이템 생산건물(?)     13만: 울타리 건물
            for (int i = 0; i < TypeID.Count; i++)
            {
                var BuildingData = BuildingUIStructure.GetInstance().dicBUIComponentTable[TypeID[i]];
                
                if (((BuildingData.ComponentDataTable_Index / 10000) - 10) == (int)Type)
                {
                    //Debug.Log(i);
                    ApplyTypeID.Add(BuildingData.ComponentDataTable_Index);
                    
                }

            }
            
        }

    }

    public void BuildingUIExplanation()
    {
        Debug.Log("시작");
        
        Debug.Log(ApplyTypeID);
    }

}
