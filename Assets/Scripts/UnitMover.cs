using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class UnitMover : MonoBehaviour
{
    [SerializeField] private float _timeBetweenChangePosition;
    [SerializeField] private Unit _unit;
    [SerializeField] private float _changePositionTime=0.7f;
    [SerializeField] private float _minimumSpeed=0.3f;
    [SerializeField] private float _maximumSpeed=1.5f;

    private Vector2 _randomPosition;
    private float _randomMoveSpeed;
    private float _areaPositionOffset = 0;
    private Spawner _unitSpawner;
    private Animator _animator;
    private Coroutine _changePositionRandomly;
    private const string IsStarted = "IsStarted";

    private void OnEnable()
    {
        _changePositionRandomly = StartCoroutine(ChangePositionRandomly());
    }

    private void OnDisable()
    {
        StopCoroutine(_changePositionRandomly);
    }
    private void Awake()
    {
        _unitSpawner = FindObjectOfType<Spawner>();
        _animator = gameObject.GetComponent<Animator>();
    }

    private IEnumerator ChangePositionRandomly()
    {
        _randomPosition.x = transform.position.x;
        _randomMoveSpeed = Random.Range(_minimumSpeed, _maximumSpeed);
        while (true)
        {
            WaitForSeconds waitForSeconds = new WaitForSeconds(_changePositionTime);
            WaitForSeconds timeBetweenChangePosition = new WaitForSeconds(_timeBetweenChangePosition);
            if (transform.position.x == _randomPosition.x)
            {
                yield return new WaitForSeconds(_timeBetweenChangePosition);
                _animator.SetTrigger(IsStarted);
                if (_unit.Level == 1)
                {
                    _areaPositionOffset = 0;
                }
                else
                {
                    _areaPositionOffset += 10 * (_unit.Level - 1);

                }
                float randomXArea = Random.Range(_unitSpawner.XBordersArea.x, _unitSpawner.XBordersArea.y) + _areaPositionOffset;
                float randomY = Random.Range(_unitSpawner.YBorders.x, _unitSpawner.YBorders.y);
                _randomPosition = new Vector2(randomXArea, randomY);
                _areaPositionOffset = 0;
                yield return waitForSeconds;
            }
            if (_unit.UnitPressed)
            {
                yield return timeBetweenChangePosition;
            }
            transform.position = Vector2.MoveTowards(transform.position, _randomPosition, _randomMoveSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
