using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CrystalCountBar : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private UnitSpawner _spawner;
    [SerializeField] private int _sliderValue = 100;

    public event UnityAction<int> ChangedCrystalCount;

    private void OnEnable()
    {
        _spawner.IsUnitCreated += OnUnitCreated;
    }

    private void OnDisable()
    {
        _spawner.IsUnitCreated -= OnUnitCreated;
    }

    public void OnUnitCreated(int unitNumber)
    {
        _slider.value++;
        if (_slider.value == _sliderValue)
        {
            int count = 1;
            ChangedCrystalCount.Invoke(count);
            _slider.value = 0;
        }
    }
}
