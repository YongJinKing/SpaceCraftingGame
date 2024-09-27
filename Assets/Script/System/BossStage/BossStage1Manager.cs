using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossStage1Manager : MonoBehaviour
{
    public Transform MortalBoxes;
    public Transform MortalGages;
    public Transform BossRabbit;
    public GameObject playerUI;

    public InputController controller;
    public FadeManager fadeManager;

    public GameObject goBackToSpaceShipButton;
    public TotalSaveManager totalSaveManager;
    public InventorySaveSystem inventorySaveSystem;
    public void StartGame()
    {
        playerUI.SetActive(true);
        MortalBoxes.gameObject.SetActive(true);
        MortalGages.gameObject.SetActive(true);
        controller.canMove = true;
        inventorySaveSystem.LoadInventorySaved();
        BossRabbit.GetComponent<BossReadyState>().StartFight();
        // 이 아래 보스를 위한 UI(ex 체력바)도 키면 됩니다.
    }

    public void TurnOnButton()
    {
        goBackToSpaceShipButton.SetActive(true);
    }

    public void GoBackToSpaceShipe()
    {
        goBackToSpaceShipButton.SetActive(false);
        StartCoroutine(ChangeScene());
    }

    IEnumerator ChangeScene()
    {
        fadeManager.StartFadeOut(2f);
        totalSaveManager.SaveAll();
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("MainStage1");
    }
}
