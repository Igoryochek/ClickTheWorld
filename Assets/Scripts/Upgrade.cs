using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Upgrade : MonoBehaviour
{
    [SerializeField] private int _price;
    [SerializeField] private string _descripton;
    [SerializeField] private TextMeshProUGUI _text;

    public int Price => _price;

    private void Start()
    {
        ShowInfo();
    }

    private void ShowInfo()
    {
        _text.text = _descripton + ". Стоимость: " + _price.ToString();
    }

    public void ChangePrice()
    {
        _price +=_price;
        ShowInfo();
    }
}
