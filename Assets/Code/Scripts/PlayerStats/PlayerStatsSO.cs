using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "PlayerStatsSO", fileName = "New Player Stats SO")]
public class PlayerStatsSO : ScriptableObject
{
    [Title("Player Base Stats")]
    [SerializeField] float maxHealth = 100;
    public float MaxHealth { set { maxHealth = value; } get { return maxHealth; } }
    [SerializeField] float currentHealth = 100;
    public float CurrentHealth { set { currentHealth = value; } get { return currentHealth; } }
    public bool PlayerIsAlive()
    {
        return currentHealth > 0;
    }
    [SerializeField] float attack = 10f;
    public float Attack { set { attack = value; } get { return attack; } }
    [SerializeField] float defense = 0f;
    public float Defense { set { defense = value; } get { return defense; } }
    [SerializeField] float criticalAttackChance = 20f;
    public float CriticalAttackChance { set { criticalAttackChance = value; } get { return criticalAttackChance; } }
    public float DodgeChance = 0f;


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
    public float AdrenalineDodgeChance = 30f;
    public float RevengePercentageIncrease = 200f;
    //[SerializeField] float healthPercentageToActivateRage = 30f;
    //public float HealthPercentageToActivateRage { get {  return healthPercentageToActivateRage; } }
    //[SerializeField] float extraRageAttack = 20f;
    //public float ExtraRageAttack { get {  return extraRageAttack; } }


    [Title("Fire Upgrade Stats")]
    [SerializeField] float fireDamageIncrement = 10f;
    [SerializeField] float fireChanceIncrement = 10f;
    [SerializeField] float timeTodoDamageFire = 1f;
    int fireLevel = 0;
    float firePercentageDamage = 0f;
    public float FirePercentageDamage { get { return fireDamageIncrement; } }

    public float TimeToDoDamageFire { get { return timeTodoDamageFire; } }
    [SerializeField] float timeToTurnOffFire = 10f;
    public float TimeToTurnOffFire { get { return timeToTurnOffFire; } }

    [Title("Player Bricks")]
    [SerializeField] List<BrickProbability> brickProbabilityList;

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
        this.Defense = permanentStatsSO.Defense;
        this.CriticalAttackChance = permanentStatsSO.CriticalAttackChance;
        this.CurrentHealth = this.MaxHealth;
        this.DodgeChance = permanentStatsSO.DodgeChance;

        this.fireLevel = permanentStatsSO.fireLevel;
        this.firePercentageDamage = permanentStatsSO.firePercentageDamage;
        this.fireDamageIncrement = permanentStatsSO.fireDamageIncrement;
        this.fireChanceIncrement = permanentStatsSO.fireChanceIncrement;

        this.brickProbabilityList = Clone(permanentStatsSO.brickProbabilityList);
        
    }

    private List<BrickProbability> Clone(List<BrickProbability> listToClone)
    {
        List<BrickProbability> probabilityList = new List<BrickProbability>();

        foreach(BrickProbability brickProbabilityToClone in listToClone)
        {
            BrickProbability newBrickProbability = new BrickProbability(brickProbabilityToClone.BrickType, brickProbabilityToClone.Probability);
            probabilityList.Add(newBrickProbability);
        }

        return probabilityList;
    }

    public void LevelUpFireAttack()
    {
        fireLevel += 1;
        firePercentageDamage += fireDamageIncrement;
        
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
