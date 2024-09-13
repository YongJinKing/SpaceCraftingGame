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
public class BossSearchedInfoSaveSystem : BaseSaveSystem
{
    public BossSearchingUI bossSearching;
    string path;
    BossSearchedInfo bossSearchedInfo;
    

    public override void Save()
    {
        if (DataManager.Instance.nowSlot == -1) return;
        base.Save();
        bool isSearchedNow = bossSearching.isSearching;
        float searchingTimeNow = bossSearching.searchedTime;
        if (!isSearchedNow) return;

        bossSearchedInfo = new BossSearchedInfo(isSearchedNow, searchingTimeNow);
        var json = JsonConvert.SerializeObject(bossSearchedInfo, Formatting.Indented);

        File.WriteAllText(path, json);
    }

    void LoadBossSearchedInfo()
    {
        if (File.Exists(path))
        {
            string JsonString = File.ReadAllText(path);
            bossSearchedInfo = JsonUtility.FromJson<BossSearchedInfo>(JsonString);
            bossSearching.isSearching = bossSearchedInfo.isSearched;
            bossSearching.searchedTime = bossSearchedInfo.searchedTime;
        }
    }

    protected override void Start()
    {
        base.Start();
        path = Path.Combine(filePath, "BossSearchedInfoSaved_" + DataManager.Instance.nowSlot +".json");

        LoadBossSearchedInfo();
    }

}
