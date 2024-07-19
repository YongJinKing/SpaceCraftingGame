using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class RiggingItemContents : MonoBehaviour
{
    [SerializeField] private Transform RiggingItemSlot;

    private RiggingItemSlot riggingItemSlot;

    public event EventHandler OnLevelUpAct;

    private int[] riggingItemLevelData = {1, 1, 1, 1, 1};
    private int rifleLevel = 1;
    private int shotGunLevel = 1;
    private int sniperLevel = 1;
    private int pickLevel = 1;
    private int hammerLevel = 2;

    private int riggingItemCount = 3;
    private void Start() 
    { 
        riggingItemLevelData[0] = rifleLevel;
        riggingItemLevelData[1] = shotGunLevel;
        riggingItemLevelData[2] = sniperLevel;
        riggingItemLevelData[3] = pickLevel;
        riggingItemLevelData[4] = hammerLevel;
        for(int i = 0; i < riggingItemCount; i++)
        {
            Instantiate(RiggingItemSlot, transform);
            transform.GetChild(i).GetComponent<RiggingItemSlot>().Init(i, riggingItemLevelData[i]);
            
        }
        Button[] LevelUpBtnList = transform.GetComponentsInChildren<Button>();
        for(int i = 0; i < LevelUpBtnList.Length; i++)
        {
            int index = i;
            LevelUpBtnList[i].onClick.AddListener(() => PressedLevelUpBtn(index));
        } 
    }
    private void PressedLevelUpBtn(int index)
    {
        OnLevelUpAct?.Invoke(this, EventArgs.Empty);
        transform.GetChild(index).GetComponent<RiggingItemSlot>().Init(index, ++riggingItemLevelData[index]);
    }

    
    
    
}
