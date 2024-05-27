using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPopup : MonoBehaviour
{
    public GameObject SlotGridLine;
    public GameObject OptionPanel;
    public int FrontPageNum;
    int BackPageNum;
    private void Start() 
    {
        FrontPageNum = 1;
        BackPageNum = 1;
        PopupUpdate(0);
    }
    public void PopupUpdate(int index)//0 : 그대로 진행 1 : FrontPageNum1로 초기화
    {
        if(index == 1)
            FrontPageNum = 1;
        PageUpdate();
        for(int i = 0; i < SlotGridLine.transform.childCount; i++)
        {
            if(Inventory.instance.DisplayInven.Count > SlotGridLine.transform.GetChild(i).GetSiblingIndex() + ((FrontPageNum - 1) * 25))
            {
                SlotGridLine.transform.GetChild(i).GetComponent<InvenItemSlot>().Display(FrontPageNum - 1);
                SlotGridLine.transform.GetChild(i).GetChild(0).gameObject.SetActive(true);
            }
            else
            {
                SlotGridLine.transform.GetChild(i).GetComponent<InvenItemSlot>().init();
                SlotGridLine.transform.GetChild(i).GetChild(0).gameObject.SetActive(false);
            }
        }
        
    }


    public void PagePtn(int index)
    {
        if(index == 0)
        {
            if(FrontPageNum > 1)
            {
                FrontPageNum--;
            }
        }
        if(index == 1)
        {
            if(BackPageNum > FrontPageNum)
            {
                FrontPageNum++;
            }
        }
        PopupUpdate(0);
    }
    void PageUpdate()
    {
        if(Inventory.instance.DisplayInven.Count > 25)
        {
            if(Inventory.instance.DisplayInven.Count % 25 > 0)
                BackPageNum = (Inventory.instance.DisplayInven.Count / 25) + 1;
        }
        else
        {
            BackPageNum = 1;
        }
        OptionPanel.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = FrontPageNum.ToString() + "/" + BackPageNum.ToString();
    }
    public void SlotHL(int index, bool onCheck)
    {
        SlotGridLine.transform.GetChild(index).GetChild(2).gameObject.SetActive(onCheck);
    }
    
}

