using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barricade : Structure
{
    public GameObject barricadeImage;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        MaxHP = this[EStat.MaxHP];
        this.DestroyEvent.AddListener(TileManager.Instance.DestoryObjectOnTile);
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        Invoke("StopBuildVFX", 2f);
    }
    protected override void OnDead()
    {
        barricadeImage.gameObject.SetActive(false);
        this.GetComponent<BoxCollider2D>().enabled = false;
        DestroyEvent?.Invoke(this.transform.position);
        destroyVFX.gameObject.SetActive(true);
        Destroy(this.transform.gameObject, 1f);
    }

    protected override void Initialize()
    {
        base.Initialize();
    }

    private void OnMouseEnter()
    {
        buildingProduceAmountUI.ActiveAmountUI(this);
    }

    private void OnMouseExit()
    {
        buildingProduceAmountUI.DeActiveAmountUI();
    }


}
