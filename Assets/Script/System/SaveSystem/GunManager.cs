using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using UnityEngine;

public class GunManager : Singleton<GunManager>
{
    public enum GunType
    {
        Rifle = 10100,
        Shotgun = 10200,
        Sniper = 10300,
    }
    public int[] gunIndexes = new int[3];
    public string savePath;
    void Start()
    {
        savePath = "WeaponLevel" + DataManager.Instance.nowSlot;
        SaveGunIndexs();
        LoadGunIndexes();
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            SaveGunIndexs();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadGunIndexes();
        }
    }
    public void SaveGunIndexs()
    {
        string json = JsonUtility.ToJson(new GunIndexData(gunIndexes));
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
        SaveGunIndexs();
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
