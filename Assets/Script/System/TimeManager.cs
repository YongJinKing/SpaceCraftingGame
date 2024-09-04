using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class TimeManager : MonoBehaviour
{
    #region Properties
    #region Private
    private int _day;
    private int _hour;
    private int _minute;
    [SerializeField] private TMP_Text timeText;
    [SerializeField] private TMP_Text dayText;
   
    private float _gameDayPerRealMinute;
    private float _gameHourPerRealMinite;
    private float _gameMinutePerRealMinite;
    #endregion
    #region Protected
    #endregion
    #region Public
    public float timeCount = 0;
    /// <summary>
    /// 게임시간 1일이 현실시간으로 몇분인가
    /// </summary>
    public float gameDayPerRealMinite = 60.0f;
    public GameObject Sunimg;
    public GameObject Nightimg;

    public int day
    {
        get { return _day; }
    }
    #endregion
    #region Events
    /// <summary>
    /// day, hour, minute 순서대로
    /// </summary>
    public UnityEvent<int, int, int> timeChangeEvent = new UnityEvent<int, int, int>();
    public UnityEvent dayChangeEvent = new UnityEvent();
    #endregion
    #endregion

    #region Constructor
    #endregion

    #region Methods
    #region Private
    #endregion
    #region Protected
    #endregion
    #region Public
    #endregion
    #endregion

    #region EventHandlers
    
    public void TimeTest(int day, int hour, int minute)
    {
        Debug.Log($"TimeManager.TimeTest day : {day}, hour : {hour}, minute : {minute}");
        timeText.text = hour.ToString("00") + ":" + minute.ToString("00");
        dayText.text = day.ToString("day " + "00");
        if(hour < 8 || hour > 21)
        {
            Sunimg.SetActive(false);
            Nightimg.SetActive(true);
        }
        if (hour > 9 && hour < 21)
        {
            Sunimg.SetActive(true);
            Nightimg.SetActive(false);
        }

    }
    
    #endregion

    #region Coroutines
    private IEnumerator TimeChecking()
    {
        int tempDay = _day;
        int tempHour = _hour;
        int tempMinute = _minute;
        bool isChanged = false;

        float tempTimeCount = 0;
        while (true)
        {
            isChanged = false;

            timeCount += Time.deltaTime;
            tempTimeCount = timeCount;

            tempDay = (int)(tempTimeCount / _gameDayPerRealMinute);
            tempTimeCount -= _gameDayPerRealMinute * _day;

            if (tempDay != _day)
            {
                _day = tempDay;
                dayChangeEvent?.Invoke();
                isChanged = true;
            }

            tempHour = (int)(tempTimeCount / _gameHourPerRealMinite);
            tempTimeCount -= _gameHourPerRealMinite * _hour;

            tempMinute = (int)(tempTimeCount / _gameMinutePerRealMinite);

            if(tempMinute != _minute)
            {
                _hour = tempHour;
                _minute = tempMinute;
                isChanged = true;
            }

            if (isChanged)
            {
                timeChangeEvent?.Invoke(_day, _hour, _minute);
            }




            yield return null;
        }
    }
    #endregion

    #region MonoBehaviour
    private void Start()
    {
        _gameDayPerRealMinute = gameDayPerRealMinite * 60;
        _gameHourPerRealMinite = _gameDayPerRealMinute / 24.0f;
        _gameMinutePerRealMinite = _gameHourPerRealMinite / 60.0f;

        StartCoroutine(TimeChecking());
    }
    #endregion
}
