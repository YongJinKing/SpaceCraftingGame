using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossSearchingExplain : MonoBehaviour
{
    public BossSearchingUI BSU;
    public Text mineralUsses;
    public Text gasUsses;
    public Text rabbitUsses;

    private void OnEnable()
    {
        mineralUsses.text = "x " + BSU.mineralAmount.ToString();
        gasUsses.text = "x " + BSU.gasAmount.ToString();
        rabbitUsses.text = "x " + BSU.rabbitAmount.ToString();
    }
}
