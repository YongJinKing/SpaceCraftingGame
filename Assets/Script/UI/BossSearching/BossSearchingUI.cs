using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class BossSearchingUI : MonoBehaviour
{
    [Header("������ ã�µ� �ɸ� �ð�"), Space(.5f)]
    public float searchingTime;

    [Header("�̳׶� �ε���"), Space(.5f)]
    public int mineralInex;

    [Header("���� �ε���"), Space(.5f)]
    public int gasIndex;

    [Header("������ ã�µ� �ʿ��� �̳׶� ����"), Space(.5f)]
    public int mineralAmount;

    [Header("������ ã�µ� �ʿ��� ���� ����"), Space(.5f)]
    public int gasAmount;

    [Header("��ᰡ �����ѵ� ã�����ϸ� ��� ȭ��"), Space(.5f)]
    [SerializeField] GameObject ItemLessPanel;

    [Header("���ʿ� ������ ã���� ��� �� ��� ȭ��"), Space(.5f)]
    [SerializeField] GameObject AskSearchingScreen;

    [Header("������ ã�� ���϶� ��� ȭ��"), Space(.5f)]
    [SerializeField] GameObject SearchingScreen;

    [Header("������ �� ã���� �� ��� ȭ��"), Space(.5f)]
    [SerializeField] GameObject SearchingCompleteScreen;

    [Header("���̵� �Ŵ���"), Space(.5f)]
    public FadeManager FD;

    public UnityEvent<float> changeTimerTextAct;

    public void TryToSearchBoss()
    {
        if(Inventory.instance.GetItemCheck(mineralInex, mineralAmount) && Inventory.instance.GetItemCheck(gasIndex, gasAmount))
        {
            Inventory.instance.UseItem(mineralInex, mineralAmount); Inventory.instance.UseItem(gasIndex, gasAmount);
            AskSearchingScreen.SetActive(false);
            SearchingScreen.SetActive(true);
            StartSearching();
        }
        else
        {
            ItemLessPanel.SetActive(true);
        }
    }

    public void GoToBoss()
    {
        StartCoroutine(JumpToBossScene());
    }

    IEnumerator JumpToBossScene()
    {
        // ���⼭ ���̺긦 �ѹ� �� ��
        FD.StartFadeOut(2f);
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Boss_Rabbit"); // �̷������� �ѱ�ǵ� ���
    }

    void StartSearching()
    {
        StartCoroutine(StartSearchingCoroutine());
    }

    IEnumerator StartSearchingCoroutine()
    {
        float time = searchingTime;

        while(time >= 0f)
        {
            time -= Time.deltaTime;
            changeTimerTextAct?.Invoke(time);
            yield return null;
        }

        SearchingScreen.SetActive(false);
        SearchingCompleteScreen.SetActive(true);
    }

}
