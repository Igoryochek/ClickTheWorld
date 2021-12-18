using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitShop : MonoBehaviour
{
    [SerializeField] private List<UnitViewer> _unitViewers;

    private void Start()
    {
        foreach (var unitViewer in _unitViewers)
        {
            unitViewer.gameObject.SetActive(false);
        }
        gameObject.SetActive(false);
    }

    public void InitializeUnitViewer(int unitNumber)
    {
        foreach (var unitViewer in _unitViewers)
        {
            if (unitViewer.gameObject.activeSelf == false && unitViewer.UnitNumber == unitNumber)
            {
                unitViewer.gameObject.SetActive(true);
            }
        }
    }
}
