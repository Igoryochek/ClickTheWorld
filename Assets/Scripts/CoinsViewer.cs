using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinsViewer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _coinsCountText;

    private int _coinsCount=0;

    public int CoinsCount => _coinsCount;

    public void ChangeCoinCount(int count)
    {
        _coinsCount += count;
        if (_coinsCount/1000>0)
        {
            if (_coinsCount / 1000000 > 0)
            {
                float megaCount =(float) _coinsCount / 1000000;
                _coinsCountText.text = megaCount.ToString() + "M";
            }
            float kiloCount =(float) _coinsCount / 1000;
            _coinsCountText.text = kiloCount.ToString()+"K";

        }
        else
        {
            _coinsCountText.text = _coinsCount.ToString();

        }
    }
}
