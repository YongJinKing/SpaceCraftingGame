using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseUI : MonoBehaviour
{
    [SerializeField]
    [Header("º¼·ý Á¶ÀýÃ¢"), Space(.5f)]
    Transform sfxVolumeSettingScreen;

    [SerializeField]
    [Header("Æ©Åä¸®¾óÃ¢"), Space(.5f)]
    Transform tuToScreen;

    bool isPaused;
    // Start is called before the first frame update
    void Start()
    {
        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                TurnOnPauseScreen();
            }
            else
            {
                TurnOffPauseScreen();
            }
        }
    }

    public void TurnOnPauseScreen()
    {
        Time.timeScale = 0f;
        isPaused = true;
        for(int i = 0; i < 2; i++)
        {
            this.gameObject.transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    public void TurnOffPauseScreen()
    {
        Time.timeScale = 1f;
        isPaused = false;

        for (int i = 0; i < this.gameObject.transform.childCount; i++)
        {
            this.gameObject.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    #region Pause Screen Button Events

    public void TurnOnSFXSettingScreen()
    {
        sfxVolumeSettingScreen.gameObject.SetActive(true);
    }

    public void TurnOffSFXSettingScreen()
    {
        sfxVolumeSettingScreen.gameObject.SetActive(false);
    }

    public void SpawnTuToCanvas()
    {
        Instantiate(tuToScreen,null);
        isPaused = false;

        for (int i = 0; i < this.gameObject.transform.childCount; i++)
        {
            this.gameObject.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    #endregion
}
