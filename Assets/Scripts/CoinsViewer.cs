using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinsViewer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _coinsCountText;

    private long _coinsCount;

    public long CoinsCount => _coinsCount;

    public void ChangeCoinCount(long count)
    {
        _coinsCount += count;
        count = _coinsCount;
        long kiloCount = count / 1000;
        long lostKiloCount = count % 1000 / 100;
        long megaCount = count / 1000000;
        long lostMegaCount = count % 1000000 / 100000;
        if (count / 1000 > 0)
        {
            if (count / 1000000 > 0)
            {
                _coinsCountText.text = megaCount.ToString() + "." + lostMegaCount.ToString() + "M";
            }
            else
            {
                _coinsCountText.text = kiloCount.ToString() + "." + lostKiloCount.ToString() + "K";
            }
        }
        else
        {
            _coinsCountText.text = count.ToString();
        }
    }
}
