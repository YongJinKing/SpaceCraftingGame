using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using System.Linq;

public struct PrefabData
{
    public int Index;
    public string Name;
}

public class PrefabLoader
{
    public Dictionary<int, PrefabData> Datas = new Dictionary<int, PrefabData>();
    public PrefabLoader()
    {
        string s = File.ReadAllText("Assets/Prefab/JongHyun/Pexplorer_Prefab.json");
        PrefabData[] prefabDatas = JsonConvert.DeserializeObject<PrefabData[]>(s);
        Datas = prefabDatas.ToDictionary(x => x.Index);
    }

    public GameObject Load(int index)
    {
        if (Datas.ContainsKey(index))
        {
            return GameObject.Instantiate(Resources.Load<GameObject>($"SpineToUnity/WeaponPrefab/{Datas[index].Name}"));
        }
        else
        {
            Debug.Log("Fail to Load Prefab");
            return null;
        }
            
    }

}