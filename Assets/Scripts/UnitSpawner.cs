using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _bubblePrefab;
    [SerializeField] private List<GameObject> _prefabs;
    [SerializeField] private GameObject _dnaPrefab;
    [SerializeField] private UnitViewer _unitViewer;
    [SerializeField] private GameObject _unitVierwersContainer;
    [SerializeField] private int _timeBetweenSpawn;
    [SerializeField] private int _maxBubbleCount;
    [SerializeField] private Vector2 _xBordersArea1;
    [SerializeField] private Vector2 _xBordersArea2;
    [SerializeField] private Vector2 _xBordersArea3;
    [SerializeField] private Vector2 _xBordersArea4;
    [SerializeField] private Vector2 _yBorders;

    private List<GameObject> _unitViewers=new List<GameObject>();
    private Coroutine _bubbleCoroutine;
    private List<GameObject> _units = new List<GameObject>();
    private List<GameObject> _bubbles = new List<GameObject>();
    private Vector2 _randomPosition;

    public Vector2 XBordersArea1 => _xBordersArea1;
    public Vector2 XBordersArea2 => _xBordersArea2;
    public Vector2 XBordersArea3 => _xBordersArea3;
    public Vector2 XBordersArea4 => _xBordersArea4;
    public Vector2 YBorders => _yBorders;
    public int TimeBetweenSpawn => _timeBetweenSpawn;

    private void Start()
    {
        foreach (var prefab in _prefabs)
        {
            CreateUnitViewer(prefab.GetComponent<Unit>());
        }
    }

    private void Update()
    {
        if (_bubbleCoroutine == null)
        {
            if (_bubbles.Count != _maxBubbleCount)
            {
                _bubbleCoroutine = StartCoroutine(InstantiateBubble());
            }
        }
    }

    private IEnumerator InstantiateBubble()
    {

        while (_bubbles.Count != _maxBubbleCount)
        {
            InstantiateRandomUnit(_bubblePrefab);
            yield return new WaitForSeconds(_timeBetweenSpawn);
        }
        _bubbleCoroutine = null;
    }

    public void InstantiateUnitViewer(GameObject prefab)
    {
       
            if (TryGetObject(out GameObject unitViewer))
            {
                if (unitViewer.GetComponent<UnitViewer>().UnitViewernumber== prefab.GetComponent<Unit>().UnitNumber&&unitViewer.activeSelf==false)
                {
                    unitViewer.SetActive(true);
                }
            }
        
    }

    public void InstantiateRandomUnit(GameObject prefab)
    {

        float randomXArea1 = Random.Range(_xBordersArea1.x, _xBordersArea1.y);
        float randomXArea2 = Random.Range(_xBordersArea2.x, _xBordersArea2.y);
        float randomXArea3 = Random.Range(_xBordersArea3.x, _xBordersArea3.y);
        float randomY = Random.Range(_yBorders.x, _yBorders.y);
        if (prefab.GetComponent<Unit>().Unitlevel == 1)
        {
            _randomPosition = new Vector2(randomXArea1, randomY);
        }
        if (prefab.GetComponent<Unit>().Unitlevel == 2)
        {
            _randomPosition = new Vector2(randomXArea2, randomY);
        }
        if (prefab.GetComponent<Unit>().Unitlevel == 3)
        {
            _randomPosition = new Vector2(randomXArea3, randomY);
        }
        var unit = Instantiate(prefab, _randomPosition, Quaternion.identity);

        AddUnit(unit);

    }

    public GameObject RandomUnit(int areaNumber)
    {

        int randomUnitIndex = Random.Range(0, _prefabs.Count);
        GameObject randomUnit = _prefabs[randomUnitIndex];
        int randomUnitLevel = randomUnit.GetComponent<Unit>().Unitlevel;
        if (randomUnitLevel == areaNumber && randomUnit.TryGetComponent(out BubbleUnit bubble) == false)
        {
            return randomUnit;
        }
        else
        {
            return _dnaPrefab;
        }
    }
    public void ChangeTimeBetweenSpawn()
    {
        _timeBetweenSpawn--;
    }
    public void AddUnit(GameObject unit)
    {
        if (unit.TryGetComponent(out BubbleUnit bubble))
        {
            _bubbles.Add(unit);
        }
        else
        {
            _units.Add(unit);
        }
    }

    public void RemoveUnit(GameObject unit)
    {
        if (unit.TryGetComponent(out BubbleUnit bubble))
        {
            _bubbles.Remove(unit);
        }
        else
        {
            _units.Remove(unit);
        }
    }

    public void CreateUnitViewer(Unit unit)
    {
        var unitViewer= Instantiate(_unitViewer,_unitVierwersContainer.transform);
        unitViewer.RenderContentUnit(unit);
        _unitViewers.Add(unitViewer.gameObject);
        unitViewer.gameObject.SetActive(false);
    }

    private bool TryGetObject(out GameObject unitViewer)
    {
        unitViewer = _unitViewers.First(c => c.activeSelf == false);
        return unitViewer != null;
    }
}
