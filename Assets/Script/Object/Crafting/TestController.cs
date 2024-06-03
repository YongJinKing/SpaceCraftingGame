using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestController : MonoBehaviour
{
    public GameObject craftToggle;
    public GameObject infoToggle;

    // Start is called before the first frame update
    void Start()
    {
        craftToggle.SetActive(false);
        infoToggle.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            SwitchController();
        }
    }

    void SwitchController()
    {
        craftToggle.SetActive(!craftToggle.activeSelf);
        infoToggle.SetActive(!infoToggle.activeSelf);
    }
}
