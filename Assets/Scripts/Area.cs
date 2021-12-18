using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Area : MonoBehaviour
{
    [SerializeField] private int _areaNumber;
    [SerializeField] private Image _image;
    [SerializeField] private Sprite _areaSprite;
    [SerializeField] private Unit _areaBubblePrefab;
    [SerializeField] private int _timeBetweenSpawnBubble;
    [SerializeField] private BubbleSpawnTimeViewer _bubbleSpawnTimeViewer;
    [SerializeField] private int _maxBubbleCount;
    [SerializeField] private List<UpgradeViewer> _upgradeViewers;

    private List<GameObject> _bubbles = new List<GameObject>();
    private int _minimumTimeBeforeSpawn;
    private int _minimumTimeBetweenSpawnCoefficient = 3;
    private int _timeBetweenSpawnCoefficient = 10;

    public int AreaNumber => _areaNumber;
    public Image Image => _image;
    public Sprite AreaSprite => _areaSprite;
    public int MaxBubbleCount => _maxBubbleCount;
    public Unit AreasBubblePrefab => _areaBubblePrefab;
    public BubbleSpawnTimeViewer BubbleSpawnTimeViewer => _bubbleSpawnTimeViewer;
    public int TimeBetweenSpawnBubble => _timeBetweenSpawnBubble;
    public int MinimumTimeBeforeSpawn => _minimumTimeBeforeSpawn;

    public int BubblesCount => _bubbles.Count;

    public event UnityAction<Unit, int> AreaIsActive;

    private void Start()
    {
        AreaIsActive?.Invoke(_areaBubblePrefab, _timeBetweenSpawnBubble);
        _minimumTimeBeforeSpawn = _timeBetweenSpawnBubble / _minimumTimeBetweenSpawnCoefficient;
        foreach (var upgradeViewer in _upgradeViewers)
        {
            if (upgradeViewer.gameObject.activeSelf == false)
            {
                upgradeViewer.gameObject.SetActive(true);
            }
        }
    }

    public void ChangeTimeBetweenSpawn()
    {
        _timeBetweenSpawnBubble -= _timeBetweenSpawnBubble / _timeBetweenSpawnCoefficient;
    }

    public void ChangeAreaSprite()
    {
        _image.sprite = _areaSprite;
    }
    public void AddBubble(GameObject unit)
    {
        _bubbles.Add(unit);
    }

    public void RemoveBubble()
    {
        _bubbles.Remove(_bubbles[0]);
    }
}
