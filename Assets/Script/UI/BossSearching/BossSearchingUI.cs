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

    [Header("�䳢 ����� �ε���"), Space(.5f)]
    public int rabbitIndex;

    [Header("������ ã�µ� �ʿ��� �̳׶� ����"), Space(.5f)]
    public int mineralAmount;

    [Header("������ ã�µ� �ʿ��� ���� ����"), Space(.5f)]
    public int gasAmount;

    [Header("������ ã�µ� �ʿ��� �䳢 ����� ����"), Space(.5f)]
    public int rabbitAmount;

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

    [Header("���� ã�� �� VFX"), Space(.5f)]
    public ParticleSystem FindingVFX;

    [Header("���� ã�� VFX"), Space(.5f)]
    public ParticleSystem FoundVFX;

    public UnityEvent<float> changeTimerTextAct;

    public bool isSearching = false;
    public float searchedTime = 0f;

    public TotalSaveManager TSM;
    private void Start()
    {
        TSM = FindObjectOfType<TotalSaveManager>();
        FindingVFX.Stop();
        FoundVFX.Stop();

        if (isSearching)
        {
            StartSearching();
        }
    }
    public void TryToSearchBoss()
    {
        if(Inventory.instance.GetItemCheck(mineralInex, mineralAmount) && Inventory.instance.GetItemCheck(gasIndex, gasAmount) && Inventory.instance.GetItemCheck(rabbitIndex,rabbitAmount))
        {
            Debug.Log("ã�� ����");
            Inventory.instance.UseItem(mineralInex, mineralAmount); Inventory.instance.UseItem(gasIndex, gasAmount); Inventory.instance.UseItem(rabbitIndex, rabbitAmount);
            StartSearching();
        }
        else
        {
            Debug.Log("��ã��");
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
        if(TSM != null) TSM.SaveAll();
        FD.StartFadeOut(2f);
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Boss_Rabbit"); // �̷������� �ѱ�ǵ� ���
    }

    void StartSearching()
    {
        isSearching = true;
        AskSearchingScreen.SetActive(false);
        SearchingScreen.SetActive(true);
        FindingVFX.Play();
        StartCoroutine(StartSearchingCoroutine());
    }

    IEnumerator StartSearchingCoroutine()
    {
        if(searchedTime == 0f) searchedTime = searchingTime;

        while(searchedTime >= 0f)
        {
            searchedTime -= Time.deltaTime;
            changeTimerTextAct?.Invoke(searchedTime);
            yield return null;
        }

        searchedTime = 0f;
        SearchingScreen.SetActive(false);
        SearchingCompleteScreen.SetActive(true);
        FindingVFX.Stop();
        FoundVFX.Play();
    }

}
