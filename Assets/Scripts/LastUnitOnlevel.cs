using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastUnitOnlevel : MonoBehaviour
{
    [SerializeField] private float _timeBeforeRemove;
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _prefab;
    [SerializeField] private Vector2 _xBorders;
    [SerializeField] private Vector2 _yBorders;

    private Coroutine _coroutine;

    private void Update()
    {
        if (_coroutine==null)
        {
            StartCoroutine(RemoveFromLevel());
        }
    }

    private IEnumerator RemoveFromLevel()
    {
        _animator.SetBool("RemoveFromLevel", true);
        yield return new WaitForSeconds(_timeBeforeRemove);
        float randomPositionX = Random.Range(_xBorders.x,_xBorders.y);
        float randomPositionY = Random.Range(_yBorders.x,_yBorders.y);
        Vector2 newPosition = new Vector2(randomPositionX,randomPositionY);
        Instantiate(_prefab,newPosition,Quaternion.identity);
        Destroy(gameObject);
    }
}
