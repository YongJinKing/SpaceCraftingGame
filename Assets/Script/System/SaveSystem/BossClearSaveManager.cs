using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public class BossClearInfo
{
    public bool isClear;

    public BossClearInfo(bool clear)
    {
        this.isClear = clear;
    }

}

public class BossClearSaveManager : BaseSaveSystem
{
    public string savePath;
    protected override void Start()
    {
        base.Start();
        savePath = Path.Combine(filePath, "BossClearData_" + DataManager.Instance.nowSlot + ".json");
    }

    public override void Save() // ��� ������ Ŭ���� ���� ���� �۵�
    {
        if (DataManager.Instance.nowSlot == -1) return;
        base.Save();
        BossClearInfo bossClearInfo = new BossClearInfo(true);

        var json = JsonConvert.SerializeObject(bossClearInfo, Formatting.Indented);

        File.WriteAllText(savePath, json);

        if (File.Exists(savePath))
        {
            Debug.Log("������ ���������� ����Ǿ����ϴ�: " + savePath);
        }
        else
        {
            Debug.LogError("���� ���忡 �����߽��ϴ�: " + savePath);
        }
    }

    public bool LoadClearInfo()
    {
        bool isClear = false;
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            BossClearInfo data = JsonUtility.FromJson<BossClearInfo>(json);
            isClear = data.isClear;
        }
        else
        {
            Debug.Log("���� ����");
        }

        return isClear;
    }
}
