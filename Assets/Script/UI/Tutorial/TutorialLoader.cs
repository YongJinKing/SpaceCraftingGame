using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class TutorialLoader : MonoBehaviour
{
    public Transform TutoCanvas;
    string savePos;
    private void Awake()
    {
        string filePath = Application.persistentDataPath + "/Save/" + DataManager.Instance.nowSlot.ToString();
        savePos = Path.Combine(filePath, "PlayerPos_" + DataManager.Instance.nowSlot + ".json");
        if(!File.Exists(savePos)) Instantiate(TutoCanvas, null);
    }
}
