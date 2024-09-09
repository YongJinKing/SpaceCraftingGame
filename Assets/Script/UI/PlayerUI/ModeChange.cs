using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeChange : MonoBehaviour
{
    [SerializeField] private Player myPlayer;
    private int UIMode = 0;
    private int priviousWeaponSelect = 0;
    private int priviousCraftSelect = 0;
    public GameObject MainButton;
    public GameObject CraftingBox;
    public GameObject CraftingBoxGrid;
    public GameObject WeaponBox;
    public GameObject WeaponBoxGrid;

    // Start is called before the first frame update
    void Start()
    {
        WeaponBox.SetActive(false);
        if (myPlayer == null) 
        {
            myPlayer = FindObjectOfType<Player>();
        }
        myPlayer.UIChangeEvent.AddListener(OnUIModeChange);
    }

    public void OnUIModeChange(int type)
    {
        if (type == 0)
        {
            MainButton.SetActive(false);
            CraftingBox.SetActive(false);
            for(int i = 0; i < CraftingBoxGrid.transform.childCount; i++)
            {
                CraftingBoxGrid.transform.GetChild(i).GetComponent<WeaponSlot>().UnSelect();
            }
            WeaponBox.SetActive(true);
        }
        else if(type == 1) 
        {
            MainButton.SetActive(true);
            CraftingBox.SetActive(true);
            WeaponBox.SetActive(false);

            for (int i = 0; i < WeaponBoxGrid.transform.childCount; i++)
            {
                WeaponBoxGrid.transform.GetChild(i).GetComponent<WeaponSlot>().UnSelect();
            }
        }
        UIMode = type;
    }

    public void OnWeaponSelect(int type)
    {
        switch (UIMode)
        {
            case 0:
                {
                    WeaponBoxGrid.transform.GetChild(priviousWeaponSelect).GetComponent<WeaponSlot>().UnSelect();
                    WeaponBoxGrid.transform.GetChild(type).GetComponent<WeaponSlot>().Select();
                    priviousWeaponSelect = type;
                }
                break;
            case 1:
                {
                    CraftingBoxGrid.transform.GetChild(priviousCraftSelect).GetComponent<WeaponSlot>().UnSelect();
                    CraftingBoxGrid.transform.GetChild(type).GetComponent<WeaponSlot>().Select();
                    priviousCraftSelect = type;
                }
                break;
        }
    }
}
