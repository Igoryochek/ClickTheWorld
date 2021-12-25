using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Viewer : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI _text;

    private string _priceString;

    protected void ShowInfo(long count, string name = "")
    {
        if (TryGetComponent(out CoinsViewer coinsViewer))
        {
            _priceString = "";
        }
        else
        {
            _priceString = " . Стоимость: ";
        }
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
                _text.text = count / 1000000000 > 0 ?name + _priceString + teraCount.ToString() + "." + lostTeraCount + "T":
                                        name + _priceString + megaCount.ToString() + "." + lostMegaCount + "M";
            }
            else
            {
                _text.text = name + _priceString + kiloCount.ToString() + "." + lostKiloCount + "K";
            }
        }
        else
        {
            _text.text = name + _priceString + count.ToString();
        }
    }
}
