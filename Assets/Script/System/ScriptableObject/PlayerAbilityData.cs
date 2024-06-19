using UnityEngine;



[CreateAssetMenu(fileName = "PlayerAbilityData", menuName = "Scriptable Object/PlayerAbilityData", order = int.MaxValue)]
public class PlayerAbilityData : ScriptableObject 
{
    [SerializeField]
    private string playerName;
    public string PlayerName 
    {
        get => playerName; 
    }
    [SerializeField]
    private int hp;
    public int Hp 
    {
        get => hp;
    }
    [SerializeField]
    private int moveSpeed;
    public int MoveSpeed
    {
        get => moveSpeed; 
    }
}

