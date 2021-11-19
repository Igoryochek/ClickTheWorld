using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Unit : MonoBehaviour
{
    [SerializeField] private string _name;
    [SerializeField] private int _coins;
    [SerializeField] private GameObject _prefab;
    [SerializeField] protected GameObject _nextPrefab;

    private int _timeBeforeCoinSpawn=5;
    private GameObject _currentUnit;
    protected bool _isDrugging=false;

    private void Start()
    {
        if (TryGetComponent(out Bubble bubble)==false)
        {
            StartCoroutine(CreateConstantCoins());
        }
    }

    public void OnMouseDown()
    {
        if (TryGetComponent(out Bubble bubble))
        {
            _currentUnit =Instantiate(_nextPrefab, gameObject.transform.position, Quaternion.identity);
            FindObjectOfType<Spawner>().AddUnit(_currentUnit);
            FindObjectOfType<Spawner>().RemoveUnit(gameObject);
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
