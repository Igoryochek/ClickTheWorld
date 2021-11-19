using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaLoader : MonoBehaviour
{
    [SerializeField] private Vector3 _firstAreaCameraPosition;
    [SerializeField] private Vector3 _secondAreaCameraPosition;
    //[SerializeField] private Vector3 _thirdAreaCameraPosition;
    public void LoadArea(int areaNumber)
    {
        switch (areaNumber)
        {
            case 1:
                Camera.main.transform.position = _firstAreaCameraPosition;
                break;
            case 2:
                Camera.main.transform.position = _secondAreaCameraPosition;
                break;
        }
    }
}
