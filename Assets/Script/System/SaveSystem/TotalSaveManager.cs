using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TotalSaveManager : MonoBehaviour
{
    public List<ISave> saves = new List<ISave>();

    public void SaveAll()
    {
        foreach(ISave save in saves)
        {
            save.Save();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            SaveAll();
        }
    }
}
