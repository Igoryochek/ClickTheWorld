using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CrystalCountBar : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private Spawner _spawner;
    [SerializeField] private int _sliderValue = 100;

    public event UnityAction<int> CountChanged;

    private void OnEnable()
    {
        _spawner.UnitCreated += OnUnitCreated;
    }

    private void OnDisable()
    {
        _spawner.UnitCreated -= OnUnitCreated;
    }

    private void OnUnitCreated(int unitNumber)
    {
        _slider.value++;
        if (_slider.value == _sliderValue)
        {
            int count = 1;
            CountChanged.Invoke(count);
            _slider.value = 0;
        }
    }
}
