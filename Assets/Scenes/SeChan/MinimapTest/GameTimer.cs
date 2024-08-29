using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using System.ComponentModel;

public class Timer : MonoBehaviour
{
    [SerializeField] private TMP_Text timeText;
    [SerializeField] private TMP_Text dayText;
    public GameObject dayImg;
    public GameObject NightImg;
    public GameObject TimeManager;


    [SerializeField] private float time;
    [SerializeField] private float curTime;
    [SerializeField] public int timeFast;

    float hours;
    float minute;
    float second;
    int day;
    int SunMoon;

    private void Awake()
    {
        NightImg.SetActive(true);
        dayImg.SetActive(false);

        timeFast = 1;
        day = 0;
        
        time = 0.1f;
        StartCoroutine(StartTimer());

        TimeManager.GetComponent<TimeManager>().TimeTest(0,0,0);
      
    }

    


    IEnumerator StartTimer()
    {
        curTime = time;
        while(curTime > 0)
        {
           

            curTime += Time.deltaTime * timeFast;
            hours = ((int)curTime / 3600) % 24;
            minute = ((int)curTime / 60) % 60;
            second = (int)curTime % 60;
            
            if(hours == 9)
            {
                NightImg.SetActive(false);
                dayImg.SetActive(true);
            }
            else if(hours == 21)
            {
                NightImg.SetActive(true);
                dayImg.SetActive(false);
            }
            day = ((int)curTime / 86400);
            timeText.text = hours.ToString("00") + ":" + minute.ToString("00") + ":" + second.ToString("00");
            dayText.text = day.ToString("day " + "00");
            
            yield return null;


        }
    }
}