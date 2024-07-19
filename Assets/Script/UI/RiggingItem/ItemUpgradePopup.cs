using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUpgradePopup : MonoBehaviour
{
    private UpgradeSelelctType upgradeSelelctType;
    private RiggingItemContents riggingItemContents;
    
    private void Start() 
    {
        riggingItemContents.OnLevelUpAct += RiggingItemContents_OnLevelUpAct;
    }
    public void RiggingItemContents_OnLevelUpAct(object sender, System.EventArgs e)
    {
       transform.GetChild(1).gameObject.SetActive(true);

    }
}
