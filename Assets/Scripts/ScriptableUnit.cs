using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new UnitInShop", menuName = "UnitsInShop", order = 51)]
public class ScriptableUnit : ScriptableObject
{
    [SerializeField] private string _description;
    [SerializeField] private Unit _prefab;

    public string Description => _description;
    public Unit Prefab => _prefab;

}
