using System;
using UnityEngine;

public class PlayerFactory
{
    public GameObject Create(int index)
    {
        PlayerDataStruct playerData = default;
        //여기에서 Json으로 데이터 불러오기
        //if(index == ~~~)
        //playerData = ~~~~;

        return Create(playerData);
    }

    public GameObject Create(PlayerDataStruct data)
    {
        GameObject playerObject = new GameObject(data.Index.ToString());

        Rigidbody2D rb = playerObject.AddComponent<Rigidbody2D>();
        rb.mass = 100;
        rb.gravityScale = 0;
        rb.freezeRotation = true;

        CircleCollider2D collider = playerObject.AddComponent<CircleCollider2D>();
        collider.radius = 0.5f;

        Player player = playerObject.AddComponent<Player>();
        player.MaxHP = data.MaxHP;
        player.Priority = data.Priority;
        player.moveSpeed = data.MoveSpeed;
        player.ATK = data.ATK;
        player.ATKSpeed = data.ATKSpeed;

        //GameObject model = GetModel(playerData.ModelPrefabIndex);
        GameObject model = GameObject.Instantiate(Resources.Load<GameObject>("SpineToUnity/Charactor/SpaceHuman"));
        model.transform.SetParent(playerObject.transform, false);

        player.graphicTransform = model.transform;
        player.weaponRotationAxis = model.GetComponentInChildren<WeaponRotationAxis>();

        playerObject.AddComponent<UnitMovement>();
        playerObject.AddComponent<PlayerEquipmentManager>();

        return playerObject;
    }
}
