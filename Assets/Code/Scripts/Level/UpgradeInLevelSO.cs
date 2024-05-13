using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "Upgrades List SO", fileName = "NewUpgradesListSO")]
public class UpgradeInLevelSO : ScriptableObject
{
    [SerializeField] List<Upgrade> upgradeList = new List<Upgrade>();
    public List<Upgrade> UpgradeList { get { return upgradeList; } }

    public void AddUpgradeToList(Upgrade upgrade)
    {
        upgradeList.Add(upgrade);
    }

    public bool HasUpgrade(string upgradeId)
    {
        foreach (Upgrade upgrade in upgradeList)
        {
            if (upgrade.UpgradeId == upgradeId)
            {
                return true;
            }
        }
        return false;
    }

    public int GetCurrentUpgradeLevel(Upgrade upgrade)
    {
        int currentUpgradeLevel = 0;
        foreach (Upgrade upgradeToCheck in upgradeList)
        {
            if (upgradeToCheck.UpgradeId == upgrade.UpgradeId)
            {
                currentUpgradeLevel++;
            }
        }
        return currentUpgradeLevel;
    }
}
