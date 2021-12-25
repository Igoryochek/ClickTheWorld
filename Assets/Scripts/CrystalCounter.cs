using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CrystalCounter : MonoBehaviour
{
    [SerializeField] private CrystalCountBar _crystalCountBar;
    [SerializeField] private TextMeshProUGUI _coinsCountText;
    [SerializeField] private SellCrystalUnitViewer _sellCrystalUnitViewer;

    private int _crystalCount;

    public int CrystalCount => _crystalCount;

    private void OnEnable()
    {
        _crystalCountBar.CountChanged += ChangeCrystalCount;
        _sellCrystalUnitViewer.ChangedCrystalCount += ChangeCrystalCount;

    }

    private void OnDisable()
    {
        _crystalCountBar.CountChanged -= ChangeCrystalCount;
        _sellCrystalUnitViewer.ChangedCrystalCount += ChangeCrystalCount;

    }

    private void Start()
    {
        _coinsCountText.text = _crystalCount.ToString();
    }
    private void ChangeCrystalCount(int count)
    {
        _crystalCount += count; ;
        _coinsCountText.text = _crystalCount.ToString();
    }
}
