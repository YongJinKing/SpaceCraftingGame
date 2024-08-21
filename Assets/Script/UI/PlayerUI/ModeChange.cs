using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeChange : MonoBehaviour
{
    [SerializeField] private Player myPlayer;
    public GameObject MainButton;
    public GameObject CraftingBox;
    public GameObject WeaponBox;

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
            WeaponBox.SetActive(true);
        }
        else if(type == 1) 
        {
            MainButton.SetActive(true);
            CraftingBox.SetActive(true);
            WeaponBox.SetActive(false);
        }
    }
}
