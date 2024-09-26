using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using UnityEngine;

public class GunManager : BaseSaveSystem
{
    public enum GunType
    {
        Rifle = 10000,
        Shotgun = 20000,
        Sniper = 30000,
    }
    public int[] gunIndexes = new int[3];
    public string savePath;
    protected override void Start()
    {
        base.Start();
        savePath = Path.Combine(filePath, "WeaponLevel_" + DataManager.Instance.nowSlot + ".json");

        //LoadGunIndexes();
        /*UpdateGunIndex(0, (int)GunType.Rifle);
        UpdateGunIndex(1, (int)GunType.Shotgun);
        UpdateGunIndex(2, (int)GunType.Sniper);*/
    }
    
    public override void Save()
    {
        if (DataManager.Instance.nowSlot == -1) return;
        base.Save();
        EquipInven EI = FindObjectOfType<EquipInven>();

        Debug.Log(EI.weapons[0] + " , " + EI.weapons[1] + ", " + EI.weapons[2]);
        string json = JsonUtility.ToJson(new GunIndexData(EI.weapons));
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

    public void UpdateGunIndex(int slot, int newIndex)
    {
        if(slot < 0 || slot>= gunIndexes.Length)
        {
            Debug.LogError("Invalid slot index");
            return;
        }
        gunIndexes[slot] = newIndex;
        Save();
    }
    public int GetGunIndex(int slot)
    {
        if(slot < 0 || slot >= gunIndexes.Length)
        {
            Debug.LogError("Invalid slot index");
            return -1; // ��ȿ���� ���� ��� -1 ��ȯ
        }

        return gunIndexes[slot];
    }

    public int[] LoadGunIndexes()
    {
        if(File.Exists(savePath))
        {
            Debug.Log("����� ������ ã�ҽ��ϴ�: " + savePath);
            string json = File.ReadAllText(savePath);
            GunIndexData data = JsonUtility.FromJson<GunIndexData>(json);
            gunIndexes = data.indexes;
        }
        else
        {
            // �ѱ��ȣ �ε��� �ֱ�
            gunIndexes[0] = (int)GunType.Rifle;
            gunIndexes[1] = (int)GunType.Shotgun;
            gunIndexes[2] = (int)GunType.Sniper;
            //Save();
        }

        return gunIndexes;
    }

    [Serializable]
    private class GunIndexData
    {
        public int[] indexes;

        public GunIndexData(int[] indexes)
        {
            this.indexes = indexes;
        }
    }
}
