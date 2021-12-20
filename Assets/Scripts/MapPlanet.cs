using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPlanet : MonoBehaviour
{
    [SerializeField] private Area _area;
    [SerializeField] private GameObject _gameButtons;

    private void OnMouseDown()
    {
        Camera.main.transform.position = new Vector3(_area.transform.position.x, _area.transform.position.y, Camera.main.transform.position.z);
        _gameButtons.SetActive(true);
    }
}
