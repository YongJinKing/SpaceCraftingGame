using UnityEngine;

public class PlayerFactory
{
    public GameObject CreatePlayer(int index)
    {
        GameObject playerObject = new GameObject(index.ToString());
        
        PlayerDataStruct playerData = default;
        //여기에서 Json으로 데이터 불러오기
        //if(index == ~~~)
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
        player.moveSpeed = playerData.MoveSpeed;
        player.ATK = playerData.ATK;
        player.ATKSpeed = playerData.ATKSpeed;


        //GameObject model = GetModel(playerData.ModelPrefabIndex);
        GameObject model = Resources.Load<GameObject>("SpineToUnity/Charactor/SpaceHuman");
        model.transform.SetParent(playerObject.transform, false);

        player.graphicTransform = model.transform;
        player.weaponRotationAxis = model.GetComponentInChildren<WeaponRotationAxis>();

        playerObject.AddComponent<UnitMovement>();
        playerObject.AddComponent<PlayerEquipmentManager>();

        return playerObject;
    }
}
