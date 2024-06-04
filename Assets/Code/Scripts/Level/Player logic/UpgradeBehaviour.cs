using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeBehaviour : MonoBehaviour
{
    [SerializeField] Upgrade upgradeData;
    [SerializeField] UpgradeInLevelSO upgradesPlayerHas;
    [SerializeField] protected PlayerStatsSO inCombatPlayerStatsSO;

    int upgradeLevel = 0;

    public bool HasUpgrade()
    {
        foreach(Upgrade upgrade in upgradesPlayerHas.UpgradeList)
        {
            if(upgradeData == upgrade)
            {
                return true;
            }
        }
        return false;
    }

    public void ResetUpgradeLevel()
    {
        upgradeLevel = 0;
    }

    public void LevelUpUpgrade()
    {
        upgradeLevel++;
    }
}
