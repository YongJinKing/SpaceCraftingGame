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

    public static string StatData(Stat stat)
    {
        return JsonUtility.ToJson(stat);
    } 

    public static string PlayerData(Player player)
    {
        return JsonUtility.ToJson(player);
    }
    
    public static string WeaponData(Weapon weapon)
    {
        return JsonUtility.ToJson(weapon);
    }

    public static string MonsterData(Monster monster)
    {
        return JsonUtility.ToJson(monster);
    }

    public static string UnitData(Unit unit)
    {
        return JsonUtility.ToJson(unit);
    }

    public static string MonsterStateData(MonsterState monsterstate)
    {
        return JsonUtility.ToJson(monsterstate);
    }

    public static string TurretData(Turret turret)
    {
        return JsonUtility.ToJson(turret);
    }

    public static string ResourcesSpawnerData(ResourcesSpawner resourcesSpawner)
    {
        return JsonUtility.ToJson(resourcesSpawner);
    }
}


