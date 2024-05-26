using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPopup : MonoBehaviour
{
    public GameObject SlotGridLine;
    public void PopupUpdate()
    {
        for(int i = 0; i < Inventory.instance.DisplayInven.Count; i++)
        {
            if(Inventory.instance.DisplayInven[i].id > 0)
            {
                SlotGridLine.transform.GetChild(i).GetComponent<InvenItemSlot>().Display();
            }
            else
            {
                SlotGridLine.transform.GetChild(i).GetChild(0).gameObject.SetActive(false);
            }
        }
        
    }
}
