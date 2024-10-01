using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class RecentWave
{
    public int waveCount;
    public int prevWave;
    public RecentWave(int waveCount, int prevWave)
    {
        this.waveCount = waveCount;
        this.prevWave = prevWave;   
    }

}

public class WaveSaveManager : BaseSaveSystem
{
    public string savePath;
    public WaveManager waveManager;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        savePath = Path.Combine(filePath, "RecentWave_" + DataManager.Instance.nowSlot + ".json");
        waveManager = FindObjectOfType<WaveManager>();
        if (waveManager != null)
        {
            LoadTime(savePath);
        }
    }

    public override void Save()
    {
        if (DataManager.Instance.nowSlot == -1) return;
        base.Save();
        RecentWave recentTime = new RecentWave(waveManager.waveCount, waveManager.priviousWaveDate);

        var json = JsonConvert.SerializeObject(recentTime, Formatting.Indented);
        File.WriteAllText(savePath, json);
    }

    void LoadTime(string path)
    {
        var jsonPath = path;
        if (File.Exists(jsonPath))
        {
            string json = File.ReadAllText(jsonPath);
            RecentWave data = JsonUtility.FromJson<RecentWave>(json);
            waveManager.waveCount = data.waveCount;
            waveManager.priviousWaveDate = data.prevWave;
        }
        else
        {
            Debug.Log("¾øÀ½");
        }
    }
}
