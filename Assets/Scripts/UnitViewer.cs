using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UnitViewer : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _text;

    private int _unitViewernumber;

    public int UnitViewernumber => _unitViewernumber;
    public void RenderContentUnit(Unit unit)
    {
        _unitViewernumber = unit.UnitNumber;
        _icon.sprite =unit.Icon;
        _text.text = unit.Name + " . Стоимость: " + unit.Price.ToString();
        if (unit.Price / 1000 > 0)
        {
            if (unit.Price / 1000000 > 0)
            {
                if (unit.Price / 1000000000 > 0)
                {
                    float teraCount = (float)unit.Price / 1000000;
                    _text.text = unit.Name + " . Стоимость: " + teraCount.ToString() + "T";
                }
                else
                {
                    float megaCount = (float)unit.Price / 1000000;
                    _text.text = unit.Name + " . Стоимость: " + megaCount.ToString() + "M";
                }
            }
            else
            {
                float kiloCount = (float)unit.Price / 1000;
                _text.text = unit.Name + " . Стоимость: " + kiloCount.ToString() + "K";
            }
        }
        else
        {
            _text.text = unit.Name + " . Стоимость: " + unit.Price.ToString();

        }
    }
}
