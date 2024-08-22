
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
                //json Index���� ������ ���ο� ����Ʈ�� ����
                TypeID.Add(data.Value.ComponentDataTable_Index);
            }
           
        }

        BuildingTabFunction(BuildingType);
        
    }

    // Update is called once per frame
    void Update()
    {


    }


    // �� ��ư ����
    public void TabClick(string tabName)
    {
        curType = tabName;

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
         * ����Ʈ �ϳ� �� �����
         * 1. ����Ʈ �ʱ�ȭ

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
            // 10�� : �ڿ�����ǹ�   11�� : �����ǹ�    12�� : ������ ����ǹ�(?)     13��: ��Ÿ�� �ǹ�
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
        Debug.Log("����");
        
        Debug.Log(ApplyTypeID);
    }

}
