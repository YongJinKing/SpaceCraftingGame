using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class BuildingUISlot : MonoBehaviour 
{
    public Image image;
    // public TMP_Text AmountTxt;
    public Image selectedImage;
    public Text selectedName;
    public Text selectedDesc;
    public Text selectedDescAmount1;
    public Text selectedDescAmount2;

    string ItemName;
    string ItemDesc;
    string spName;
    int buildIndex;
    int buildAmount1;
    int buildAmount2;
    Sprite sp;
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
        var BuildingInfoData = BuildingUIStructure.GetInstance().dicBUIInfoTable[CraftBuildingUIManager.instance.ApplyTypeID[transform.GetSiblingIndex()]];
        var BuildingAbilityData = BuildingUIStructure.GetInstance().dicBUIAbilityTable[CraftBuildingUIManager.instance.ApplyTypeID[transform.GetSiblingIndex()]];
        var BuildingSpriteData = BuildingUIStructure.GetInstance().dicBUIImgTable[BuildingData.Component_Image];
        buildAmount1 = BuildingAbilityData.Consume_CountArr[0];
        buildAmount2 = BuildingAbilityData.Consume_CountArr[1];
        buildIndex = BuildingInfoData.BuildingInformation_Index;
        ItemName = BuildingInfoData.BuildingInformation_Name;
        ItemDesc = BuildingInfoData.BuildingInformation_Text;

        //AmountTxt.text = "x" + UnitCalculate.GetInstance().Calculate(CraftBuildingUIManager.instance.TypeID[transform.GetSiblingIndex()]);
        spName = BuildingSpriteData.ImageResource_Name;
        sp = Resources.Load<Sprite>($"Component/Image/Sprite/{spName}");
        image.sprite = sp;
        if (sp == null)
        {
            image.color = Color.black;
        }
        else
        {
            image.color = Color.white;
        }
    }

    public int GetBuildingIndex()
    {
        return this.buildIndex;
    }

    public void ChangeDescImage()
    {
        selectedImage.sprite = sp;
        selectedName.text = ItemName;
        selectedDesc.text = ItemDesc;
        selectedDescAmount1.text = "x" + buildAmount1.ToString();
        selectedDescAmount2.text = "x" + buildAmount2.ToString();
        CraftBuildingUIManager.instance.buildIndex = buildIndex;
    }

}
