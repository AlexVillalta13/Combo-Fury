using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades SO", fileName = "NewUpgradeSO")]
public class Upgrade : ScriptableObject
{
    [SerializeField] string upgradeName;
    public string UpgradeName { get { return upgradeName; } }


    [SerializeField] string upgradeId;
    public string UpgradeId { get { return upgradeId; } }

    [PreviewField]
    [SerializeField] Sprite image;
    public Sprite Image { get { return image; } }


    [SerializeField] int maxLevel;
    public int MaxLevel { get { return maxLevel; } }


    [SerializeField] int levelUnlock;
    public int LevelUnlock { get { return levelUnlock; } }


    [SerializeField] string upgradeDescription;
    public string UpgradeDescription { get { return upgradeDescription; } }


    [SerializeField] GameEvent upgradeEvent;
    public GameEvent UpgradeEvent { get { return upgradeEvent; } }
}
