using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildAbleUI : MonoBehaviour
{
    public int mineralIndex;
    public int gasIndex;
    public TextMeshProUGUI dronAmount;
    public TextMeshProUGUI mineralAmount;
    public TextMeshProUGUI gasAmount;
    SpaceShip spacship;

    private void Awake()
    {
        spacship = FindObjectOfType<SpaceShip>();
    }
    private void Start()
    {
        spacship.dronCountChangeAct.AddListener(ChangeDronText);
        Inventory.instance.buildUIChangeAct.AddListener(ChangeResourcesText);
    }
    private void OnEnable()
    {
        dronAmount.text = "x " + spacship.builderDrons.Count.ToString();
        mineralAmount.text = "x " + Inventory.instance.GetItemAmount(mineralIndex).ToString();
        gasAmount.text = "x " + Inventory.instance.GetItemAmount(gasIndex).ToString();
    }

    public void ChangeDronText(int amount)
    {
        dronAmount.text = "x " + amount.ToString();
    }

    public void ChangeResourcesText(int id, int amount)
    {
        if (id == 100000)
        {
            mineralAmount.text = "x " + amount.ToString();
        }
        else if (id == 100001)
        {
            gasAmount.text = "x " + amount.ToString();
        }
    }
}
