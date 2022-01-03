using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    public void ShowInfo(long count)
    {
        long kiloCount = count / 1000;
        long lostKiloCount = count % 1000 / 100;
        long megaCount = count / 1000000;
        long lostMegaCount = count % 1000000 / 100;

        if (count / 1000 > 0)
        {
            _text.text = count / 1000000 > 0 ? megaCount.ToString() + "." + lostMegaCount.ToString() + "M" :
                                kiloCount.ToString() + "." + lostKiloCount.ToString() + "K";
        }
        else
        {
            _text.text = count.ToString();
        }
    }
}
