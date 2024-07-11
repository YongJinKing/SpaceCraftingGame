using UnityEngine;

public class PlayerFactory
{
    public GameObject CreatePlayer(int index)
    {
        GameObject playerObject = new GameObject(index.ToString());
        
        PlayerDataStruct playerData = new PlayerDataStruct();
        //���⿡�� Json���� ������ �ҷ�����
        //playerData = ~~~~;
        
        Rigidbody2D rb = playerObject.AddComponent<Rigidbody2D>();
        rb.mass = 100;
        rb.gravityScale = 0;
        rb.freezeRotation = true;

        CircleCollider2D collider = playerObject.AddComponent<CircleCollider2D>();
        collider.radius = 0.5f;

        Player player = playerObject.AddComponent<Player>();
        player.MaxHP = playerData.MaxHP;
        player.Priority = playerData.Priority;
        player.moveSpeed = playerData.moveSpeed;
        player.ATK = playerData.ATK;
        player.ATKSpeed = playerData.ATKSpeed;

        playerObject.AddComponent<UnitMovement>();
        //playerObject.AddComponent<PlayerEquipmentManager>();

        return playerObject;
    }
}
