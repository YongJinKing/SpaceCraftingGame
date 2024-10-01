using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class BuildModeChangeController : MonoBehaviour
{
    public GameObject craftToggle;
    public GameObject invenCanvas;
    public GameObject buildCanvas;
    public Player player;

    // Start is called before the first frame update
    void Start()
    {
        craftToggle.SetActive(false);
        player.PlayerModeChangeEvent.AddListener(SwitchController);
    }

    // <<<<<<<<<<<<<<<<<<<<<<<<<<<<<
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Home))
        {
            Inventory.instance.AddItem(100000,99);
            Inventory.instance.AddItem(100001, 99);
            Inventory.instance.AddItem(200000, 10);
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            if(player.modeType == 1)
            {
                invenCanvas.SetActive(!invenCanvas.activeSelf);
                if(invenCanvas.activeSelf) SoundManager.Instance.PlaySFX(SoundManager.Instance.UISound.OpenTabSFX);
                else SoundManager.Instance.PlaySFX(SoundManager.Instance.UISound.CloseTabSFX);
            }
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            if (player.modeType == 1)
            {
                buildCanvas.SetActive(!buildCanvas.activeSelf);
                if (buildCanvas.activeSelf) SoundManager.Instance.PlaySFX(SoundManager.Instance.UISound.OpenTabSFX);
                else SoundManager.Instance.PlaySFX(SoundManager.Instance.UISound.CloseTabSFX);
            }
        }
    }

    void SwitchController(bool _toggle)
    {
        craftToggle.SetActive(_toggle);
    }
    
}
