using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class BubbleSpawnTimeViewer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _secondsCountText;
    [SerializeField] private UnitSpawner _unitSpawner;
    [SerializeField] private Area _spawningArea;

    private int _secondsCount;
    private int _second = 1;

    private void Awake()
    {
        _secondsCount = _spawningArea.TimeBetweenSpawnBubble;
    }

    private void OnEnable()
    {
        _spawningArea.AreaIsActive += OnAreaIsActive;
    }

    private void OnDisable()
    {
        _spawningArea.AreaIsActive -= OnAreaIsActive;

    }

    private void OnAreaIsActive(Unit unit, int time)
    {
        StartCoroutine(CountSeconds(unit, time));
    }

    private IEnumerator CountSeconds(Unit unit, int timeBetweenSpawn)
    {
        while (true)
        {
            if (_spawningArea.BubblesCount < _spawningArea.MaxBubbleCount)
            {
                _secondsCount--;
            }

            ShowInfo();
            if (_secondsCount < 0)
            {
                _secondsCount = _spawningArea.TimeBetweenSpawnBubble;
                InitializeBubble(unit);
            }
            yield return new WaitForSeconds(_second);
        }
    }

    public void InitializeBubble(Unit unit)
    {
        if (_spawningArea.BubblesCount <= _spawningArea.MaxBubbleCount)
        {
            _unitSpawner.InitializeUnit(unit, true, gameObject.transform.position);
            _spawningArea.AddBubble(unit.gameObject);
        }
    }

    private void ShowInfo()
    {
        int secondsCount = _secondsCount + _second;
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
                if (minutesInLostSecondsOfHour / 10 > 1)
                {
                    _secondsCountText.text = hoursInSecondsCount.ToString() + " : " + minutesInLostSecondsOfHour.ToString() + " : " + secondsInMinutesOfHour.ToString();
                }
                else
                {
                    _secondsCountText.text = hoursInSecondsCount.ToString() + " : 0" + minutesInLostSecondsOfHour.ToString() + " : " + secondsInMinutesOfHour.ToString();
                }

            }
            else
            {
                if (minutesInSecondsCount / 10 < 1)
                {
                    _secondsCountText.text = minutesInSecondsCount.ToString() + " : 0" + lostSecondsOfMinute.ToString();
                }
                else
                {
                    _secondsCountText.text = minutesInSecondsCount.ToString() + " : " + lostSecondsOfMinute.ToString();
                }
            }
        }
        else
        {
            if (secondsCount / 10 >= 1)
            {
                _secondsCountText.text = "0 : " + secondsCount.ToString();
            }
            else
            {
                _secondsCountText.text = "0 : 0" + secondsCount.ToString();
            }
        }
    }
}
