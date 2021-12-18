using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeViewer : MonoBehaviour
{
    [SerializeField] protected Area _area;
    [SerializeField] protected CoinsViewer _coinsViewer;
    [SerializeField] private long _price;
    [SerializeField] private string _description;
    [SerializeField] private TextMeshProUGUI _text;

    private int _number;

    public int Number => _number;
    public long Price => _price;


    private void Start()
    {
        _number = _area.AreaNumber;
        ShowInfo();
    }

    protected void OnUpgradeButtonClick()
    {
        if (_coinsViewer.CoinsCount > _price)
        {
            _coinsViewer.ChangeCoinCount(-_price);
            ChangePrice();
        }
    }

    private void ShowInfo()
    {
        long count = _price;
        long kiloCount = count / 1000;
        long lostKiloCount = count % 1000 / 100;
        long megaCount = count / 1000000;
        long lostMegaCount = count % 1000000 / 100000;
        long teraCount = count / 1000000000;
        long lostTeraCount = count % 1000000000 / 100000000;
        if (count / 1000 > 0)
        {
            if (count / 1000000 > 0)
            {
                if (count / 1000000000 > 0)
                {
                    _text.text = _description + " . Стоимость: " + teraCount.ToString() + "." + lostTeraCount + "T";
                }
                else
                {
                    _text.text = _description + " . Стоимость: " + megaCount.ToString() + "." + lostMegaCount + "M";
                }
            }
            else
            {
                _text.text = _description + " . Стоимость: " + kiloCount.ToString() + "." + lostKiloCount + "K";
            }
        }
        else
        {
            _text.text = _description + " . Стоимость: " + count.ToString();
        }
    }

    public void ChangePrice()
    {
        _price *= 3;
        ShowInfo();
    }
}
