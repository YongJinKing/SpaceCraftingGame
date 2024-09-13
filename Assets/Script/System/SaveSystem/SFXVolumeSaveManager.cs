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
    public bool bgmMute;
    public bool sfxMute;

    public SoundVolume(float bgm, float sfx, bool bgmMute, bool sfxMute)
    {
        this.bgm = bgm;
        this.sfx = sfx;
        this.bgmMute = bgmMute;
        this.sfxMute = sfxMute; 
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
        if (DataManager.Instance.nowSlot == -1) return;
        filePath = Application.persistentDataPath + "/Save/" + DataManager.Instance.nowSlot.ToString();
        base.Save();
        path = Path.Combine(filePath, "SFXVolume_" + DataManager.Instance.nowSlot + ".json");
        SoundVolume Sv = new SoundVolume(SoundManager.Instance.GetBGMVolume(), SoundManager.Instance.GetSFXVolume(), SoundManager.Instance.isBGMMuted, SoundManager.Instance.isSFXMuted);

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
                Debug.LogError("JSON 데이터를 역직렬화하는데 실패했습니다.");
                return;
            }

            SoundManager.Instance.SetBGMVolume(sfxSaveInfo.bgm);
            SoundManager.Instance.SetSFXVolume(sfxSaveInfo.sfx);
            SoundManager.Instance.ToggleBGMMute(sfxSaveInfo.bgmMute);
            SoundManager.Instance.ToggleSFXMute(sfxSaveInfo.sfxMute);
        }
        else
        {
            SoundManager.Instance.SetBGMVolume(1f);
            SoundManager.Instance.SetSFXVolume(1f);
        }
    }
}
