using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using Newtonsoft.Json;

public class BossSearchedInfo
{
    public bool isSearched;
    public float searchedTime;

    public BossSearchedInfo(bool _isSearched, float _searchedTime) {
        isSearched = _isSearched;
        searchedTime = _searchedTime;
    }
}
public class BossSearchedInfoSaveSystem : MonoBehaviour
{
    public BossSearchingUI bossSearching;
    string path;
    BossSearchedInfo bossSearchedInfo;

    private void Awake()
    {
        LoadBossSearchedInfo();
    }

    public void SaveBossSearchedInfo()
    {
        bool isSearchedNow = bossSearching.isSearching;
        float searchingTimeNow = bossSearching.searchedTime;

        bossSearchedInfo = new BossSearchedInfo(isSearchedNow,searchingTimeNow);
        var json = JsonConvert.SerializeObject(bossSearchedInfo, Formatting.Indented);
        path = "BossSearchedInfoSaved_" + DataManager.Instance.nowSlot;
        File.WriteAllText(path, json);
    }

    void LoadBossSearchedInfo()
    {
        path = "BossSearchedInfoSaved_" + DataManager.Instance.nowSlot;
        if (File.Exists(path))
        {
            string JsonString = File.ReadAllText(path);
            bossSearchedInfo = JsonUtility.FromJson<BossSearchedInfo>(JsonString);
            bossSearching.isSearching = bossSearchedInfo.isSearched;
            bossSearching.searchedTime = bossSearchedInfo.searchedTime;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            SaveBossSearchedInfo();
        }
    }
}
