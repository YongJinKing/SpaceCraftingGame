using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingUISlot : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        BuildingUIStructure.GetInstance().LoadBuildingInfo();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InputSlotData(int index)
    {
        var BuildingData = BuildingUIStructure.GetInstance().dicBUIComponentTable[CraftBuildingUIManager.instance.BuildingID[transform.GetSiblingIndex()]];
        
    }
}
