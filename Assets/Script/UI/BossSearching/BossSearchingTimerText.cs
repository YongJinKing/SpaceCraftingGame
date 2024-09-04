using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossSearchingTimerText : MonoBehaviour
{
    public Text timeText;    
    public BossSearchingUI searchingUI;

    // Start is called before the first frame update
    private void OnEnable()
    {
        searchingUI.changeTimerTextAct.AddListener(ChangeTimerText);
    }

    void ChangeTimerText(float time)
    {
        float minuate = time / 60;
        float seconds = time % 60;

        timeText.text = (int)minuate + "Ка " + (int)seconds + "УЪ";
    }
}
