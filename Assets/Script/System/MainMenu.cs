using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Canvas DataSlotCanavs;
    public void OnClickStart()
    {
        //SceneManager.LoadScene("DataSlots");
        DataSlotCanavs.gameObject.SetActive(true);
        this.gameObject.SetActive(false);
    }

    public void OnClickOption()
    {
        Debug.Log("���� �ɼ�");
    }

    public void OnClickQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
