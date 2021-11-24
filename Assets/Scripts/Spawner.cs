using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _bubblePrefab;
    [SerializeField] private Vector2 _xBordersArea1;
    [SerializeField] private Vector2 _xBordersArea2;
    [SerializeField] private Vector2 _yBorders;
    [SerializeField] private int _timeBetweenSpawn;
    [SerializeField] private int _maxBubbleCount;
    [SerializeField] private List <GameObject> _coins;
    [SerializeField] private CoinsViewer _coinsViewer;

    private Coroutine _bubbleCoroutine;
    private Coroutine _coinCoroutine;
    private List<GameObject> _units=new List<GameObject>();
    private List<GameObject> _bubbles=new List<GameObject>();
    private List<GameObject> _coinsList=new List<GameObject>();
    private int _coinLifetime=2;
    private Vector2 _randomPosition;

    private GameObject _bubble;

    public Vector2 XBordersArea1=>_xBordersArea1;
    public Vector2 XBordersArea2=>_xBordersArea2;
    public Vector2 YBorders=>_yBorders;

    private void Start()
    {
        _coinsList = _coins;
    }

    private void Update()
    {
        if (_bubbleCoroutine==null)
        {
            foreach (var bubble in _bubbles)
            {
                if (bubble==null)
                {
                    _bubbles.Remove(bubble);
                    break;
                }
            }
            if (_bubbles.Count!=_maxBubbleCount)
            {
                _bubbleCoroutine = StartCoroutine(InstantiateBubble());
            }
        }
    }

    private IEnumerator InstantiateBubble()
    {
        
        while (_units.Count!=_maxBubbleCount)
        {
            InstantiateUnit(_bubblePrefab);
            yield return new WaitForSeconds(_timeBetweenSpawn);
        }
        _bubbleCoroutine = null;
    }

    public void InstantiateUnit(GameObject prefab)
    {
        
        float randomXArea1 = Random.Range(_xBordersArea1.x, _xBordersArea1.y);
        float randomXArea2 = Random.Range(_xBordersArea2.x, _xBordersArea2.y);
        float randomY = Random.Range(_yBorders.x, _yBorders.y);
        if (prefab.GetComponent<Unit>().Unitlevel == 1)
        {
            _randomPosition = new Vector2(randomXArea1, randomY);
        }
        if (prefab.GetComponent<Unit>().Unitlevel == 2)
        {
            _randomPosition = new Vector2(randomXArea2, randomY);
        }
        var unit = Instantiate(prefab, _randomPosition, Quaternion.identity);
        _units.Add(unit);
    }

    public void AddUnit(GameObject unit)
    {
        _units.Add(unit);
    }

    public void RemoveUnit(GameObject unit)
    {
        _units.Remove(unit);
    }

    private IEnumerator CreateCoin(Vector3 position,int count)
    {
        if (TryGetObject(out GameObject coin))
        {
            coin.transform.position = position;
            coin.GetComponentInChildren<TextMeshProUGUI>().text = count.ToString();
            coin.SetActive(true);
            yield return new WaitForSeconds(_coinLifetime);
            coin.SetActive(false);
        }
       

    }

    private bool TryGetObject(out GameObject coin)
    {
        coin = _coinsList.First(c=>c.activeSelf==false);
        return coin!=null;
    }

    public void ShowCoin(Vector3 position,int count)
    {
        StartCoroutine(CreateCoin(position,count));
    }
}
