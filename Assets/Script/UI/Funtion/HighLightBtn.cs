
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;



public class HighLightBtn : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{   
    public UnityEvent<int, bool> OnHighLite;
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(Inventory.instance.DisplayInven.Count > transform.GetSiblingIndex())
            OnHighLite?.Invoke(transform.GetSiblingIndex(),true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {   
        if(Inventory.instance.DisplayInven.Count > transform.GetSiblingIndex())
            OnHighLite?.Invoke(transform.GetSiblingIndex(),false);
    }
}