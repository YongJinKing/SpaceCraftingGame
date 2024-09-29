using Newtonsoft.Json;
using Spine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class RecentTime
{
    public float time;
    public int waveCount;

    public RecentTime(float time, int waveCount)
    {
        this.time = time;
        this.waveCount = waveCount;
    }   

}
public class TimeSaveManager : BaseSaveSystem
{
    public string savePath;
    public TimeManager timeManager;
    WaveManager waveManager;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        savePath = Path.Combine(filePath, "RecentTime_" + DataManager.Instance.nowSlot + ".json");
        timeManager = FindObjectOfType<TimeManager>();
        waveManager = FindObjectOfType<WaveManager>();
        if (timeManager != null)
        {
            LoadTime(savePath);
        }
    }

    public override void Save()
    {
        if (DataManager.Instance.nowSlot == -1) return;
        base.Save();
        RecentTime recentTime = new RecentTime(timeManager.timeCount, waveManager.waveCount);

        var json = JsonConvert.SerializeObject(recentTime, Formatting.Indented);
        File.WriteAllText(savePath, json);
    }

    void LoadTime(string path)
    {
        var jsonPath = path;
        if (File.Exists(jsonPath))
        {
            string json = File.ReadAllText(jsonPath);
            RecentTime data = JsonUtility.FromJson<RecentTime>(json);
            timeManager.timeCount = data.time;
            waveManager.waveCount = data.waveCount;
        }
        else
        {
            Debug.Log("¾øÀ½");
        }
    }

}
