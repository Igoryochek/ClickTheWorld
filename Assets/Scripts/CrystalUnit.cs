using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CrystalUnit : Unit
{
    private TextMeshProUGUI _crystalCountText;
    private int _crystalCount = 1;

    private void Start()
    {
        _crystalCountText = FindObjectOfType<CrystalCountText>().Text;
    }

    private void Update()
    {
        if (_crystalCountText != null)
        {
            _crystalCountText.transform.position = transform.localPosition;
        }
    }

    public void ChangeCrystalCountText()
    {
        ChangeCrystalCount();
        _crystalCountText.text = _crystalCount.ToString();
    }

    private void ChangeCrystalCount()
    {
        _crystalCount++;
        _coins += _startCoins;
    }
}
