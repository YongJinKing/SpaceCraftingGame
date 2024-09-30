using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPopup : MonoBehaviour
{
    public GameObject ObjSlotGridLine;
    public GameObject ObjOptionPanel;
    public GameObject ObjSlotPopup;
    Coroutine SlotPopup;
    private void Start()
    {
        Inventory.instance.updatePopup.AddListener(PopupUpdate);
    }
    private void OnEnable() 
    {
        PopupUpdate();//팝업 업데이트   
    }
    public void PopupUpdate()
    {   
        for(int i = 0; i < ObjSlotGridLine.transform.childCount; i++)
        {
            if(Inventory.instance.GetDisplayInvenDataWithIdLength() > ObjSlotGridLine.transform.GetChild(i).GetSiblingIndex())
            {
                ObjSlotGridLine.transform.GetChild(i).GetComponent<InvenItemSlot>().Display();
                //Inventory스크립트에 있는 DisplayInven에 값이 있을 경우 Display 실행
                ObjSlotGridLine.transform.GetChild(i).GetChild(0).gameObject.SetActive(true);
                //Debug.Log("팝업업데이트 작동확인");
            }
            else
            {
                ObjSlotGridLine.transform.GetChild(i).GetComponent<InvenItemSlot>().init();
                //없을 경우 초기화
                ObjSlotGridLine.transform.GetChild(i).GetChild(0).gameObject.SetActive(false);
            }
        }
        
    }


  
  
    /* public void SlotHL(int index, bool onCheck)//마우스 커서 올리면 해당 아이템주변에 하이라이트 생김
    {
        ObjSlotGridLine.transform.GetChild(index).GetChild(2).gameObject.SetActive(onCheck);
        if(onCheck)
        {
            SlotPopup = StartCoroutine(CorSlotPopup(index));
        }
        else
        {
            if(SlotPopup != null)
            {
                StopCoroutine(SlotPopup);
                SlotPopup = null;
                ObjSlotPopup.SetActive(false);
            }
        }
    } */
    
    /* IEnumerator CorSlotPopup(int index)//마우스 올리고 1초뒤에 팝업창 생기는 코루틴 단, 아이템 있을 경우 실행됨
    {
        yield return new WaitForSeconds(1);
        ObjSlotPopup.SetActive(true);
        ObjSlotPopup.transform.GetComponent<RectTransform>().anchoredPosition=
        ObjSlotGridLine.transform.GetChild(index).GetComponent<RectTransform>().anchoredPosition;
    } */
}

