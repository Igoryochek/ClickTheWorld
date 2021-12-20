using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public class Unit : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField] private string _name;
    [SerializeField] private int _unitNumber;
    [SerializeField] private Sprite _icon;
    [SerializeField] private Unit _nextPrefab;
    [SerializeField] protected int _unitLevel;
    [SerializeField] private float _timeBetweenChangePosition;
    [SerializeField] private AudioClip _sound;
    [SerializeField] private long _price = 1000;
    [SerializeField] protected long _coins = 1;

    private float _randomMoveSpeed;
    private int _timeBeforeCoinSpawn = 5;
    private Animator _animator;
    private Vector2 _randomPosition;
    private AudioSource _audio;
    private bool _isUnitPressed = false;
    private bool _isDrugging = false;
    private bool _isFirstTime = true;
    private bool _hasCollided = false;
    private bool _isCreated = false;
    private UnitSpawner _unitSpawner;
    private CoinSpawner _coinSpawner;
    protected long _startCoins;
    private Coroutine _bubbleSpawn;
    private Coroutine _changePosition;
    private float _areaPositionOffset = 0;
    private CoinsViewer _coinsViewer;

    public long Price => _price;
    public int UnitNumber => _unitNumber;
    public string Name => _name;
    public Sprite Icon => _icon;
    public int Unitlevel => _unitLevel;
    public bool IsFirstTime => _isFirstTime;


    private void OnEnable()
    {
        if (_isCreated == true)
        {
            if (TryGetComponent(out BubbleUnit bubble) == false)
            {
                _bubbleSpawn = StartCoroutine(CreateConstantCoins());
            }
            _changePosition = StartCoroutine(ChangePositionRandomly());
        }
        else
        {
            _isCreated = true;
        }
    }
    private void OnDisable()
    {
        if (_bubbleSpawn != null && _changePosition != null)
        {
            StopCoroutine(_bubbleSpawn);
            StopCoroutine(_changePosition);
        }
    }
    private void Awake()
    {
        _unitSpawner = FindObjectOfType<UnitSpawner>();
        _coinsViewer = FindObjectOfType<CoinsViewer>();
        _coinSpawner = FindObjectOfType<CoinSpawner>();
        _animator = GetComponent<Animator>();
        _audio = GetComponent<AudioSource>();
        _startCoins = _coins;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        _isUnitPressed = true;
        if (TryGetComponent(out BubbleUnit bubble))
        {
            OnBabbleClick(bubble);
        }
        else
        {
            CreateCoin();
            _coinsViewer.IncreaseCoinCount(_coins);
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
        _coinSpawner.CreateCoins(gameObject.transform.position, _coins);
    }

    public void OnBabbleClick(BubbleUnit bubble)
    {
        _unitSpawner.InitializeUnit(_nextPrefab, false, gameObject.transform.position);
        _audio.PlayOneShot(_sound);
        _unitSpawner.RemoveBubble(_nextPrefab._unitLevel, gameObject.transform.position);
        gameObject.SetActive(false);
    }

    private IEnumerator ChangePositionRandomly()
    {
        _randomPosition.x = transform.position.x;
        _randomMoveSpeed = Random.Range(0.3f, 1.5f);
        while (true)
        {
            if (transform.position.x == _randomPosition.x)
            {
                yield return new WaitForSeconds(_timeBetweenChangePosition);
                if (_unitLevel == 1)
                {
                    _areaPositionOffset = 0;
                }
                else
                {
                    _areaPositionOffset += 10 * (_unitLevel - 1);

                }
                float randomXArea = Random.Range(_unitSpawner.XBordersArea.x, _unitSpawner.XBordersArea.y) + _areaPositionOffset;
                float randomY = Random.Range(_unitSpawner.YBorders.x, _unitSpawner.YBorders.y);
                _randomPosition = new Vector2(randomXArea, randomY);
                _areaPositionOffset = 0;
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
            _coinsViewer.IncreaseCoinCount(_coins);
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
        if (_nextPrefab.TryGetComponent(out LastUnitOnlevel lastUnitOnlevel))
        {
            _unitSpawner.InstantiateUnit(_nextPrefab, gameObject.transform.position);
        }
        else
        {
            _unitSpawner.InitializeUnit(_nextPrefab, false, gameObject.transform.position);
        }
        gameObject.SetActive(false);
        other.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Unit unit) && _isDrugging == true)
        {
            if (unit.UnitNumber == _unitNumber && _hasCollided == false)
            {
                _hasCollided = true;
                OnChangePrefab(collision.gameObject);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Unit unit) && (_isDrugging == true || _isUnitPressed == true))
        {
            if (unit.UnitNumber == _unitNumber && _hasCollided == false)
            {
                _hasCollided = true;
                OnChangePrefab(collision.gameObject);
            }
        }
    }

    public void ResetBools()
    {
        _hasCollided = false;
        _isDrugging = false;
        _isUnitPressed = false;
    }

    public void SetNoFirstTime()
    {
        _isFirstTime = false;
    }
}
