using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaLoader : MonoBehaviour
{
    [SerializeField] private Area[] _areas;
    [SerializeField] private GameObject _gameButtons;

    public void LoadArea(int areaNumber)
    {
        if (_gameButtons.activeSelf==false)
        {
            _gameButtons.SetActive(true);
        }

        foreach (var area in _areas)
        {
            if (area.AreaNumber==areaNumber)
            {
                Camera.main.transform.position = new Vector3(area.transform.position.x,area.transform.position.y,-10);
            }
        }

        

    }
}
