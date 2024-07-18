using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barricade : Structure
{
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
        Instantiate(destroyVFX, this.transform);
        Destroy(this.transform.gameObject, 2f);
    }

    protected override void Initialize()
    {
        base.Initialize();
    }

    
}
