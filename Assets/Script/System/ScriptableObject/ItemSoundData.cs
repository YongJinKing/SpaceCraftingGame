using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemSoundData", menuName = "Scriptable Object/ItemSoundData")]
public class ItemSoundData : ScriptableObject
{
    public AudioClip NaturalResourceBroken;
    public AudioClip GetDropItem;
    public AudioClip GetFactoryItem;

}
