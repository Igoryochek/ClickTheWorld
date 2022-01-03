using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeCounter : MonoBehaviour
{
    [SerializeField] private BubbleSpawnTimeViewer _bubbleSpawnTimeViewer;

    private TextMeshProUGUI _text;

    private void OnEnable()
    {
        _bubbleSpawnTimeViewer.BubbleCreated += OnBubbleCreated;
    }

    private void OnDisable()
    {
        _bubbleSpawnTimeViewer.BubbleCreated -= OnBubbleCreated;

    }
    private void Awake()
    {
        _text = _bubbleSpawnTimeViewer.SecondCountText;
    }

    private void OnBubbleCreated(int count)
    {
        StartCoroutine(StartCounter(count));
    }

    private IEnumerator StartCounter(int count)
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(1);
        while (count > 0)
        {
            count--;
            ShowInfo(count);
            yield return waitForSeconds;
            yield return null;
        }
    }

    private void ShowInfo(int count)
    {
        int hours = count / 3600;
        int minutes = count > 3600 ? (count % 3600) / 60 : count / 60;
        int seconds = count > 3600 ? (count % 3600) % 60 : count % 60;
        _text.text = count > 3600 ? string.Format("{00:00}:{1:00}:{2:00}", hours, minutes, seconds) : string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
