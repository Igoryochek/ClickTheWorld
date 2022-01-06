using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CrystalCounter : MonoBehaviour
{
    [SerializeField] private CrystalCountBar _crystalCountBar;
    [SerializeField] private TextMeshProUGUI _coinsCountText;
    [SerializeField] private SellCrystalUnitViewer _sellCrystalUnitViewer;

    private int _count;

    public int Count => _count;

    private void OnEnable()
    {
        _crystalCountBar.CountChanged += ChangeCount;
        _sellCrystalUnitViewer.CountChanged += ChangeCount;
    }

    private void OnDisable()
    {
        _crystalCountBar.CountChanged -= ChangeCount;
        _sellCrystalUnitViewer.CountChanged -= ChangeCount;
    }

    private void Start()
    {
        _coinsCountText.text = _count.ToString();
    }
    private void ChangeCount(int count)
    {
        _count += count; ;
        _coinsCountText.text = _count.ToString();
    }
}
