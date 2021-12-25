using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class SellCrystalUnitViewer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private string _description;
    [SerializeField] private int _price;
    [SerializeField] private CrystalCounter _crystalCountViewer;
    [SerializeField] private UnitSpawner _unitSpawner;
    [SerializeField] private List<Area> _areas;

    private int _unitsCount;
    private int _unitsCountToSpawn = 10;

    public event UnityAction<int> ChangedCrystalCount;

    private void Start()
    {
        ShowInfo();
    }
    public void OnButtonClick()
    {
        if (_crystalCountViewer.CrystalCount >= _price)
        {
            ChangedCrystalCount.Invoke(-_price);
            OnSellCrystalButtonClick();
        }
    }
    private void ShowInfo()
    {

        int count = _price;
        int kiloCount = count / 1000;
        int lostKiloCount = count % 1000 / 100;
        int megaCount = count / 1000000;
        int lostMegaCount = count % 1000000 / 100000;
        int teraCount = count / 1000000000;
        int lostTeraCount = count % 1000000000 / 100000000;
        if (count / 1000 > 0)
        {
            if (count / 1000000 > 0)
            {
                if (count / 1000000000 > 0)
                {
                    _text.text = _description + " . Стоимость: " + teraCount.ToString() + "." + lostTeraCount + "T" + " кристаллов";
                }
                else
                {
                    _text.text = _description + " . Стоимость: " + megaCount.ToString() + "." + lostMegaCount + "M" + " кристаллов";
                }
            }
            else
            {
                _text.text = _description + " . Стоимость: " + kiloCount.ToString() + "." + lostKiloCount + "K" + " кристаллов";
            }
        }
        else
        {
            _text.text = _description + " . Стоимость: " + count.ToString() + " кристаллов";

        }
    }

    private void OnSellCrystalButtonClick()
    {
        List<Area> activeAreas = new List<Area>();
        List<Unit> tempUnits = new List<Unit>();

        foreach (var area in _areas)
        {
            if (area.gameObject.activeSelf && area.AreaNumber != 4)
            {
                activeAreas.Add(area);
            }
        }

        while (_unitsCount <= _unitsCountToSpawn)
        {
            int randomIndexActiveArea = Random.Range(0, activeAreas.Count);

            for (int i = randomIndexActiveArea; i == randomIndexActiveArea; i++)
            {
                tempUnits.Add(_unitSpawner.RandomUnit(i + 1));
            }
            _unitsCount++;
        }
        _unitsCount = 0;
        foreach (var unit in tempUnits)
        {
            _unitSpawner.InitializeUnit(unit, true, gameObject.transform.position);
        }
    }
}
