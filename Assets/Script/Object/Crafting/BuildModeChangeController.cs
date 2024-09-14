using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BuildModeChangeController : MonoBehaviour
{
    public GameObject craftToggle;
    public Player player;

    // Start is called before the first frame update
    void Start()
    {
        craftToggle.SetActive(false);
        player.PlayerModeChangeEvent.AddListener(SwitchController);
    }


    void SwitchController(bool _toggle)
    {
        craftToggle.SetActive(_toggle);
    }
    
}
