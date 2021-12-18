using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleSpawnTimeUpgrade : UpgradeViewer
{
    public void OnTimeBetweenSpawnBubbleButtonClick()
    {
        if (_area.TimeBetweenSpawnBubble > _area.MinimumTimeBeforeSpawn)
        {
            _area.ChangeTimeBetweenSpawn();
            OnUpgradeButtonClick();
        }
    }
}
