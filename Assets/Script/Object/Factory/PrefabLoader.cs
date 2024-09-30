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
    private bool inited = false;
    public Dictionary<int, PrefabData> Datas = new Dictionary<int, PrefabData>();

    public GameObject Load(int index)
    {
        if (!inited)
        {
            var s = Resources.Load<TextAsset>("Component/WeaponFactory/Pexplorer_Prefab").text;
            PrefabData[] prefabDatas = JsonConvert.DeserializeObject<PrefabData[]>(s);
            Datas = prefabDatas.ToDictionary(x => x.Index);
            inited = true;
        }
        

        if (Datas.ContainsKey(index))
        {
            return Resources.Load<GameObject>($"{Datas[index].Name}");
        }
        else
        {
            Debug.Log("Fail to Load Prefab");
            return null;
        }
            
    }

}