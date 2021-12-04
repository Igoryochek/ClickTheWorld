using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public class Unit : MonoBehaviour,IPointerDownHandler,IDragHandler,IPointerUpHandler
{
    [SerializeField] private string _name;
    [SerializeField] private int _unitNumber;
    [SerializeField] private Sprite _icon;
    [SerializeField] private GameObject _nextPrefab;
    [SerializeField] private int _unitLevel;
    [SerializeField] private float _timeBetweenChangePosition;
    [SerializeField] private AudioClip _sound;

    private float _randomMoveSpeed;
    private int _coins=10;
    [SerializeField]private int _price=1000;
    private int _timeBeforeCoinSpawn=5;
    private GameObject _currentUnit;
    private Animator _animator;
    private UnitSpawner _spawner;
    private Vector2 _randomPosition;
    private float _changePositionSpeed;
    private Coroutine _changePrefab;
    private AudioSource _audio;
    private bool _isUnitPressed=false;
    private bool _isDrugging=false;
    private bool _hasCollided = false;


    public int Price => _price;
    public int UnitNumber => _unitNumber;
    public string Name => _name;
    public Sprite Icon => _icon;
    public int Unitlevel => _unitLevel;

    private void Start()
    {
        _coins *= _unitNumber;
        _price *= _unitNumber * _unitLevel;

        if (TryGetComponent(out BubbleUnit bubble)==false)
        {
            StartCoroutine(CreateConstantCoins());
        }
        StartCoroutine(ChangePositionRandomly());

        _spawner = FindObjectOfType<UnitSpawner>();
        _animator = GetComponent<Animator>();
        _audio = GetComponent<AudioSource>();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        _isUnitPressed = true;
        if (TryGetComponent(out BubbleUnit bubble))
        {
            StartCoroutine(OnBabbleClick(bubble));
        }
        else
        {
            CreateCoin();
            FindObjectOfType<CoinsViewer>().ChangeCoinCount(_coins);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = newPosition;
        _isDrugging = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isDrugging = false;
        _isUnitPressed = false;
    }

    private void CreateCoin()
    {
        FindObjectOfType<CoinSpawner>().ShowCoin(gameObject.transform.position,_coins);
    }

    public IEnumerator OnBabbleClick(BubbleUnit bubble)
    {
        //GameObject randomUnit = _spawner.RandomUnit(_unitLevel).gameObject;
        _currentUnit = Instantiate(_nextPrefab, gameObject.transform.position, Quaternion.identity);
        FindObjectOfType<UnitSpawner>().AddUnit(_currentUnit);
        FindObjectOfType<UnitSpawner>().RemoveUnit(bubble.gameObject);
        FindObjectOfType<UnitSpawner>().InstantiateUnitViewer(_nextPrefab);

        _audio.PlayOneShot(_sound);
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }

    private IEnumerator ChangePositionRandomly()
    {
        _randomPosition.x = transform.position.x;
        _randomMoveSpeed = Random.Range(0.3f,1.5f);
        while (true)
        {

            if (transform.position.x==_randomPosition.x)
            {
                yield return new WaitForSeconds(_timeBetweenChangePosition);

                float randomXArea1 = Random.Range(_spawner.XBordersArea1.x, _spawner.XBordersArea1.y);
                float randomXArea2 = Random.Range(_spawner.XBordersArea2.x, _spawner.XBordersArea2.y);
                float randomXArea3 = Random.Range(_spawner.XBordersArea3.x, _spawner.XBordersArea3.y);
                float randomXArea4 = Random.Range(_spawner.XBordersArea4.x, _spawner.XBordersArea4.y);
                float randomY = Random.Range(_spawner.YBorders.x, _spawner.YBorders.y);
                if (_unitLevel == 1)
                {
                    _randomPosition = new Vector2(randomXArea1, randomY);
                }
                if (_unitLevel == 2)
                {
                    _randomPosition = new Vector2(randomXArea2, randomY);
                }
                if (_unitLevel == 3)
                {
                    _randomPosition = new Vector2(randomXArea3, randomY);
                }
                if (_unitLevel == 4)
                {
                    _randomPosition = new Vector2(randomXArea4, randomY);
                }
                _animator.SetTrigger("IsStarted");
                yield return new WaitForSeconds(0.7f);
            }
            if (_isUnitPressed)
            {
                yield return new WaitForSeconds(_timeBetweenChangePosition);
            }

            transform.position = Vector2.MoveTowards(transform.position, _randomPosition, _randomMoveSpeed * Time.deltaTime);

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

    protected void OnChangePrefab(GameObject other)
    {
        
            ChangePrefab(other);
       
    }

    private void ChangePrefab(GameObject other)
    {
  
        FindObjectOfType<UnitSpawner>().RemoveUnit(gameObject);
        Destroy(gameObject);
        FindObjectOfType<UnitSpawner>().RemoveUnit(other);
        Destroy(other);
        _currentUnit =Instantiate(_nextPrefab, transform.position, Quaternion.identity);
        FindObjectOfType<UnitSpawner>().AddUnit(_currentUnit);
        FindObjectOfType<UnitSpawner>().InstantiateUnitViewer(_nextPrefab);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Unit unit) && _isDrugging == true)
        {

            if (unit.UnitNumber==_unitNumber&&_hasCollided==false)
            {
                _hasCollided = true;
                OnChangePrefab(collision.gameObject);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Unit unit) &&( _isDrugging == true||_isUnitPressed==true))
        {
            if (unit.UnitNumber == _unitNumber && _hasCollided == false)
            {
                _hasCollided = true;
                OnChangePrefab(collision.gameObject);
            }
        }
    }
}
