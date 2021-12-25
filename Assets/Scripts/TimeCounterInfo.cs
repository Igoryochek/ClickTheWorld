using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeCounterInfo : MonoBehaviour
{
    [SerializeField] private BubbleSpawnTimeViewer _bubbleSpawnTimeViewer;

    private TextMeshProUGUI _text;

    private void Start()
    {
        _text = _bubbleSpawnTimeViewer.SecondCountText;
    }

    public void ShowInfo()
    {
        int secondsCount = _bubbleSpawnTimeViewer.SecondsCount + _bubbleSpawnTimeViewer.Second;
        int minute = 60;
        int hour = 3600;
        int minutesInSecondsCount = secondsCount / minute;
        int lostSecondsOfMinute = secondsCount % minute;
        int hoursInSecondsCount = secondsCount / hour;
        int lostSecondsOfHour = secondsCount % hour;
        int minutesInLostSecondsOfHour = lostSecondsOfHour / minute;
        int secondsInMinutesOfHour = lostSecondsOfHour % minute;

        if (minutesInSecondsCount >= 1)
        {
            if (hoursInSecondsCount >= 1)
            {
                _text.text =minutesInLostSecondsOfHour / 10 > 1 ? hoursInSecondsCount.ToString() + " : " + minutesInLostSecondsOfHour.ToString() + " : " + secondsInMinutesOfHour.ToString():
                                        hoursInSecondsCount.ToString() + " : 0" + minutesInLostSecondsOfHour.ToString() + " : " + secondsInMinutesOfHour.ToString();
            }
            else
            {
                _text.text= minutesInSecondsCount / 10 < 1? minutesInSecondsCount.ToString() + " : 0" + lostSecondsOfMinute.ToString():
                                       minutesInSecondsCount.ToString() + " : " + lostSecondsOfMinute.ToString();
            }
        }
        else
        {
            _text.text=secondsCount / 10 >= 1 ? _text.text = "0 : " + secondsCount.ToString() : 
                _text.text = "0 : 0" + secondsCount.ToString();
        }
    }
}
