using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using Newtonsoft.Json;

/*public struct Component
{
    public Vector3Int coordinates;
    public int index;
    public float Hp;
}

[System.Serializable]
public class ComponetsInfo
{
    public List<Component> components;
    public ComponetsInfo(List<Component> components)
    {
        this.components = components;
    }
}*/

[Serializable]
public class Component
{
    public Vector3Int coordinates;
    public int index;
    public float Hp;
    public int size;
    public bool placeable;
    public int producedAmount;

    // JSON ���ڿ����� Vector3Int�� ���� ó���� �� ���� ������,
    // ����ȭ �� ������ȭ �� ���ڿ��� ��ȯ�ϴ� �߰� �ʵ尡 �ʿ���
    public string coordinatesString;

    public void OnBeforeSerialize()
    {
        coordinatesString = $"{coordinates.x},{coordinates.y},{coordinates.z}";
    }

    public void OnAfterDeserialize()
    {
        var parts = coordinatesString.Split(',');
        if (parts.Length == 3 &&
            int.TryParse(parts[0], out int x) &&
            int.TryParse(parts[1], out int y) &&
            int.TryParse(parts[2], out int z))
        {
            coordinates = new Vector3Int(x, y, z);
        }
        else
        {
            Debug.LogError("Invalid coordinates string: " + coordinatesString);
        }
    }
}

[Serializable]
public class ComponetsInfo
{
    public List<Component> components;

    public ComponetsInfo(List<Component> components)
    {
        this.components = components;
    }
}

public class ComponentSaveSystem : BaseSaveSystem
{
    ComponetsInfo componetsInfo;
    public string savePath;
    public LayerMask structureLayerMask;
    public LayerMask resourcesLayerMask;
    
    // Start is called before the first frame update
    private void Awake()
    {
        filePath = Application.persistentDataPath + "/Save/" + DataManager.Instance.nowSlot.ToString();
        MakeDir(filePath);

        savePath = Path.Combine(filePath, "TileSaveData_" + DataManager.Instance.nowSlot + ".json");
    }

    protected override void Start()
    {
        //base.Start();
        totalSaveManager = this.GetComponentInParent<TotalSaveManager>();
        totalSaveManager.saves.Add(this);
        //savePath = Path.Combine(filePath, "TileSaveData_" + DataManager.Instance.nowSlot + ".json");

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Save()
    {
        if (DataManager.Instance.nowSlot == -1) return;
        base.Save();
        SaveTilesInfo();
    }

    public void SaveTilesInfo()
    {
        
        TileManager tileManager = FindObjectOfType<TileManager>();
        Dictionary<Vector3Int, Tile> tmpDic = tileManager.GetTileMap();

        List<Component> componentList = new List<Component>();
        foreach (KeyValuePair<Vector3Int, Tile> tile in tmpDic)
        {
            if (!tile.Value.available)
            {
                Component component = new Component();
                Vector3Int coordinate = new Vector3Int(tile.Key.x, tile.Key.y, tile.Key.z);
                component.coordinates = coordinate;
                component.OnBeforeSerialize();
                if (!(tile.Value.Object == null))
                {
                    Debug.Log("�� �ƴ�");
                    if ((structureLayerMask & 1 << tile.Value.Object.layer) != 0)
                    {
                        Debug.Log("�ǹ�");
                        Debug.Log(tile.Value.Object.GetComponent<Structure>().mComponentName);
                        component.index = int.Parse(tile.Value.Object.GetComponent<Structure>().mComponentName);
                        component.Hp = tile.Value.Object.GetComponent<Structure>()[EStat.HP];
                        component.producedAmount = 0;
                        if (tile.Value.Object.CompareTag("Factory")) component.producedAmount = tile.Value.Object.GetComponent<FactoryBuilding>().produceAmount;
                    }
                    else if ((resourcesLayerMask & 1 << tile.Value.Object.layer) != 0)
                    {
                        Debug.Log("�ڿ� ����");
                        component.index = tile.Value.Object.GetComponent<NaturalResources>().indexNum;
                        component.Hp = tile.Value.Object.GetComponent<NaturalResources>().hp;
                        component.producedAmount = 0;
                    }
                }
                else
                {
                    Debug.Log("����");
                    component.index = 0;
                    component.Hp = 0;
                    component.producedAmount = 0;
                }

                
                component.size = tile.Value.size;
                componentList.Add(component);
                Debug.Log(tile.Key + ", " + tile.Value);
                Debug.Log(component.coordinates + ", " + component.index + ", " + component.Hp);
            }
        }


        componetsInfo = new ComponetsInfo(componentList);
        var json = JsonConvert.SerializeObject(componetsInfo, Formatting.Indented);
        //savePath = SceneManager.GetActiveScene().name + "/" + json;
        File.WriteAllText(savePath, json);
    }

    string LoadJson(string path)
    {
        var jsonPath = path;
        if (File.Exists(jsonPath))
        {
            string JsonString = File.ReadAllText(jsonPath);
            Debug.Log("�б�");
            return JsonString;
        }
        else
        {
            Debug.Log("����");
            return string.Empty;
        }
    }

    public ComponetsInfo LoadTileInfo(string path)
    {
        var jsonString = LoadJson(path);
        if (string.IsNullOrEmpty(jsonString))
        {
            Debug.LogError("JSON �����Ͱ� ��� �ֽ��ϴ�.");
            return null;
        }

        ComponetsInfo componetsList = JsonUtility.FromJson<ComponetsInfo>(jsonString);
        if (componetsList == null || componetsList.components == null)
        {
            Debug.LogError("JSON �����͸� ������ȭ�ϴµ� �����߽��ϴ�.");
            return null;
        }

        foreach (var item in componetsList.components)
        {
            // ������ȭ �� coordinates ����
            item.OnAfterDeserialize();
        }

        return componetsList;

    }

    
}
