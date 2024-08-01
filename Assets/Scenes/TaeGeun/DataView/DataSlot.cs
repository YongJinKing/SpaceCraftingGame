using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.IO;

public class DataSlot : MonoBehaviour
{
    public GameObject creat; // �÷��̾� �г��� �Է� UI
    public TextMeshProUGUI[] slotText; // ���Թ�ư �Ʒ��� �����ϴ� �ؽ�Ʈ��
    public TextMeshProUGUI newPlayerName; // ���� �Էµ� �÷��̾��� �г���

    bool[] savefile = new bool[3]; // ���̺����� ���� ���� ����

   void Start()
    {
        for(int i=0; i < 3; i++ )  
        {
            if(File.Exists(DataManager.Instance.savePath + $"{i}"))
            {
                savefile[i] = true; //�ش� ���� ��ȣ�� �� �迭 Ʈ��� ��ȯ

                DataManager.Instance.nowSlot = i; //������ ���� ��ȣ ����
                DataManager.Instance.LoadJson(); //�ش� ���� ������ �ҷ�����
                //slotText[i].text = DataManager.Instance.NowPlayer.name; // ��ư�� �г��� ǥ��
            }
            else // �����Ͱ� ���� ���
            {
                slotText[i].text = "�������";
            }
        }
        DataManager.Instance.DataClear();
    }

    public void Slot(int number) // ������ ��� ����
    {
        DataManager.Instance.nowSlot = number; // ������ ��ȣ�� ���Թ�ȣ�� �Է���

        if (savefile[number]) //�� �迭���� ���� ���Թ�ȣ�� Ʈ��� = �����Ͱ� ����
        {
            DataManager.Instance.LoadJson();
            StartGame();
        }
        else
        {
            Creat(); // �г��� �Է� UI Ȱ��ȭ
        }
    }

    public void Creat() // �г��� �Է� UI�� Ȱ��ȭ�ϴ� �޼ҵ�
    {
        creat.gameObject.SetActive(true);
    }

    public void StartGame() // ���� �� �̵�
    {
        if (!savefile[DataManager.Instance.nowSlot]) // ���� ���� ��ȣ�� �����Ͱ� ���ٸ�
        {
            //DataManager.Instance.NowPlayer.name = newPlayerName.text; // �Է��� �̸��� ����

            DataManager.Instance.SavePlayerInfo(); // ���� ������ ����
        }
        SceneManager.LoadScene("PlayerDataManager"); // ���� �� �̵�
    }
}
