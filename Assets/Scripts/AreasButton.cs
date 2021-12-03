using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreasButton : MonoBehaviour
{
    [SerializeField] private GameObject[] _areaButtons;



    public void OnAreasButtonClick()
    {
        foreach (var button in _areaButtons)
        {
            button.SetActive(true);
        }
    }

    public void OnChoosingArea()
    {
        foreach (var button in _areaButtons)
        {
            button.SetActive(false);
        }
    }
}
