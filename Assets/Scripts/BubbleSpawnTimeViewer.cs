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
    [SerializeField] private TimeCounterInfo _timeCounterInfo;

    private int _secondsCount;
    private int _second = 1;

    public int SecondsCount => _secondsCount;
    public int Second => _second;
    public TextMeshProUGUI SecondCountText => _secondsCountText;

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
        WaitForSeconds waitForSeconds = new WaitForSeconds(_second);

        while (true)
        {
            if (_spawningArea.BubblesCount < _spawningArea.MaxBubbleCount)
            {
                _secondsCount--;
            }

            _timeCounterInfo.ShowInfo();
            if (_secondsCount < 0)
            {
                _secondsCount = _spawningArea.TimeBetweenSpawnBubble;
                InitializeBubble(unit);
            }
            yield return waitForSeconds;
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


}
