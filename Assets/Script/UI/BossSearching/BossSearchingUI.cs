using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;

public class BossSearchingUI : MonoBehaviour
{
    [Header("보스를 찾는데 걸릴 시간"), Space(.5f)]
    public float searchingTime;

    [Header("미네랄 인덱스"), Space(.5f)]
    public int mineralInex;

    [Header("가스 인덱스"), Space(.5f)]
    public int gasIndex;

    [Header("토끼 드랍템 인덱스"), Space(.5f)]
    public int rabbitIndex;

    [Header("보스를 찾는데 필요한 미네랄 갯수"), Space(.5f)]
    public int mineralAmount;

    [Header("보스를 찾는데 필요한 가스 갯수"), Space(.5f)]
    public int gasAmount;

    [Header("보스를 찾는데 필요한 토끼 드랍템 갯수"), Space(.5f)]
    public int rabbitAmount;

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

    [Header("보스 찾는 중 VFX"), Space(.5f)]
    public ParticleSystem FindingVFX;

    [Header("보스 찾은 VFX"), Space(.5f)]
    public ParticleSystem FoundVFX;

    [Header("보스 서칭 버튼"), Space(.5f)]
    public Transform searchingButton;

    public UnityEvent<float> changeTimerTextAct;

    public bool isSearching = false;
    public bool isSearched = false;
    public float searchedTime = 0f;

    public TotalSaveManager TSM;
    public Transform SpaceShipCanvas;
    BossClearSaveManager bossClearInfo;
    private void Start()
    {
        TSM = FindObjectOfType<TotalSaveManager>();
        bossClearInfo = FindObjectOfType<BossClearSaveManager>();
        FindingVFX.Stop();
        FoundVFX.Stop();

        if (bossClearInfo.LoadClearInfo())
        {
            searchingButton.GetComponent<Button>().interactable = false;
            return;
        }

        if (isSearching)
        {
            StartSearching();
        }
    }
    public void TryToSearchBoss()
    {
        if(Inventory.instance.GetItemCheck(mineralInex, mineralAmount) && Inventory.instance.GetItemCheck(gasIndex, gasAmount) && Inventory.instance.GetItemCheck(rabbitIndex,rabbitAmount))
        {
            Debug.Log("찾기 시작");
            Inventory.instance.UseItem(mineralInex, mineralAmount); Inventory.instance.UseItem(gasIndex, gasAmount); Inventory.instance.UseItem(rabbitIndex, rabbitAmount);
            StartSearching();
        }
        else
        {
            Debug.Log("못찾음");
            ItemLessPanel.SetActive(true);
        }
    }

    public void GoToBoss()
    {
        StartCoroutine(JumpToBossScene());
    }

    IEnumerator JumpToBossScene()
    {
        FindObjectOfType<InputController>().canMove = false;
        this.transform.GetChild(0).gameObject.SetActive(false);
        // 여기서 세이브를 한번 쭉 함
        if(TSM != null) TSM.SaveAll();
        SpaceShipCanvas.gameObject.SetActive(false);
        FD.StartFadeOut(2f);
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Boss_Rabbit"); // 이런식으로 넘길건데 잠깐
    }

    void StartSearching()
    {
        isSearching = true;
        AskSearchingScreen.SetActive(false);
        SearchingScreen.SetActive(true);
        FindingVFX.Play();
        if (isSearched)
        {
            SearchComplete();
            return;
        }
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

        SearchComplete();
        /*isSearched = true;
        searchedTime = 0f;
        SearchingScreen.SetActive(false);
        SearchingCompleteScreen.SetActive(true);
        FindingVFX.Stop();
        FoundVFX.Play();*/
    }

    void SearchComplete()
    {
        isSearched = true;
        searchedTime = 0f;
        SearchingScreen.SetActive(false);
        SearchingCompleteScreen.SetActive(true);
        FindingVFX.Stop();
        FoundVFX.Play();
    }
}
