using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmPopup : MonoBehaviour
{
    [SerializeField] private Transform ResourcesPrefab;
    private Transform Contents;
    
    
    private void Awake() 
    {
        Contents = transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0);
    }
    private void OnEnable() 
    {
        if(Contents.childCount != 0)
        {
            for(int i = 0; i < Contents.childCount; i++)
            {
                Contents.GetChild(i).GetComponent<ResourceSlot>().DestroySelf();
            }
        }
    }
    public void UpdatePopup(int id)
    {
        var riggingItemInstance = RiggingItemStaticDataManager.GetInstance();
        
        riggingItemInstance.LoadRiggingItemDatas();
        
        var riggingItemData = riggingItemInstance.dicRiggingItemData[id];
        for(int i = 0; i < riggingItemData.Consume_IndexArr.Length; i++)
        {
            Instantiate(ResourcesPrefab, Contents);
            Contents.GetChild(i).GetComponent<ResourceSlot>().Init(riggingItemData.Consume_IndexArr[i], riggingItemData.Consume_CountArr[i]);
        }   
    }
}
