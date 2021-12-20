using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UnitViewer : Viewer
{
    [SerializeField] private Image _icon;
    [SerializeField] private CoinsViewer _coinsViewer;
    [SerializeField] private Unit _unitPrefab;
    [SerializeField] private UnitSpawner _unitSpawner;

    private int _unitNumber;

    public int UnitNumber => _unitNumber;

    private void Awake()
    {
        _unitNumber = _unitPrefab.UnitNumber;
        ShowInfo(_unitPrefab.Price,_unitPrefab.Name);
    }

    public void BuyButtonClick()
    {
        if (_coinsViewer.CoinsCount > _unitPrefab.Price)
        {
            _unitSpawner.InitializeUnit(_unitPrefab, true, gameObject.transform.position);
            _coinsViewer.DecreaseCoinCount(_unitPrefab.Price);
        }
    }
}
