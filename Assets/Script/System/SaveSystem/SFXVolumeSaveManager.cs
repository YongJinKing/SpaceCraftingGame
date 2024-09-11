using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SoundVolume
{
    public float bgm;
    public float sfx;

    public SoundVolume(float bgm, float sfx)
    {
        this.bgm = bgm;
        this.sfx = sfx;
    }
}
public class SFXVolumeSaveManager : BaseSaveSystem
{
    string path;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        path = Path.Combine(filePath, "SFXVolume_" + DataManager.Instance.nowSlot + ".json");

        LoadSFXVolumeSaved();
    }

    public override void Save()
    {
        SoundVolume Sv = new SoundVolume(SoundManager.Instance.GetBGMVolume(), SoundManager.Instance.GetSFXVolume());

        var json = JsonConvert.SerializeObject(Sv, Formatting.Indented);
        File.WriteAllText(path, json);
    }


    void LoadSFXVolumeSaved()
    {
        if (File.Exists(path))
        {
            string JsonString = File.ReadAllText(path);

            SoundVolume sfxSaveInfo = JsonUtility.FromJson<SoundVolume>(JsonString);

            if (sfxSaveInfo == null)
            {
                Debug.LogError("JSON �����͸� ������ȭ�ϴµ� �����߽��ϴ�.");
                return;
            }

            SoundManager.Instance.SetBGMVolume(sfxSaveInfo.bgm);
            SoundManager.Instance.SetSFXVolume(sfxSaveInfo.sfx);
            
        }
        else
        {
            SoundManager.Instance.SetBGMVolume(1f);
            SoundManager.Instance.SetSFXVolume(1f);
        }
    }
}
