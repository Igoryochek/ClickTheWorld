using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinsViewer : Viewer
{
    [SerializeField] private CoinSpawner _coinSpawner;

    private void OnEnable()
    {
        _coinSpawner.CountChanged += OnCountChanged;
    }

    private void OnDisable()
    {
        _coinSpawner.CountChanged -= OnCountChanged;

    }
    private void OnCountChanged(long coinsCount)
    {
        ShowInfo(coinsCount);
    }
}
