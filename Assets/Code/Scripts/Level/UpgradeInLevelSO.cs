using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [System.Serializable]
    public class Upgrade
    {
        [SerializeField] string upgradeName;
        public string UpgradeName { get { return upgradeName; } }

        [SerializeField] string upgradeId;
        public string UpgradeId { get { return upgradeId; } }

        [SerializeField] Sprite image;
        public Sprite Image { get { return image; } }

        [SerializeField] bool canRepeat;
        public bool CanRepeat { get { return canRepeat; } }

        [SerializeField] int levelUnlock;
        public int LevelUnlock { get { return levelUnlock; } }

        [SerializeField] string upgradeDescription;
        public string UpgradeDescription { get { return upgradeDescription; } }

        [SerializeField] GameEvent getUpgradeEvent;
        public GameEvent UpgradeEvent { get { return getUpgradeEvent; } }
    }
}
