using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinsViewer : Viewer
{
    private long _coinsCount;

    public long CoinsCount => _coinsCount;

    public void IncreaseCoinCount(long count)
    {
        _coinsCount += count;
        ShowInfo(_coinsCount);
    }
    public void DecreaseCoinCount(long count)
    {
        _coinsCount -= count;
        ShowInfo(_coinsCount);
    }
}
