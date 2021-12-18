using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UnitViewer : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private CoinsViewer _coinsViewer;
    [SerializeField] private Unit _unitPrefab;
    [SerializeField] private UnitSpawner _unitSpawner;

    private int _unitNumber;

    public int UnitNumber => _unitNumber;

    private void Awake()
    {
        _unitNumber = _unitPrefab.UnitNumber;
        RenderContentUnit();
    }

    public void RenderContentUnit()
    {
        long count = _unitPrefab.Price;
        long kiloCount = count / 1000;
        long lostKiloCount = count % 1000 / 100;
        long megaCount = count / 1000000;
        long lostMegaCount = count % 1000000 / 100000;
        long teraCount = count / 1000000000;
        long lostTeraCount = count % 1000000000 / 100000000;
        if (_unitPrefab.Price / 1000 > 0)
        {
            if (count / 1000000 > 0)
            {
                if (count / 1000000000 > 0)
                {
                    _text.text = _unitPrefab.Name + " . Стоимость: " + teraCount.ToString() + "." + lostTeraCount + "T";
                }
                else
                {
                    _text.text = _unitPrefab.Name + " . Стоимость: " + megaCount.ToString() + "." + lostMegaCount + "M";
                }
            }
            else
            {
                _text.text = _unitPrefab.Name + " . Стоимость: " + kiloCount.ToString() + "." + lostKiloCount + "K";
            }
        }
        else
        {
            _text.text = _unitPrefab.Name + " . Стоимость: " + count.ToString();
        }
    }

    public void BuyButtonClick()
    {
        if (_coinsViewer.CoinsCount > _unitPrefab.Price)
        {
            _unitSpawner.InitializeUnit(_unitPrefab, true, gameObject.transform.position);
            _coinsViewer.ChangeCoinCount(-_unitPrefab.Price);
        }
    }
}
