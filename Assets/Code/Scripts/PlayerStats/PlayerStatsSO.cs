using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

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
    public float MinAttack = 10f;
    public float MaxAttack = 20f;

    public float Defense = 0f;
    public float CriticalAttackChance = 20f;
    public float DodgeChance = 0f;
    public int comboCount = 0;


    [Title("Player Upgrade Stats Increments")]
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

    [Title("Player Bricks")]
    [SerializeField] List<BrickProbability> brickProbabilityList;
    public List<BrickProbability> BrickProbabilityList => brickProbabilityList;

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

        this.Defense = permanentStatsSO.Defense;
        this.CriticalAttackChance = permanentStatsSO.CriticalAttackChance;
        this.CurrentHealth = this.MaxHealth;
        this.DodgeChance = permanentStatsSO.DodgeChance;

        this.healPercentage = permanentStatsSO.HealPercentage;
        this.littleAttackIncreasePercentage = permanentStatsSO.littleAttackIncreasePercentage;
        this.mediumAttackIncreasePercentage = permanentStatsSO.mediumAttackIncreasePercentage;
        this.maxHealthIncreasePercentage = permanentStatsSO.maxHealthIncreasePercentage;
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
