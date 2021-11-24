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
        _coinsCountText.text = _coinsCount.ToString();
    }
}
