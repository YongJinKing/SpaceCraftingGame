using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeChange : MonoBehaviour
{
    public GameObject MainButton;
    public GameObject CraftingBox;
    public GameObject WeaponBox;

    public int Count;

    // Start is called before the first frame update
    void Start()
    {
        WeaponBox.SetActive(false);
        Count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.B)) 
        {
            if(Count == 0)
            {
                MainButton.SetActive(false);
                CraftingBox.SetActive(false);
                WeaponBox.SetActive(true);
                Count = 1;
            }
            else
            {
                MainButton.SetActive(true);
                CraftingBox.SetActive(true);
                WeaponBox.SetActive(false);
                Count = 0;
            }

        }
    }
}
