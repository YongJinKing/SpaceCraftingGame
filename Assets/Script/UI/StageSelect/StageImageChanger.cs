using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class StageImageChanger : MonoBehaviour
{
    public Sprite[] sprites;
    public bool isClicked;
    public Image StageImage;
    public TMP_Text StageTitle;
    public TMP_Text StageDescription;


    string EarthTitle;
    string EarthDescription;

    string MarsTitle;
    string MarsDescription;

    string JupiterTitle;
    string JupiterDescription;

    string SaturnTitle;
    string SaturnDescription;

    private void Start()
    {
        EarthTitle = "EARTH";
        EarthDescription = "Bunny monster, T1 mineral, T1 Gas";

        MarsTitle = "MARS";
        MarsDescription = "Mars, T2 mineral, T2 Gas";

        JupiterTitle = "JUPITER";
        JupiterDescription = "Jupiter monster, T3 mineral, T3 Gas";

        SaturnTitle = "SATURN";
        SaturnDescription = "Saturn Monster, T4 Mineral, T4 Gas";

        StageTitle.GetComponent<TMP_Text>().text = EarthTitle;
        StageDescription.text = EarthDescription;

    }


    public void BtnClickEarth()
    {
        
        StageImage.GetComponent<Image>().sprite = sprites[0];
        
        StageTitle.GetComponent<TMP_Text>().text = EarthTitle;
        StageDescription.text = EarthDescription;
    }
    public void BtnClickMars()
    {
        StageImage.GetComponent<Image>().sprite = sprites[1];
        StageTitle.text = MarsTitle;
        StageDescription.text = MarsDescription;
    }
    public void BtnClickJupiter()
    {
        StageImage.GetComponent<Image>().sprite = sprites[2];
        StageTitle.text = JupiterTitle;
        StageDescription.text = JupiterDescription;
    }
    public void BtnClickSaturn()
    {
        StageImage.GetComponent<Image>().sprite = sprites[3];
        StageTitle.text = SaturnTitle;
        StageDescription.text = SaturnDescription;
    }
}
