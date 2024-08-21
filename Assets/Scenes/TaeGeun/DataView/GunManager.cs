using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GunManager : Singleton<GunManager>
{
    public int[] gunIndexes = new int[3];
    public string savePath;
    void Start()
    {
        LoadGunIndexes();
    }

    public void SaveGunIndexs()
    {
        string json = JsonUtility.ToJson(new GunIndexData(gunIndexes));
        File.WriteAllText(savePath, json);
    }

    public void UpdateGunIndex(int slot, int newIndex)
    {
        if(slot < 0 || slot>= gunIndexes.Length)
        {
            Debug.LogError("Invalid slot index");
            return;
        }
        gunIndexes[slot] = newIndex;
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

    public void LoadGunIndexes()
    {
        if(File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            GunIndexData data = JsonUtility.FromJson<GunIndexData>(json);
            gunIndexes = data.indexes;
        }
        else
        {
            // �ѱ��ȣ �ε��� �ֱ�
            gunIndexes[0] = 10100;
            gunIndexes[1] = 10200;
            gunIndexes[2] = 10300;
            SaveGunIndexs();
        }
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
