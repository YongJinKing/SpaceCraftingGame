using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPopup : MonoBehaviour
{
    public GameObject SlotGridLine;
    public void PopupUpdate()
    {
        for(int i = 0; i < Inventory.instance.InventoryDatas.Count; i++)
        {
            if(Inventory.instance.DisplayInven.Count > SlotGridLine.transform.GetChild(i).GetSiblingIndex())
            {
                SlotGridLine.transform.GetChild(i).GetComponent<InvenItemSlot>().Display();
                SlotGridLine.transform.GetChild(i).GetChild(0).gameObject.SetActive(true);
            }
            else
            {
                SlotGridLine.transform.GetChild(i).GetComponent<InvenItemSlot>().init();
                SlotGridLine.transform.GetChild(i).GetChild(0).gameObject.SetActive(false);
            }
        }
    }
    public void SlotHL(int index, bool onCheck)
    {
        SlotGridLine.transform.GetChild(index).GetChild(2).gameObject.SetActive(onCheck);
    }
    
}

