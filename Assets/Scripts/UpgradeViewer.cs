using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeViewer : Viewer
{
    [SerializeField] protected Area _area;
    [SerializeField] protected CoinSpawner _coinsSpawner;
    [SerializeField] private long _price;
    [SerializeField] private string _description;

    private int _number;

    public int Number => _number;
    public long Price => _price;


    private void Start()
    {
        _number = _area.Number;
        ShowInfo(_price, _description);
    }

    protected void OnUpgradeButtonClick()
    {
        if (_coinsSpawner.CoinsCount > _price)
        {
            _coinsSpawner.DecreaseCoinCount(_price);
            ChangePrice();
        }
    }

    public void ChangePrice()
    {
        _price *= 3;
        ShowInfo(_price, _description);
    }

    public void ChangeTimeBetweenSpawn()
    {
        if (_area.TimeBetweenSpawnBubble > _area.MinimumTimeBeforeSpawn)
        {
            _area.ChangeTimeBetweenSpawn();
            OnUpgradeButtonClick();
        }
    }
}
