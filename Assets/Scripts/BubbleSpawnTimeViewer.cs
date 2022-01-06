using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class BubbleSpawnTimeViewer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _secondsCountText;
    [SerializeField] private Spawner _spawner;
    [SerializeField] private Area _spawningArea;

    private int _secondsCount;
    private int _second = 1;

    public int SecondsCount => _secondsCount;
    public int Second => _second;
    public TextMeshProUGUI SecondCountText => _secondsCountText;

    public event UnityAction<int> BubbleCreated;

    private void OnEnable()
    {
        _spawningArea.Active += OnAreaIsActive;
    }

    private void OnDisable()
    {
        _spawningArea.Active -= OnAreaIsActive;
    }

    private void Awake()
    {
        _secondsCount = _spawningArea.TimeBetweenSpawnBubble;
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

            if (_secondsCount < 0)
            {
                _secondsCount = _spawningArea.TimeBetweenSpawnBubble;
                InitializeBubble(unit);
            }
            BubbleCreated.Invoke(_secondsCount);
            yield return waitForSeconds;
        }
    }

    public void InitializeBubble(Unit unit)
    {
        if (_spawningArea.BubblesCount <= _spawningArea.MaxBubbleCount)
        {
            _spawner.InitializeUnit(unit, true, gameObject.transform.position);
            _spawningArea.AddBubble(unit.gameObject);
        }
    }
}
