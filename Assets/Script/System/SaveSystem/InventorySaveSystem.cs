using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Inventory;
using System;
using System.IO;
using Newtonsoft.Json;

public class InvenSaveInfo
{
    public List<SlotItemData> savedInvenList;

    public InvenSaveInfo(List<SlotItemData> savedInvenList) {
        this.savedInvenList = savedInvenList;
    }
}
public class InventorySaveSystem : BaseSaveSystem
{
    string path;

    public Inventory inven;

    protected override void Start()
    {
        base.Start();
        path = "InventorySave_" + DataManager.Instance.nowSlot;
        LoadInventorySaved();
    }

    public override void Save()
    {
        SaveInventory(inven.InventoryDatas);
    }

    public void SaveInventory(List<SlotItemData> InventoryDatas)
    {
        InvenSaveInfo invenSaveInfo = new InvenSaveInfo(InventoryDatas);

        var json = JsonConvert.SerializeObject(invenSaveInfo, Formatting.Indented);
        File.WriteAllText(path, json);
    }

    void LoadInventorySaved()
    {
        if (File.Exists(path))
        {
            string JsonString = File.ReadAllText(path);

            InvenSaveInfo invenSaveInfo = JsonUtility.FromJson<InvenSaveInfo>(JsonString);

            if (invenSaveInfo == null || invenSaveInfo.savedInvenList == null)
            {
                Debug.LogError("JSON 데이터를 역직렬화하는데 실패했습니다.");
                return;
            }

            foreach (var item in invenSaveInfo.savedInvenList)
            {
                inven.AddItem(item.id, item.amount);
            }
        }
    }

}
