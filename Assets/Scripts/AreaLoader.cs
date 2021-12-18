using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaLoader : MonoBehaviour
{
    [SerializeField] private Area[] _areas;
    [SerializeField] private GameObject _gameButtons;

    private int _cameraOffsetZ = -10;
    private int _cameraOffsetX = 10;
    private int _firstAreaIndex = 0;
    private float _minimumScale = 0.01f;

    private void Start()
    {
        if (TryGetComponent(out Camera camera))
        {
            LoadArea(_areas[_firstAreaIndex].AreaNumber);
        }
    }

    public void LoadArea(int areaNumber)
    {
        if (_gameButtons.activeSelf == false)
        {
            _gameButtons.SetActive(true);
        }

        foreach (var area in _areas)
        {
            if (area.AreaNumber == areaNumber)
            {
                if (areaNumber == 1)
                {
                    Camera.main.transform.position = new Vector3(0, 0, _cameraOffsetZ);
                }
                else
                {
                    Camera.main.transform.position = new Vector3((areaNumber - 1) * _cameraOffsetX, Camera.main.transform.position.y, _cameraOffsetZ);
                }
                area.ChangeAreaSprite();
                area.Image.color = new Color(area.Image.color.r, area.Image.color.g, area.Image.color.b, 1);
                area.BubbleSpawnTimeViewer.transform.localScale = Vector3.one;
            }
            else
            {
                area.Image.color = new Color(area.Image.color.r, area.Image.color.g, area.Image.color.b, 0);
                area.BubbleSpawnTimeViewer.transform.localScale = new Vector3(_minimumScale, _minimumScale, _minimumScale);
            }
        }
    }
}
