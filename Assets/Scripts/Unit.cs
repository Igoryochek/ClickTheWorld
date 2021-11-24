using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Animator))]
public class Unit : MonoBehaviour
{
    [SerializeField] private string _name;
    [SerializeField] private int _coins;
    [SerializeField] private Sprite _icon;
    [SerializeField] private int _price;
    [SerializeField] protected GameObject _nextPrefab;
    [SerializeField] private int _unitLevel;
    [SerializeField] private float _timeBetweenChangePosition;
    [SerializeField] private float _randomMoveSpeed=2;

    private int _timeBeforeCoinSpawn=5;
    private GameObject _currentUnit;
    private Animator _animator;
    private Spawner _spawner;
    private Vector2 _randomPosition;
    private float _changePositionSpeed;
    protected bool _isDrugging=false;

    public int Price => _price;
    public string Name => _name;
    public Sprite Icon => _icon;
    public int Unitlevel => _unitLevel;


    private void Start()
    {
        _spawner = FindObjectOfType<Spawner>();
        _animator = GetComponent<Animator>();
        if (TryGetComponent(out Bubble bubble)==false)
        {
            StartCoroutine(CreateConstantCoins());
            StartCoroutine(ChangePositionRandomly());
        }
    }

    public void OnMouseDown()
    {
        if (TryGetComponent(out Bubble bubble))
        {
            _currentUnit =Instantiate(_nextPrefab, gameObject.transform.position, Quaternion.identity);
            FindObjectOfType<Spawner>().AddUnit(_currentUnit);
            FindObjectOfType<Spawner>().RemoveUnit(bubble.gameObject);
            Destroy(gameObject);
        }
        
        CreateCoin();
        FindObjectOfType<CoinsViewer>().ChangeCoinCount(_coins);

    }

    private void OnMouseDrag()
    {
        Vector2 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = newPosition;
        _isDrugging = true;
    }

    private void OnMouseUp()
    {
        _isDrugging = false;
    }
    
    private void CreateCoin()
    {
        FindObjectOfType<Spawner>().ShowCoin(gameObject.transform.position,_coins);
    }

    private IEnumerator ChangePositionRandomly()
    {
        _randomPosition.x = transform.position.x;

        while (true)
        {
            if (transform.position.x==_randomPosition.x)
            {
                yield return new WaitForSeconds(_timeBetweenChangePosition);
                float randomXArea1 = Random.Range(_spawner.XBordersArea1.x, _spawner.XBordersArea1.y);
                float randomXArea2 = Random.Range(_spawner.XBordersArea2.x, _spawner.XBordersArea2.y);
                float randomY = Random.Range(_spawner.YBorders.x, _spawner.YBorders.y);
                if (_unitLevel == 1)
                {
                    _randomPosition = new Vector2(randomXArea1, randomY);
                }
                if (_unitLevel == 2)
                {
                    _randomPosition = new Vector2(randomXArea2, randomY);
                }
                _animator.SetTrigger("IsStarted");
            }
            
            transform.position = Vector2.MoveTowards(transform.position, _randomPosition, _randomMoveSpeed * Time.deltaTime);

            //yield return new WaitForSeconds(_timeBetweenChangePosition);
            yield return null;
        }
    }

    private IEnumerator CreateConstantCoins()
    {
        while (true)
        {
            CreateCoin();
            FindObjectOfType<CoinsViewer>().ChangeCoinCount(_coins);
            yield return new WaitForSeconds(_timeBeforeCoinSpawn);
            yield return null;
        }
    }

    protected void ChangePrefab(GameObject other)
    {
        FindObjectOfType<Spawner>().RemoveUnit(gameObject);
        FindObjectOfType<Spawner>().RemoveUnit(other);
        Destroy(gameObject);
        Destroy(other);
        _currentUnit =Instantiate(_nextPrefab, transform.position, Quaternion.identity);
        FindObjectOfType<Spawner>().AddUnit(_currentUnit);
    }

}
