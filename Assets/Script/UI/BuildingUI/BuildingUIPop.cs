using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingUIPop : MonoBehaviour
{
    public GameObject objSlotGridLine;
    public GameObject objSlotPopup;
    Coroutine SlotPopup;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PopupBuildUpadate()
    {
        for (int i = 0; i < objSlotGridLine.transform.childCount; i++)
        {
            if (CraftBuildingUIManager.instance.ApplyTypeID.Count > objSlotGridLine.transform.GetChild(i).GetSiblingIndex())
            {
                objSlotGridLine.transform.GetChild(i).GetComponent<BuildingUISlot>().InputSlotData();
                objSlotGridLine.transform.GetChild(i).GetChild(0).gameObject.SetActive(true);
            }
            else
            {
                objSlotGridLine.transform.GetChild(i).GetChild(0).gameObject.SetActive(false);
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
