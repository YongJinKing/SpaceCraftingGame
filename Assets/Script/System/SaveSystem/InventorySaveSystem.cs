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
public class InventorySaveSystem : MonoBehaviour
{
    string path;

    public Inventory inven;

    private void Start()
    {
        path = "InventorySave_" + DataManager.Instance.nowSlot;
        LoadInventorySaved();
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
                Debug.LogError("JSON �����͸� ������ȭ�ϴµ� �����߽��ϴ�.");
                return;
            }

            foreach (var item in invenSaveInfo.savedInvenList)
            {
                inven.AddItem(item.id, item.amount);
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            Debug.Log("�κ� ���̺�");
            SaveInventory(inven.InventoryDatas);
        }
    }
}
