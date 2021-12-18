using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CrystalCountViewer : MonoBehaviour
{
    [SerializeField] private CrystalCountBar _crystalCountBar;
    [SerializeField] private TextMeshProUGUI _coinsCountText;

    private int _crystalCount;

    public int CrystalCount => _crystalCount;

    private void OnEnable()
    {
        _crystalCountBar.ChangeCrystalCount += ChangeCrystalCount;
    }

    private void OnDisable()
    {
        _crystalCountBar.ChangeCrystalCount -= ChangeCrystalCount;
    }

    private void Start()
    {
        _coinsCountText.text = _crystalCount.ToString();
    }
    public void ChangeCrystalCount(int count)
    {
        _crystalCount += count; ;
        _coinsCountText.text = _crystalCount.ToString();
    }
}
