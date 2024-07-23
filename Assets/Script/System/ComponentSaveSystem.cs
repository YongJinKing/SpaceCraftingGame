using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static UnityEditor.Progress;

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

public class ComponentSaveSystem : Singleton<ComponentSaveSystem>
{
    ComponetsInfo componetsInfo;
    string savePath;
    public LayerMask structureLayerMask;
    public LayerMask resourcesLayerMask;
    
    // Start is called before the first frame update
    private void Awake()
    {
        
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            SaveTilesInfo();
        }
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
                        component.Hp = tile.Value.Object.GetComponent<Structure>().MaxHP;
                    }
                    else if ((resourcesLayerMask & 1 << tile.Value.Object.layer) != 0)
                    {
                        Debug.Log("�ڿ� ����");
                        component.index = tile.Value.Object.GetComponent<NaturalResources>().indexNum;
                        component.Hp = tile.Value.Object.GetComponent<NaturalResources>().hp;
                    }
                }
                else
                {
                    Debug.Log("����");
                    component.index = 0;
                    component.Hp = 0;
                }

                
                component.size = tile.Value.size;
                componentList.Add(component);
                Debug.Log(tile.Key + ", " + tile.Value);
                Debug.Log(component.coordinates + ", " + component.index + ", " + component.Hp);
            }
        }


        componetsInfo = new ComponetsInfo(componentList);
        var json = JsonConvert.SerializeObject(componetsInfo);
        //savePath = SceneManager.GetActiveScene().name + "/" + json;
        savePath = "SaveTestJson.json";
        File.WriteAllText(savePath, json);
    }

    string LoadJson()
    {
        var jsonPath = "SaveTestJson.json";
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

    public ComponetsInfo LoadTileInfo()
    {
        var jsonString = LoadJson();
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
            Debug.Log(item.coordinates + ", " + item.index + ", " + item.Hp);
        }

        return componetsList;

    }

}
