using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private List<Coin> _coins;
    [SerializeField] private CoinsViewer _coinsViewer;

    private List<Coin> _coinsList = new List<Coin>();
    private int _coinLifetime = 2;
    private long _coinsCount;

    public long CoinsCount => _coinsCount;

    public event UnityAction<long> CountChanged;
    private void Start()
    {
        _coinsList = _coins;
    }

    private IEnumerator CreateCoin(Vector3 position, long count)
    {
        if (TryGetObject(out Coin coin))
        {
            coin.transform.position = position;
            coin.GetComponent<Coin>().ShowInfo(count);
            coin.gameObject.SetActive(true);
            yield return new WaitForSeconds(_coinLifetime);
            coin.gameObject.SetActive(false);
        }
    }

    private bool TryGetObject(out Coin coin)
    {
        coin = _coinsList.First(c => c.gameObject.activeSelf == false);
        return coin != null;
    }

    public void CreateCoins(Vector3 position, long count)
    {
        StartCoroutine(CreateCoin(position, count));
    }

    public void IncreaseCoinCount(long count)
    {
        _coinsCount += count;
        CountChanged.Invoke(_coinsCount);
    }

    public void DecreaseCoinCount(long count)
    {
        _coinsCount -= count;
        CountChanged.Invoke(_coinsCount);

    }
}
