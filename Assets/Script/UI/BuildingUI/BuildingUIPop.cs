using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildingUIPop : MonoBehaviour
{
    public GameObject objSlotGridLine;
    public GameObject objSlotPopup;
    Coroutine SlotPopup;


    // Start is called before the first frame update
    void Start()
    {
        PopupBuildUpadate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PopupBuildUpadate()
    {
    
        //Debug.Log("팝업" + objSlotGridLine.transform.childCount);
        //Debug.Log("ApplyID 카운트 " +CraftBuildingUIManager.instance.ApplyTypeID.Count);
        //CraftBuildingUIManager.instance.ApplyTypeID.Count
        for (int i = 0; i < objSlotGridLine.transform.childCount; i++)
        {
         

            if (CraftBuildingUIManager.instance.ApplyTypeID.Count > objSlotGridLine.transform.GetChild(i).GetSiblingIndex())
            {
                objSlotGridLine.transform.GetChild(i).GetComponent<BuildingUISlot>().InputSlotData();
                objSlotGridLine.transform.GetChild(i).GetChild(0).gameObject.SetActive(true);
            }
            else
            {
                objSlotGridLine.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }

    public void SlotHL(int index, bool onCheck)
    {
        //getchild() 안에 숫자는 자식의 위치
        objSlotGridLine.transform.GetChild(index).GetChild(2).gameObject.SetActive(onCheck);
        if (onCheck)
        {
            SlotPopup = StartCoroutine(CorBuildSlotPopup(index));
        }
        else
        {
            if(SlotPopup != null)
            {
                StopCoroutine(SlotPopup);
                SlotPopup = null;
                objSlotPopup.SetActive(false);
            }
        }
    }
    IEnumerator CorBuildSlotPopup(int index)
    {
        yield return new WaitForSeconds(1);
        objSlotPopup.SetActive(true);
        objSlotPopup.transform.GetComponent<RectTransform>().anchoredPosition =
            objSlotGridLine.transform.GetChild(index).GetComponent<RectTransform>().anchoredPosition;
    }
}
