using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeViewer : Viewer
{
    [SerializeField] protected Area _area;
    [SerializeField] protected CoinsViewer _coinsViewer;
    [SerializeField] private long _price;
    [SerializeField] private string _description;

    private int _number;

    public int Number => _number;
    public long Price => _price;


    private void Start()
    {
        _number = _area.AreaNumber;
        ShowInfo(_price,_description);
    }

    protected void OnUpgradeButtonClick()
    {
        if (_coinsViewer.CoinsCount > _price)
        {
            _coinsViewer.DecreaseCoinCount(_price);
            ChangePrice();
        }
    }

    public void ChangePrice()
    {
        _price *= 3;
        ShowInfo(_price,_description);
    }
}
