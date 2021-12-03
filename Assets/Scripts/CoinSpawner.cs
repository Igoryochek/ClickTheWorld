using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class CoinSpawner : MonoBehaviour
{

    [SerializeField] private List <GameObject> _coins;
    [SerializeField] private CoinsViewer _coinsViewer;

    private Coroutine _coinCoroutine;

    private List<GameObject> _coinsList=new List<GameObject>();
    private int _coinLifetime=2;




    private void Start()
    {
        _coinsList = _coins;
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
