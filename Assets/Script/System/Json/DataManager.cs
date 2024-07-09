using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using Spine;


public class DataSlots : MonoBehaviour
{
    ResourcesSpawner resourcesSpawner = new ResourcesSpawner
    {

    };
    
    public void Start()
    {
        // json = ResourcesSpawner(resourcesSpawner);
    }
}

public static class DataManager
{

    public static string ResourcesSpawnerData(ResourcesSpawner resourcesSpawner)
    {
        return JsonUtility.ToJson(resourcesSpawner);
    }
}


