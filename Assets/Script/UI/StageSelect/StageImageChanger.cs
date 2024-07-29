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


    string Stage1Title;
    string Stage1Description;

    string Stage2Title;
    string Stage2Description;

    string Stage3Title;
    string Stage3Description;

    string Stage4Title;
    string Stage4Description;

    private void Start()
    {
        Stage1Title = "Bunnster";
        Stage1Description = "Bunny monster, T1 mineral, T1 Gas";

        Stage2Title = "Stage2";
        Stage2Description = "Stage2, T2 mineral, T2 Gas";

        Stage3Title = "Stage3";
        Stage3Description = "Stage3 monster, T3 mineral, T3 Gas";

        Stage4Title = "Stage4";
        Stage4Description = "Stage4 Monster, T4 Mineral, T4 Gas";

        StageTitle.GetComponent<TMP_Text>().text = Stage1Title;
        StageDescription.text = Stage1Description;

    }


    public void BtnClickStage1()
    {
        
        StageImage.GetComponent<Image>().sprite = sprites[0];
        
        StageTitle.GetComponent<TMP_Text>().text = Stage1Title;
        StageDescription.text = Stage1Description;
    }
    public void BtnClickStage2()
    {
        StageImage.GetComponent<Image>().sprite = sprites[1];
        StageTitle.text = Stage2Title;
        StageDescription.text = Stage2Description;
    }
    public void BtnClickStage3()
    {
        StageImage.GetComponent<Image>().sprite = sprites[2];
        StageTitle.text = Stage3Title;
        StageDescription.text = Stage3Description;
    }
    public void BtnClickStage4()
    {
        StageImage.GetComponent<Image>().sprite = sprites[3];
        StageTitle.text = Stage4Title;
        StageDescription.text = Stage4Description;
    }
}
