using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpaceShipSondData", menuName = "Scriptable Object/SpaceShipSondData")]
public class SpaceShipSondData : ScriptableObject
{
    public AudioClip sacpeshipBoost;
    public AudioClip spaceshipChangeMode;
    public AudioClip spaceshipSearching;
    public AudioClip spaceshipLand;
    public AudioClip spaceShipWarning;
    public AudioClip spaceShipAttacked;
}
