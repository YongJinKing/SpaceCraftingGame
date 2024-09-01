using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TestController : MonoBehaviour
{
    public GameObject craftToggle;
    public Weapon AR;
    public Weapon PickAxe;

    public UnityEvent<Equipment> weaponChange = new UnityEvent<Equipment>();

    // Start is called before the first frame update
    void Start()
    {
        craftToggle.SetActive(false);
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
        if (craftToggle.activeSelf) // 건설모드
        {
            weaponChange?.Invoke(PickAxe);
        }
        else
        {
            weaponChange?.Invoke(AR);
        }
    }
}
