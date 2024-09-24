using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.Windows;

public class EquipInven : MonoBehaviour
{
    private int mode = 0;
    private FactoryDIContainer container;
    private int inputWeaponSlot = 0;
    private int inputCraftSlot = 0;

    public PlayerEquipmentManager equipmentManager;
    public Weapon AR;
    public Weapon SG;
    public Weapon SR;
    public Weapon Hammer;
    public Weapon PickAxe;

    public UnityEvent<Equipment> EquipEvent = new UnityEvent<Equipment>();
    public UnityEvent<EEquipmentType> UnEquipEvent = new UnityEvent<EEquipmentType>();
    public UnityEvent<int> selectSlotEvent = new UnityEvent<int>();

    public int[] weapons;

    public EquipInven()
    {
        container = new FactoryDIContainer();
        weapons = new int[3];
    }

    private void Start()
    {
        InputController.Instance.numberKeyEvent.AddListener(OnNumInput);
        Player player = equipmentManager.GetComponent<Player>();
        player.UIChangeEvent.AddListener(OnModeChange);
        
        GunManager weaponManager = FindObjectOfType<GunManager>();
        weapons = weaponManager.LoadGunIndexes();

        for(int i = 0; i < weapons.Length; i++)
        {
            Upgrade(weapons[i]);
        }
        
        EquipEvent.AddListener(equipmentManager.EquipItem);
        UnEquipEvent.AddListener(equipmentManager.UnEquip);

        equipmentManager.equipSuccessEvent.AddListener(OnSuccessEquip);
    }

    private void OnModeChange(int i)
    {
        mode = i;
        OnNumInput(1);
    }

    public void OnSuccessEquip()
    {
        switch (mode)
        {
            case 0:
                selectSlotEvent?.Invoke(inputWeaponSlot - 1);
                break;
            case 1:
                selectSlotEvent?.Invoke(inputCraftSlot - 1);
                break;
        }
        
    }

    public void Upgrade(int itemType, int level)
    {
        int index = (itemType + 1) * 10000 + (level - 1);
        Upgrade(index);
        weapons[itemType] = index;
    }

    public void Upgrade(int index)
    {
        Weapon upgraded = container.itemFac.Create(index).GetComponent<Weapon>();
        upgraded.gameObject.SetActive(false);
        upgraded.transform.SetParent(transform, false);

        switch (index / 10000)
        {
            case 1:
                {
                    UnEquipEvent?.Invoke(AR.itemType);
                    Destroy(AR.gameObject);
                    AR = upgraded;
                    //EquipEvent?.Invoke(AR);
                    //selectSlotEvent?.Invoke(0);
                }
                break;
            case 2:
                {
                    UnEquipEvent?.Invoke(SG.itemType);
                    Destroy(SG.gameObject);
                    SG = upgraded;
                    //EquipEvent?.Invoke(SG);
                    //selectSlotEvent?.Invoke(0);
                }
                break;
            case 3:
                {
                    UnEquipEvent?.Invoke(SR.itemType);
                    Destroy(SR.gameObject);
                    SR = upgraded;
                    //EquipEvent?.Invoke(SR);
                    //selectSlotEvent?.Invoke(0);
                }
                break;
        }
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
        inputWeaponSlot = i;
        switch (i)
        {
            case 1:
                EquipEvent?.Invoke(AR);
                break;
            case 2:
                EquipEvent?.Invoke(SG);
                break;
            case 3:
                EquipEvent?.Invoke(SR);
                break;
        }
    }
    private void BuildModeProcess(int i)
    {
        inputCraftSlot = i;
        switch (i)
        {
            case 1:
                EquipEvent?.Invoke(PickAxe);
                break;
            case 2:
                EquipEvent?.Invoke(Hammer);
                break;
            case 3:
                break;
        }
    }
}
