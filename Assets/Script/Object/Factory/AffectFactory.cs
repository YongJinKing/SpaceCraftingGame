using Newtonsoft.Json;
using System.IO;
using UnityEngine;
using UnityEngine.UIElements;

public class AffectFactory
{


    public GameObject Create(int index)
    {
        GameObject gameObject = null;

        switch(index / 10000)
        {
            //DamageAffect
            case 1:
                {
                    DamageAffectStruct data = default;
                    string s = File.ReadAllText("Assets/Prefab/JongHyun/Affect/Pexplorer_Damage_Affect.json");
                    foreach (DamageAffectStruct temp in JsonConvert.DeserializeObject<DamageAffectStruct[]>(s))
                    {
                        if (temp.Index == index)
                        {
                            data = temp;
                            break;
                        }
                    }
                    if (data.Equals(default))
                    {
                        break;
                    }

                    gameObject = new GameObject(index.ToString());

                    DamageAffect damage = gameObject.AddComponent<DamageAffect>();

                    for(int i = 0; i < data.LayerMask.Length; ++i)
                    {
                        damage.targetMask = damage.targetMask | (1 << data.LayerMask[i]);
                    }
                    
                    damage.power = data.power;
                }
                break;
            case 2: 
                { }
                break;
            case 3:
                { }
                break;
        }

        return gameObject;
    }
}
