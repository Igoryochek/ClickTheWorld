using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyUnitButton : MonoBehaviour
{
    public void OnBuyButtonClick(Unit unit)
    {
        if (unit.Price<=FindObjectOfType<CoinsViewer>().CoinsCount)
        {
            FindObjectOfType<Spawner>().InstantiateUnit(unit.gameObject);
            FindObjectOfType<CoinsViewer>().ChangeCoinCount(-unit.Price);
        }
    }
}
