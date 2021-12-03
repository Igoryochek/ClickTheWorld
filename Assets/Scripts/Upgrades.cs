using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrades : MonoBehaviour
{
    [SerializeField] private UnitSpawner _spawner;
    [SerializeField] private CoinsViewer _coinsViewer;

    public void OnTimeBetweenSpawnBubbleButtonClick(Upgrade upgrade)
    {
        _spawner.ChangeTimeBetweenSpawn();
        OnUpgradeButtonClick(upgrade);
    }

    public void OnBubbleLevelUpgradeButtonClick(Upgrade upgrade)
    {

        OnUpgradeButtonClick(upgrade);
    }

    private void OnUpgradeButtonClick(Upgrade upgrade)
    {
       
        _coinsViewer.ChangeCoinCount(-upgrade.Price);
        upgrade.ChangePrice();
    }


}
