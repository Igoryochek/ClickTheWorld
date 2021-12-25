using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class UnitSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _bubblePrefab;
    [SerializeField] private List<Unit> _prefabs;
    [SerializeField] private Unit _dnaPrefab;
    [SerializeField] private GameObject _unitVierwersContainer;
    [SerializeField] private int _maxBubbleCount;
    [SerializeField] private Vector2 _xBordersArea;
    [SerializeField] private Vector2 _yBorders;
    [SerializeField] private List<GameObject> _areaButtons;
    [SerializeField] private List<GameObject> _bubbleEffects;
    [SerializeField] private List<Area> _areas;
    [SerializeField] private List<ScriptableUnit> _scriptableUnits;
    [SerializeField] private GameObject _unitsPooler;
    [SerializeField] private GameObject _bubblesPooler;
    [SerializeField] private UnitShop _unitShop;

    private List<Unit> _units = new List<Unit>();
    private List<Unit> _bubbles = new List<Unit>();
    private int _sameUnitsCount = 15;
    private float _areaPositionOffset = 0;
    private int _bubblesCount;
    private int _bubbleEffectLifetime = 1;


    public Vector2 XBordersArea => _xBordersArea;

    public Vector2 YBorders => _yBorders;

    public int BubblesCount => _bubblesCount;


    public event UnityAction<int> IsUnitCreated;

    private void Awake()
    {
        for (int i = 0; i < _areas.Count; i++)
        {
            for (int j = 0; j < _areas[i].MaxBubbleCount + 1; j++)
            {
                var bubble = Instantiate(_areas[i].AreasBubblePrefab, _bubblesPooler.transform);
                _bubbles.Add(bubble);
                bubble.gameObject.SetActive(false);
            }
        }
        for (int i = 0; i < _scriptableUnits.Count; i++)
        {
            for (int j = 0; j < _sameUnitsCount; j++)
            {
                var unit = Instantiate(_scriptableUnits[i].Prefab, _unitsPooler.transform);
                _units.Add(unit);
                unit.gameObject.SetActive(false);
            }
        }
    }

    public void InstantiateUnit(Unit unit, Vector3 position)
    {
        Instantiate(unit, position, Quaternion.identity);
        int areaButtonIndex = unit.UnitLevel - 1;
        if (_areaButtons[areaButtonIndex].activeSelf == false)
        {
            if (_areaButtons[0].activeSelf == false)
            {
                _areaButtons[0].SetActive(true);
            }
            _areaButtons[areaButtonIndex].SetActive(true);
            _areas[areaButtonIndex].gameObject.SetActive(true);
        }
    }

    public void InitializeUnit(Unit prefab, bool isNeedRandomPosition, Vector3 position)
    {
        List<Unit> units = new List<Unit>();
        if (prefab.TryGetComponent(out BubbleUnit bubbleUnit))
        {
            units = _bubbles;
        }
        else
        {
            units = _units;
        }


        if (TryGetObject(out Unit unit, prefab.UnitNumber, prefab.UnitLevel, units))
        {
            if (isNeedRandomPosition)
            {
                if (unit.UnitLevel == 1)
                {
                    _areaPositionOffset = 0;
                }
                else
                {
                    _areaPositionOffset += 10 * (unit.UnitLevel - 1);

                }
                float randomXArea = Random.Range(_xBordersArea.x, _xBordersArea.y) + _areaPositionOffset;
                float randomY = Random.Range(_yBorders.x, _yBorders.y);
                unit.transform.position = new Vector2(randomXArea, randomY);
                _areaPositionOffset = 0;
            }
            else
            {
                unit.transform.position = position;
            }
            if (TryGetComponent(out BubbleUnit bubble) == false)
            {
                if (unit.IsFirstTime == true)
                {
                    foreach (var unit2 in _units)
                    {
                        if (unit2.UnitNumber == unit.UnitNumber)
                        {
                            unit2.SetNoFirstTime();
                        }
                    }
                }
                _unitShop.InitializeUnitViewer(unit.UnitNumber);
                unit.ResetBools();
                IsUnitCreated?.Invoke(prefab.UnitNumber);
            }
            else
            {
                _bubblesCount++;
            }
            unit.gameObject.SetActive(true);
        }
    }

    public void ChangePrefab(GameObject other, Unit prefab)
    {
        if (prefab.TryGetComponent(out LastUnitOnlevel lastUnitOnlevel))
        {
            InstantiateUnit(prefab, gameObject.transform.position);
        }
        else
        {
            InitializeUnit(prefab, false, gameObject.transform.position);
        }
        gameObject.SetActive(false);
        other.SetActive(false);
    }

    public Unit RandomUnit(int areaNumber)
    {
        int randomUnitIndex = Random.Range(0, _units.Count);
        Unit randomUnit = _units[randomUnitIndex];
        while (randomUnit.UnitLevel != areaNumber && randomUnit.IsFirstTime == false)
        {
            randomUnitIndex = Random.Range(0, _units.Count);
            randomUnit = _units[randomUnitIndex];
        }
        if (randomUnit.IsFirstTime == false)
        {
            return randomUnit;
        }
        else
        {
            return _dnaPrefab;
        }
    }

    public void RemoveBubble(int unitLevel, Vector3 position)
    {
        _areas[unitLevel - 1].RemoveBubble();
        StartCoroutine(CreateBubbleEffect(position));
    }

    private IEnumerator CreateBubbleEffect(Vector3 position)
    {
        if (TryGetBubbleEffect(out GameObject bubbleEffect))
        {
            bubbleEffect.transform.position = position;
            bubbleEffect.SetActive(true);
            yield return new WaitForSeconds(_bubbleEffectLifetime);
            bubbleEffect.SetActive(false);
        }
    }

    private bool TryGetObject(out Unit unit, int unitNumber, int unitLevel, List<Unit> poolerUnits)
    {
        if (poolerUnits == _bubbles)
        {
            unit = poolerUnits.First(u => u.gameObject.activeSelf == false && u.UnitLevel == unitLevel);
        }
        else
        {
            unit = poolerUnits.First(u => u.gameObject.activeSelf == false && u.UnitNumber == unitNumber);
        }
        return unit != null;
    }

    private bool TryGetBubbleEffect(out GameObject bubbleEffect)
    {
        bubbleEffect = _bubbleEffects.First(b => b.gameObject.activeSelf == false);
        return bubbleEffect != null;
    }
}
