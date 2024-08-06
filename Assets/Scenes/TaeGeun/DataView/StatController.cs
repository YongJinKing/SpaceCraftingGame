using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatController : MonoBehaviour
{
    public int hpIncrease = 10;
    public float speedIncrease = 1f;
    public float atkIncrease = 5;
    public float atkSpeedIncrease = 1f;
    
    public void IncreasePlayerStats()
    {
        PlayerDataStruct[] playerData = DataManager.Instance.pd;

        playerData[0].MaxHP += hpIncrease;
        playerData[0].moveSpeed += speedIncrease;
        playerData[0].ATK += atkIncrease;
        playerData[0].ATKSpeed += atkSpeedIncrease;

         // �α׷� ������ ���� Ȯ��
        Debug.Log("Stats increased: " +
                  $"MaxHP: {playerData[0].MaxHP}, " +
                  $"MoveSpeed: {playerData[0].moveSpeed}, " +
                  $"ATK: {playerData[0].ATK}, " +
                  $"ATKSpeed: {playerData[0].ATKSpeed}, "
                  );

        // ������ ����
        DataManager.Instance.SavePlayerInfo();
        DataManager.Instance.LoadJson("PlayerData" + DataManager.Instance.nowSlot.ToString() + ".json");
    }
}
