using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UnitViewer : Viewer
{
    [SerializeField] private Image _icon;
    [SerializeField] private Unit _prefab;
    [SerializeField] private Spawner _spawner;

    private CoinSpawner _coinsSpawner;
    private int _number;

    public int Number => _number;

    private void Awake()
    {
        _coinsSpawner = FindObjectOfType<CoinSpawner>();
        _number = _prefab.Number;
        ShowInfo(_prefab.Price, _prefab.Name);
    }

    public void BuyButtonClick()
    {
        if (_coinsSpawner.CoinsCount > _prefab.Price)
        {
            _spawner.InitializeUnit(_prefab, true, gameObject.transform.position);
            _coinsSpawner.DecreaseCoinCount(_prefab.Price);
        }
    }
}
