using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[RequireComponent(typeof(AudioSource))]
public class Unit : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField] private string _name;
    [SerializeField] private int _number;
    [SerializeField] private Sprite _icon;
    [SerializeField] private Unit _nextPrefab;
    [SerializeField] protected int _level;
    [SerializeField] private AudioClip _sound;
    [SerializeField] private long _price = 1000;
    [SerializeField] protected long _coins = 1;

    private int _timeBeforeCoinSpawn = 5;
    private AudioSource _audio;
    private bool _unitPressed = false;
    private bool _isDrugging = false;
    private bool _firstTime = true;
    private bool _hasCollided = false;
    private bool _isCreated = false;
    private Spawner _spawner;
    private CoinSpawner _coinSpawner;
    protected long _startCoins;
    private Coroutine _bubbleSpawn;

    public long Price => _price;
    public int Number => _number;
    public string Name => _name;
    public Sprite Icon => _icon;
    public int Level => _level;
    public bool FirstTime => _firstTime;
    public bool UnitPressed => _unitPressed;



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
        _spawner = FindObjectOfType<Spawner>();
        _coinSpawner = FindObjectOfType<CoinSpawner>();
        _audio = GetComponent<AudioSource>();
        _startCoins = _coins;
    }

    private void CreateCoin()
    {
        _coinSpawner.CreateCoins(gameObject.transform.position, _coins);
    }

    private void OnBabbleClick(BubbleUnit bubble)
    {
        _spawner.InitializeUnit(_nextPrefab, false, gameObject.transform.position);
        _audio.PlayOneShot(_sound);
        _spawner.RemoveBubble(_level, gameObject.transform.position);
        gameObject.SetActive(false);
    }

    private IEnumerator CreateConstantCoins()
    {
        WaitForSeconds timeBeforeCoinSpawn = new WaitForSeconds(_timeBeforeCoinSpawn);

        while (true)
        {
            CreateCoin();
            _coinSpawner.IncreaseCoinCount(_coins);
            yield return timeBeforeCoinSpawn;
            yield return null;
        }
    }

    private void OnChangePrefab(GameObject other)
    {
        _spawner.ChangePrefab(other, _nextPrefab);
        gameObject.SetActive(false);
        other.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Unit unit) && _isDrugging == true)
        {
            if (unit.Number == _number && _hasCollided == false)
            {
                _hasCollided = true;
                OnChangePrefab(collision.gameObject);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Unit unit) && (_isDrugging == true || _unitPressed == true))
        {
            if (unit.Number == _number && _hasCollided == false)
            {
                _hasCollided = true;
                OnChangePrefab(collision.gameObject);
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _unitPressed = true;
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
        _unitPressed = false;
    }

    public void UpdateUnitCondition()
    {
        _hasCollided = false;
        _isDrugging = false;
        _unitPressed = false;
    }

    public void SetNoFirstTime()
    {
        _firstTime = false;
    }
}
