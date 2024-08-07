using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class forEquipTeat : MonoBehaviour
{
    public PlayerEquipmentManager equipmentManager;
    public Weapon AR;
    public Weapon Hammer;
    public Weapon PickAxe;

    public UnityEvent<Equipment> testEvent = new UnityEvent<Equipment>();

    private void Start()
    {
        InputController.Instance.numberKeyEvent.AddListener(OnNumInput);
    }

    public void OnNumInput(int i)
    {
        switch (i)
        {
            case 1:
                testEvent?.Invoke(AR);
                break;
            case 2:
                testEvent?.Invoke(Hammer);
                break;
            case 3:
                testEvent?.Invoke(PickAxe);
                break;
        }
    }
}
