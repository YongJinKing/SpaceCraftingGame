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
    bool gameStarted = false;
    Player player;
    Boss_Rabbit rabbit;
    public void StartGame()
    {
        gameStarted = true;
        playerUI.SetActive(true);
        MortalBoxes.gameObject.SetActive(true);
        MortalGages.gameObject.SetActive(true);
        controller.canMove = true;
        inventorySaveSystem.LoadInventorySaved();
        BossRabbit.GetComponent<BossReadyState>().StartFight();
        // �� �Ʒ� ������ ���� UI(ex ü�¹�)�� Ű�� �˴ϴ�.
    }

    private void Start()
    {
        player = FindObjectOfType<Player>();
        rabbit = BossRabbit.GetComponent<Boss_Rabbit>();
    }
    private void Update()
    {
        if (rabbit[EStat.HP] <= 0f) return;
        if (gameStarted && !controller.canMove && player[EStat.HP] > 0)
        {
            controller.canMove = true;
        }
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
