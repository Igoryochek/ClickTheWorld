using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UnitViewer : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Unit _unit;

    private void Start()
    {
        RenderContentUnit();
    }
    private void RenderContentUnit()
    {
        _icon.sprite =_unit.Icon;
        _text.text = _unit.Name + " . Стоимость: " + _unit.Price.ToString();
    }
}
