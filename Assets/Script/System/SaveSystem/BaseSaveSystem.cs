using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class BaseSaveSystem : MonoBehaviour, ISave
{
    protected TotalSaveManager totalSaveManager;
    public string filePath;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        totalSaveManager = this.GetComponentInParent<TotalSaveManager>();
        totalSaveManager.saves.Add(this);

        filePath = Application.persistentDataPath + "/Save/" + DataManager.Instance.nowSlot.ToString();

        MakeDir(filePath);
    }

    public virtual void Save() { }

    protected void MakeDir(string path)
    {
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
            Debug.Log("Save 폴더가 생성되었습니다.");
        }
    }

}
