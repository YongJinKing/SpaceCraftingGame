using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSaveSystem : MonoBehaviour, ISave
{
    protected TotalSaveManager totalSaveManager;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        totalSaveManager = FindObjectOfType<TotalSaveManager>();
        totalSaveManager.saves.Add(this);
    }

    public virtual void Save() { }

}
