using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class BossSearchingUI : MonoBehaviour
{
    [Header("보스를 찾는데 걸릴 시간"), Space(.5f)]
    public float searchingTime;

    [Header("미네랄 인덱스"), Space(.5f)]
    public int mineralInex;

    [Header("가스 인덱스"), Space(.5f)]
    public int gasIndex;

    [Header("보스를 찾는데 필요한 미네랄 갯수"), Space(.5f)]
    public int mineralAmount;

    [Header("보스를 찾는데 필요한 가스 갯수"), Space(.5f)]
    public int gasAmount;

    [Header("재료가 부족한데 찾으려하면 띄울 화면"), Space(.5f)]
    [SerializeField] GameObject ItemLessPanel;

    [Header("최초에 보스를 찾을지 물어볼 때 띄울 화면"), Space(.5f)]
    [SerializeField] GameObject AskSearchingScreen;

    [Header("보스를 찾는 중일때 띄울 화면"), Space(.5f)]
    [SerializeField] GameObject SearchingScreen;

    [Header("보스를 다 찾았을 때 띄울 화면"), Space(.5f)]
    [SerializeField] GameObject SearchingCompleteScreen;

    [Header("페이드 매니저"), Space(.5f)]
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
        // 여기서 세이브를 한번 쭉 함
        FD.StartFadeOut(2f);
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Boss_Rabbit"); // 이런식으로 넘길건데 잠깐
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
