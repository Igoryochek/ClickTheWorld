using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastUnitOnlevel : MonoBehaviour
{
    [SerializeField] private float _timeBeforeRemove;
    [SerializeField] private float _speed;
    [SerializeField] private Animator _animator;
    [SerializeField] private Unit _prefab;

    private Vector3 _newPosition;
    private CrystalUnit _crystal;
    private UnitSpawner _unitSpawner;
    private const string _removeFromLevel = "RemoveFromLevel";

    private void Start()
    {
        _crystal = FindObjectOfType<CrystalUnit>();
        _unitSpawner = FindObjectOfType<UnitSpawner>();
        StartCoroutine(RemoveFromLevel());
    }

    private IEnumerator RemoveFromLevel()
    {
        _animator.SetBool(_removeFromLevel, true);
        float newPositionX = transform.position.x + 10;
        _newPosition = new Vector3(newPositionX, transform.position.y, transform.position.z);
        float xBorder = transform.position.x + 8;
        while (transform.position != _newPosition && transform.position.x < xBorder)
        {
            transform.position = Vector3.MoveTowards(transform.position, _newPosition, Time.deltaTime * _speed);
            yield return null;
        }

        Unit unit = GetComponent<Unit>();
        if (_crystal != null && unit.Number == _crystal.Number)
        {
            _crystal.ChangeCrystalCountText();
        }
        else
        {
            _unitSpawner.InitializeUnit(_prefab, true, gameObject.transform.position);
        }
        Destroy(gameObject);
    }
}
