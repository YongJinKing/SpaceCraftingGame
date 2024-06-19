using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuildingUISlot : MonoBehaviour
{
    public Image image;
   // public TMP_Text AmountTxt;

    string ItemName;
    string ItemDesc;
    string spName;
    // Start is called before the first frame update
    void Start()
    {
        BuildingUIStructure.GetInstance().LoadBuildingInfo();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InputSlotData()
    {
        var BuildingData = BuildingUIStructure.GetInstance().dicBUIComponentTable[CraftBuildingUIManager.instance.ApplyTypeID[transform.GetSiblingIndex()]];
        var BuildingSpriteData = BuildingUIStructure.GetInstance().dicBUIImgTable[BuildingData.Component_Image];
        //AmountTxt.text = "x" + UnitCalculate.GetInstance().Calculate(CraftBuildingUIManager.instance.TypeID[transform.GetSiblingIndex()]);
        spName = BuildingSpriteData.ImageResource_Name;
        Sprite sp = Resources.Load<Sprite>($"Component/Image/{spName}");
        image.sprite = sp;
        

    }

}
