using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class forEquipTeat : MonoBehaviour
{
    private int mode = 0;
    public PlayerEquipmentManager equipmentManager;
    public Weapon AR;
    public Weapon SG;
    public Weapon SR;
    public Weapon Hammer;
    public Weapon PickAxe;

    public UnityEvent<Equipment> testEvent = new UnityEvent<Equipment>();
    public UnityEvent<int> selectSlotEvent = new UnityEvent<int>();

    private void Start()
    {
        InputController.Instance.numberKeyEvent.AddListener(OnNumInput);
        Player player = equipmentManager.GetComponent<Player>();
        player.UIChangeEvent.AddListener(OnModeChange);
    }

    private void OnModeChange(int i)
    {
        mode = i;
    }

    public void OnNumInput(int i)
    {
        switch (mode)
        {
            case 0:
                WeaponModeProcess(i);
                break;
            case 1:
                BuildModeProcess(i);
                break;
        }

        
    }

    private void WeaponModeProcess(int i)
    {
        switch (i)
        {
            case 1:
                testEvent?.Invoke(AR);
                selectSlotEvent?.Invoke(0);
                break;
            case 2:
                testEvent?.Invoke(SG);
                selectSlotEvent?.Invoke(1);
                break;
            case 3:
                testEvent?.Invoke(SR);
                selectSlotEvent?.Invoke(2);
                break;
        }
    }
    private void BuildModeProcess(int i)
    {
        switch (i)
        {
            case 1:
                testEvent?.Invoke(PickAxe);
                selectSlotEvent?.Invoke(0);
                break;
            case 2:
                testEvent?.Invoke(Hammer);
                selectSlotEvent?.Invoke(1); 
                break;
            case 3:
                break;
        }
    }
}
