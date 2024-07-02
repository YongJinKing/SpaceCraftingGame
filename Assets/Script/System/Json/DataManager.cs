using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public static class DataManager
{
    public static string serializeComponents(ComponetsInfo componetsInfo)
    {
        return JsonUtility.ToJson(componetsInfo);
    }

    public static string PlayerData(Player player)
    {
        return JsonUtility.ToJson(player);
    }
    
    public static string WeaponData(Weapon weapon)
    {
        return JsonUtility.ToJson(weapon);
    }


}


