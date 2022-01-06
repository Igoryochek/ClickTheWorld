using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Spawner : MonoBehaviour
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
    [SerializeField] private List<GameObject> _areas;
    [SerializeField] private List<GameObject> _scriptableUnits;
    [SerializeField] private GameObject _unitsPooler;
    [SerializeField] private GameObject _bubblesPooler;
    [SerializeField] private UnitShop _unitShop;

    private List<Unit> _units = new List<Unit>();
    private List<Unit> _bubbles = new List<Unit>();
    private int _sameUnitsCount = 15;
    private float _areaPositionOffset = 0;
    private int _bubblesCount=15;
    private int _bubbleEffectLifetime = 1;

    public Vector2 XBordersArea => _xBordersArea;

    public Vector2 YBorders => _yBorders;

    public int BubblesCount => _bubblesCount;

    public event UnityAction<int> UnitCreated;
    public event UnityAction<int> BubbleCreated;

    private void Awake()
    {
        CreatePooler(_bubblesCount,_areas);
        CreatePooler(_sameUnitsCount,_scriptableUnits);
    }

    private void CreatePooler(int maxCount,List<GameObject> gameObjects)
    {
        for (int i = 0; i < gameObjects.Count; i++)
        {
            Unit unit = new Unit();

            for (int j = 0; j < maxCount + 1; j++)
            {
                if (gameObjects[i].TryGetComponent(out Area area))
                {
                    unit = Instantiate(area.BubblePrefab, _bubblesPooler.transform);
                    _bubbles.Add(unit);
                }
                else if (gameObjects[i].TryGetComponent(out ScriptableUnit scriptableUnit))
                {
                    unit = Instantiate(scriptableUnit.Prefab, _unitsPooler.transform);
                    _units.Add(unit);
                }
                unit.gameObject.SetActive(false);
            }
        }
    }

    public void InstantiateUnit(Unit unit, Vector3 position)
    {
        Instantiate(unit, position, Quaternion.identity);
        int areaButtonIndex = unit.Level - 1;
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

        if (TryGetObject(out Unit unitPrefab, prefab.Number, prefab.Level, units))
        {
            if (isNeedRandomPosition)
            {
                NeedRandomPosition(unitPrefab);
            }
            else
            {
                unitPrefab.transform.position = position;
            }
            if (TryGetComponent(out BubbleUnit bubble) == false)
            {
                units = _units;
                if (unitPrefab.FirstTime == true)
                {
                    foreach (var unit in _units)
                    {
                        if (unit.Number == unitPrefab.Number)
                        {
                            unit.SetNoFirstTime();
                        }
                    }
                }
                _unitShop.InitializeUnitViewer(unitPrefab.Number);
                unitPrefab.UpdateUnitCondition();
                UnitCreated?.Invoke(prefab.Number);
            }
            else
            {
                units = _bubbles;
                BubbleCreated?.Invoke(prefab.Level);
                _bubblesCount++;
            }
            unitPrefab.gameObject.SetActive(true);
        }
    }

    private void NeedRandomPosition(Unit unit)
    {
        if (unit.Level == 1)
        {
            _areaPositionOffset = 0;
        }
        else
        {
            _areaPositionOffset += 10 * (unit.Level - 1);

        }
        float randomXArea = Random.Range(_xBordersArea.x, _xBordersArea.y) + _areaPositionOffset;
        float randomY = Random.Range(_yBorders.x, _yBorders.y);
        unit.transform.position = new Vector2(randomXArea, randomY);
        _areaPositionOffset = 0;
    }

    public void ChangePrefab(GameObject other, Unit prefab)
    {
        if (prefab.TryGetComponent(out LastUnitOnlevel lastUnitOnlevel))
        {
            InstantiateUnit(prefab, other.gameObject.transform.position);
        }
        else
        {
            InitializeUnit(prefab, false, other.gameObject.transform.position);
        }
    }

    public Unit RandomUnit(int areaNumber)
    {
        int randomUnitIndex = Random.Range(0, _units.Count);
        Unit randomUnit = _units[randomUnitIndex];
        while (randomUnit.Level != areaNumber && randomUnit.FirstTime == false)
        {
            randomUnitIndex = Random.Range(0, _units.Count);
            randomUnit = _units[randomUnitIndex];
        }
        if (randomUnit.FirstTime == false)
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
        _areas[unitLevel - 1].GetComponent<Area>().RemoveBubble();
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
            unit = poolerUnits.First(u => u.gameObject.activeSelf == false && u.Level == unitLevel);
        }
        else
        {
            unit = poolerUnits.First(u => u.gameObject.activeSelf == false && u.Number == unitNumber);
        }
        return unit != null;
    }

    private bool TryGetBubbleEffect(out GameObject bubbleEffect)
    {
        bubbleEffect = _bubbleEffects.First(b => b.gameObject.activeSelf == false);
        return bubbleEffect != null;
    }
}
