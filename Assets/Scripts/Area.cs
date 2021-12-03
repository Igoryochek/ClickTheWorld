using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Area : MonoBehaviour
{
    [SerializeField] private int _areaNumber;
    [SerializeField] private GameObject _areasBubblePrefab;

  

    public int AreaNumber => _areaNumber;
    public event UnityAction<GameObject> AreaIsActive;

    private void Start()
    {
        AreaIsActive?.Invoke(_areasBubblePrefab);
    }
}
