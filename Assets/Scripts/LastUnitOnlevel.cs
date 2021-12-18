using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastUnitOnlevel : MonoBehaviour
{
    [SerializeField] private float _timeBeforeRemove;
    [SerializeField] private float _speed;
    [SerializeField] private Animator _animator;
    [SerializeField] private Unit _prefab;

    private Coroutine _coroutine;
    private Vector3 _newPosition;
    private CrystalUnit _crystal;
    private UnitSpawner _unitSpawner;

    private void Start()
    {
        _crystal = FindObjectOfType<CrystalUnit>();
        _unitSpawner = FindObjectOfType<UnitSpawner>();
    }

    private void Update()
    {
        if (_coroutine == null)
        {
            _coroutine = StartCoroutine(RemoveFromLevel());
        }
    }

    private IEnumerator RemoveFromLevel()
    {
        _animator.SetBool("RemoveFromLevel", true);
        float newPositionX = transform.position.x + 10;
        _newPosition = new Vector3(newPositionX, transform.position.y, transform.position.z);
        float xBorder = transform.position.x + 8;
        while (transform.position != _newPosition && transform.position.x < xBorder)
        {
            transform.position = Vector3.MoveTowards(transform.position, _newPosition, Time.deltaTime * _speed);
            yield return null;
        }

        Unit unit = GetComponent<Unit>();
        if (_crystal != null && unit.UnitNumber == _crystal.UnitNumber)
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
