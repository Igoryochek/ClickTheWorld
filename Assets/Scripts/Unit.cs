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
    [SerializeField] private AudioClip _sound;
    [SerializeField] private long _price = 1000;
    [SerializeField] protected long _coins = 1;

    private int _timeBeforeCoinSpawn = 5;
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

    public long Price => _price;
    public int UnitNumber => _unitNumber;
    public string Name => _name;
    public Sprite Icon => _icon;
    public int UnitLevel => _unitLevel;
    public bool IsFirstTime => _isFirstTime;
    public bool IsUnitPressed =>_isUnitPressed;



    private void OnEnable()
    {
        if (_isCreated == true)
        {
            if (TryGetComponent(out BubbleUnit bubble) == false)
            {
                _bubbleSpawn = StartCoroutine(CreateConstantCoins());
            }
        }
        else
        {
            _isCreated = true;
        }
    }
    private void OnDisable()
    {
        if (_bubbleSpawn != null)
        {
            StopCoroutine(_bubbleSpawn);
        }
    }
    private void Awake()
    {
        _unitSpawner = FindObjectOfType<UnitSpawner>();
        _coinSpawner = FindObjectOfType<CoinSpawner>();
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
            _coinSpawner.IncreaseCoinCount(_coins);
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

    private IEnumerator CreateConstantCoins()
    {
        WaitForSeconds timeBeforeCoinSpawn= new WaitForSeconds(_timeBeforeCoinSpawn);

        while (true)
        {
            CreateCoin();
            _coinSpawner.IncreaseCoinCount(_coins);
            yield return timeBeforeCoinSpawn ;
            yield return null;
        }
    }

    protected void OnChangePrefab(GameObject other)
    {
        _unitSpawner.ChangePrefab(other,_nextPrefab);
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
