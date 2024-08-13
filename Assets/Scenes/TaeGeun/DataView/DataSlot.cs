using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.IO;

public class DataSlot : MonoBehaviour
{
    public GameObject creat; // 플레이어 닉네임 입력 UI
    public TextMeshProUGUI[] slotText; // 슬롯버튼 아래에 존재하는 텍스트들
    public string newPlayerName; // 새로 입력된 플레이어의 닉네임

    bool[] savefile = new bool[3]; // 세이브파일 존재 유무 저장

    void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            if (File.Exists($"PlayerData{i}.json"))
            {
                savefile[i] = true; //해당 슬롯 번호의 불 배열 트루로 변환

                DataManager.Instance.nowSlot = i; //선택한 슬롯 번호 저장
                DataManager.Instance.LoadJson("PlayerData" + DataManager.Instance.nowSlot.ToString() + ".json"); //해당 슬롯 데이터 불러오기

                // Hp, 맵 아이콘 + 저장 시간 표시
                string saveTime = DataManager.Instance.pd[0].saveTime;
                string saveHP = DataManager.Instance.pd[0].MaxHP.ToString();
                slotText[i].text = $"체력 : {saveHP} 현재 위치 : \n저장 시간: {saveTime}";
            }
            else // 데이터가 없는 경우
            {
                slotText[i].text = $"비어있음";
            }
        }
    }

    public void Slot(int number) // 슬롯의 기능 구현
    {
        DataManager.Instance.nowSlot = number; // 슬롯의 번호를 슬롯번호로 입력함

        if (savefile[number]) //불 배열에서 현재 슬롯번호가 트루면 = 데이터가 존재
        {
            DataManager.Instance.LoadJson("PlayerData" + DataManager.Instance.nowSlot.ToString() + ".json");
            StartGame();
        }
        else
        {
            Creat(); // 닉네임 입력 UI 활성화
        }
    }

    public void Creat() // 닉네임 입력 UI를 활성화하는 메소드
    {
        creat.gameObject.SetActive(true);
    }

    public void StartGame() // 게임 씬 이동
    {
        Debug.Log(DataManager.Instance.nowSlot + " <<<<<<<<<<<<<<<<<<<< ");
        if (!savefile[DataManager.Instance.nowSlot]) // 현재 슬롯 번호의 데이터가 없다면
        {
           
        }
        SceneManager.LoadScene("PlayerDataManager"); // 게임 씬 이동
    }
}
