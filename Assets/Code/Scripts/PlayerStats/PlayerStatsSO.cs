using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "PlayerStatsSO", fileName = "New Player Stats SO")]
public class PlayerStatsSO : ScriptableObject
{
    [Title("Player Base Stats")]
    public float MaxHealth = 100f;
    public float CurrentHealth = 100f;
    public bool PlayerIsAlive()
    {
        return CurrentHealth > 0;
    }
    public float Attack = 10f;
    public float MinAttack = 3f;
    public float MaxAttack = 7f;

    public float Defense = 0f;
    public float CriticalAttackChance = 20f;
    public float DodgeChance = 0f;
    public int comboCount = 0;
    
    [Title("Player Bricks")]
    [SerializeField] List<BrickProbability> brickProbabilityList;
    public List<BrickProbability> BrickProbabilityList => brickProbabilityList;
    
    [Title("Spawn Time Player Bricks")]
    [SerializeField] private float minTimeToSpawnPlayerBrick = 0.5f;
    public float MinTimeToSpawnPlayerBrick => minTimeToSpawnPlayerBrick;
    [SerializeField] private float maxTimeToSpawnPlayerBrick = 1.5f;
    public float MaxTimeToSpawnPlayerBrick => maxTimeToSpawnPlayerBrick;
    [SerializeField] private int maxSimultaneousPlayerBricks = 6;
    public int MaxSimultaneousPlayerBricks => maxSimultaneousPlayerBricks;

    [Title("Player constant Upgrade")]
    [Title("Health", titleAlignment: TitleAlignments. Centered, horizontalLine: false, bold: false)]
    public float maxHealthLevel = 0;
    [SerializeField] float maxHealthIncreasePerLevel = 10f;
    public float MaxHealthIncreasePerLevel => maxHealthIncreasePerLevel;

    [Title("Min Attack", titleAlignment: TitleAlignments. Centered, horizontalLine: false, bold: false)]
    public int minAttackLevel = 0;
    public float MinAttackLittleIncrement = 3f;
    [SerializeField] private float constToIncreaseMinLittleIncrement = 1f;
    public float ConstToIncreaseMinLittleIncrement => constToIncreaseMinLittleIncrement;
    
    public float MinAttackBigIncrement = 6f;
    [SerializeField] private float constToIncreaseMinBigIncrement = 4f;
    public float ConstToIncreaseMinBigIncrement => constToIncreaseMinBigIncrement;
    
    [Title("Max Attack", titleAlignment: TitleAlignments. Centered, horizontalLine: false, bold: false)]
    public int maxAttackLevel = 0;
    public float MaxAttackLittleIncrement = 8f;
    [SerializeField] private float constToIncreaseMaxLittleIncrement = 2f;
    public float ConstToIncreaseMaxLittleIncrement => constToIncreaseMaxLittleIncrement;
    
    public float MaxAttackBigIncrement = 15f;
    [SerializeField] private float constToIncreaseMaxBigIncrement = 5f;
    public float ConstToIncreaseMaxBigIncrement => constToIncreaseMaxBigIncrement;


    [Title("Player Percent Stats Increments")]
    [SerializeField] float healPercentage = 25f;
    public float HealPercentage { get { return healPercentage; } }

    [SerializeField] float littleAttackIncreasePercentage = 15f;
    public float LittleAttackIncreasePercentage { get {  return littleAttackIncreasePercentage; } }

    [SerializeField] float mediumAttackIncreasePercentage = 30f;
    public float MediumAttackIncreasePercentage { get { return mediumAttackIncreasePercentage; } }

    [SerializeField] float mediumDefenseIncreasePercentage = 10f;
    public float MediumDefenseIncreasePercentage { get { return mediumDefenseIncreasePercentage; } }

    [SerializeField] float maxHealthIncreasePercentage = 10f;
    public float MaxHealthIncreasePercentage { get { return maxHealthIncreasePercentage; } }

    [SerializeField] float criticalChanceIncrease = 5f;
    public float CriticalChanceIncrease { get {  return criticalChanceIncrease; } }

    [Title("Special Effects Upgrades")]
    [SerializeField] float adrenalineDogdeChance = 30f;
    public float AdrenalineDodgeChance { get { return adrenalineDogdeChance; } }

    [SerializeField] float revengePercentageIncrease = 100f;
    public float RevengePercentageIncrease {  get { return revengePercentageIncrease; } }

    [SerializeField] float healthPercentageToActivateRage = 30f;
    public float HealthPercentageToActivateRage { get { return healthPercentageToActivateRage; } }

    [SerializeField] float extraRageAttack = 20f;
    public float ExtraRageAttack { get { return extraRageAttack; } }

    [SerializeField] float spinesPercentageDamage = 25f;
    public float SpinesPercentageDamage { get {  return spinesPercentageDamage; } }


    [Title("Heavy Attack Upgrade Stats")]
    public int HeavyAttackLevel = 0;
    public float HeavyAttackPercentageDamage = 250f;


    [Title("Fire Upgrade Stats")]
    [SerializeField] int fireLevel = 0;

    [SerializeField] float firePercentageDamage = 0f;
    public float FirePercentageDamage { get { return firePercentageDamage; } }

    [SerializeField] float firePercentageDamageIncrement = 10f;
    public float FirePercentageDamageIncrement { get { return firePercentageDamageIncrement; } }

    [SerializeField] float fireChanceIncrement = 10f;

    [SerializeField] float timeToDoDamageFire = 2f;
    public float TimeToDoDamageFire { get { return timeToDoDamageFire; } }

    [SerializeField] float timeToTurnOffFire = 10f;
    public float TimeToTurnOffFire { get { return timeToTurnOffFire; } }

    public BrickTypeEnum GetRandomPlayerBrick()
    {
        float randomNumber = UnityEngine.Random.Range(0f, 100f);
        float rangeNumberToSpawn = 0f;
        foreach (BrickProbability brickProbability in brickProbabilityList)
        {
            if (rangeNumberToSpawn < randomNumber && (rangeNumberToSpawn + brickProbability.Probability) > randomNumber)
            {
                return brickProbability.BrickType;
            }
            rangeNumberToSpawn += brickProbability.Probability;
        }

        Debug.LogError("LevelSo: No random Enemy brick selected");
        return BrickTypeEnum.YellowBrick;
    }

    public void StartGame(PlayerStatsSO permanentStatsSO)
    {
        this.MaxHealth = permanentStatsSO.MaxHealth;
        this.Attack = permanentStatsSO.Attack;
        this.MinAttack = permanentStatsSO.MinAttack;
        this.MaxAttack = permanentStatsSO.MaxAttack;

        this.minTimeToSpawnPlayerBrick = permanentStatsSO.minTimeToSpawnPlayerBrick;
        this.maxTimeToSpawnPlayerBrick = permanentStatsSO.maxTimeToSpawnPlayerBrick;
        this.maxSimultaneousPlayerBricks = permanentStatsSO.maxSimultaneousPlayerBricks;

        this.maxHealthLevel = permanentStatsSO.maxHealthLevel;
        this.minAttackLevel = permanentStatsSO.minAttackLevel;
        this.MinAttackLittleIncrement = permanentStatsSO.MinAttackLittleIncrement;
        this.constToIncreaseMinLittleIncrement = permanentStatsSO.ConstToIncreaseMinLittleIncrement;
        this.MinAttackBigIncrement = permanentStatsSO.MinAttackBigIncrement;
        this.constToIncreaseMinBigIncrement = permanentStatsSO.ConstToIncreaseMinBigIncrement;
        this.maxAttackLevel = permanentStatsSO.maxAttackLevel;
        this.MaxAttackLittleIncrement = permanentStatsSO.MaxAttackLittleIncrement;
        this.constToIncreaseMaxLittleIncrement = permanentStatsSO.ConstToIncreaseMaxLittleIncrement;
        this.MaxAttackBigIncrement = permanentStatsSO.MaxAttackBigIncrement;
        this.constToIncreaseMaxBigIncrement = permanentStatsSO.ConstToIncreaseMaxBigIncrement;

        this.Defense = permanentStatsSO.Defense;
        this.CriticalAttackChance = permanentStatsSO.CriticalAttackChance;
        this.CurrentHealth = this.MaxHealth;
        this.DodgeChance = permanentStatsSO.DodgeChance;

        this.healPercentage = permanentStatsSO.HealPercentage;
        this.littleAttackIncreasePercentage = permanentStatsSO.littleAttackIncreasePercentage;
        this.mediumAttackIncreasePercentage = permanentStatsSO.mediumAttackIncreasePercentage;
        this.maxHealthIncreasePerLevel = permanentStatsSO.maxHealthIncreasePerLevel;
        this.criticalChanceIncrease = permanentStatsSO.criticalChanceIncrease;
        this.adrenalineDogdeChance = permanentStatsSO.adrenalineDogdeChance;
        this.revengePercentageIncrease = permanentStatsSO.revengePercentageIncrease;
        this.healthPercentageToActivateRage = permanentStatsSO.healthPercentageToActivateRage;
        this.extraRageAttack = permanentStatsSO.extraRageAttack;
        this.spinesPercentageDamage = permanentStatsSO.spinesPercentageDamage;

        this.fireLevel = permanentStatsSO.fireLevel;
        this.firePercentageDamage = permanentStatsSO.firePercentageDamage;
        this.firePercentageDamageIncrement = permanentStatsSO.firePercentageDamageIncrement;
        this.fireChanceIncrement = permanentStatsSO.fireChanceIncrement;
        this.timeToDoDamageFire = permanentStatsSO.timeToDoDamageFire;
        this.timeToTurnOffFire = permanentStatsSO.TimeToTurnOffFire;

        this.brickProbabilityList = Clone(permanentStatsSO.brickProbabilityList);
        
    }

    private List<BrickProbability> Clone(List<BrickProbability> listToClone)
    {
        List<BrickProbability> probabilityList = new List<BrickProbability>();

        foreach(BrickProbability brickProbabilityToClone in listToClone)
        {
            BrickProbability newBrickProbability = new BrickProbability(brickProbabilityToClone.BrickType)
            {
                Probability = brickProbabilityToClone.Probability
            };
            probabilityList.Add(newBrickProbability);
        }

        return probabilityList;
    }

    public void LevelUpFireAttack()
    {
        fireLevel += 1;
        firePercentageDamage += firePercentageDamageIncrement;
        
        foreach(BrickProbability brickProbability in brickProbabilityList)
        {
            if(brickProbability.BrickType == BrickTypeEnum.FireBrick)
            {
                brickProbability.Probability += fireChanceIncrement;
                return;
            }
        }
    }
}
