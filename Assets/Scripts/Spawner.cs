using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private Vector2 _xBorders;
    [SerializeField] private Vector2 _yBorders;
    [SerializeField] private int _timeBetweenSpawn;
    [SerializeField] private int _maxBubbleCount;
    [SerializeField] private List <GameObject> _coins;
    [SerializeField] private CoinsViewer _coinsViewer;

    private Coroutine _coroutine;
    private Coroutine _coinCoroutine;
    private List<GameObject> _units=new List<GameObject>();
    private List<GameObject> _bubbles=new List<GameObject>();
    private List<GameObject> _coinsList=new List<GameObject>();
    private int _coinLifetime=2;

    private GameObject _bubble;

    private void Start()
    {
        _coinsList = _coins;
    }

    private void Update()
    {
        if (_coroutine==null)
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
                _coroutine = StartCoroutine(InstantiateUnit());
            }
        }
    }

    private IEnumerator InstantiateUnit()
    {
        
        while (_units.Count!=_maxBubbleCount)
        {
            float randomX = Random.Range(_xBorders.x, _xBorders.y);
            float randomY = Random.Range(_yBorders.x, _yBorders.y);
            Vector2 randomPosition = new Vector2(randomX, randomY);
             _bubble=Instantiate(_prefab,randomPosition,Quaternion.identity);
            _units.Add(_bubble);
            yield return new WaitForSeconds(_timeBetweenSpawn);
        }
        _coroutine = null;
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
