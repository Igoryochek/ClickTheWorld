using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BubbleSpawner : MonoBehaviour
{
    [SerializeField] private List<Area> _areas;
    [SerializeField] private GameObject _bubbleObjectPooler;
    [SerializeField] private Vector2 _xBordersArea1;
    [SerializeField] private Vector2 _xBordersArea2;
    [SerializeField] private Vector2 _xBordersArea3;
    [SerializeField] private Vector2 _xBordersArea4;
    [SerializeField] private Vector2 _yBorders;

    private int _bubblesCount;
    private List<Unit> _bubbles=new List<Unit>();

    public Vector2 XBordersArea1 => _xBordersArea1;
    public Vector2 XBordersArea2 => _xBordersArea2;
    public Vector2 XBordersArea3 => _xBordersArea3;
    public Vector2 XBordersArea4 => _xBordersArea4;
    public Vector2 YBorders => _yBorders;

    public int BubblesCount=>_bubblesCount;


    private void Awake()
    {
        for (int i = 0; i < _areas.Count; i++)
        {
            for (int j = 0; j < _areas[i].MaxBubbleCount+1; j++)
            {
              var bubble =Instantiate(_areas[i].AreasBubblePrefab,_bubbleObjectPooler.transform);
                _bubbles.Add(bubble);
                bubble.gameObject.SetActive(false);

            }
        }
    }

    public void InitializeBubble(int unitLevel, bool isNeedRandomPosition, Vector3 position)
    {


        if (TryGetObject(out Unit bubble, unitLevel))
        {
            if (isNeedRandomPosition)
            {
                float randomXArea1 = Random.Range(_xBordersArea1.x, _xBordersArea1.y);
                float randomXArea2 = Random.Range(_xBordersArea2.x, _xBordersArea2.y);
                float randomXArea3 = Random.Range(_xBordersArea3.x, _xBordersArea3.y);
                float randomXArea4 = Random.Range(_xBordersArea4.x, _xBordersArea4.y);
                float randomY = Random.Range(_yBorders.x, _yBorders.y);
                if (unitLevel == 1)
                {
                    bubble.transform.position = new Vector2(randomXArea1, randomY);
                }
                if (unitLevel == 2)
                {
                    bubble.transform.position = new Vector2(randomXArea2, randomY);
                }
                if (unitLevel == 3)
                {
                    bubble.transform.position = new Vector2(randomXArea3, randomY);
                }
                if (unitLevel == 4)
                {
                    bubble.transform.position = new Vector2(randomXArea4, randomY);
                }
            }
            else
            {
                bubble.transform.position = position;
            }
            bubble.gameObject.SetActive(true);
            _bubblesCount++;
        }
    }


    private bool TryGetObject(out Unit bubble, int level)
    {
        bubble = _bubbles.First(b => b.gameObject.activeSelf == false && b.Unitlevel == level);
        return bubble != null;
    }
}
