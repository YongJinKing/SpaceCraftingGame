using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialItem : DropItem
{
    public string itemName;
    public string itemDesc;
    public Sprite img;

    SpecialItemPickupUI itemPickupPannel;
    protected override void OnEnable()
    {
        
    }
    protected override void Start()
    {
        base.Start();
        itemPickupPannel = FindObjectOfType<SpecialItemPickupUI>();
    }

    protected override void Update()
    {
        base.Update();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((1 << collision.gameObject.layer & layerMask) != 0)
        {
            itemPickupPannel.SetUpPannel(img, itemName, itemDesc);
            SoundManager.Instance.PlaySFX(SoundManager.Instance.itemSoundData.GetDropItem);
            Destroy(this.gameObject);
        }
    }

}
