using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerStatsSO", fileName = "New Player Stats SO")]
public class PlayerStatsSO : ScriptableObject
{
    [SerializeField] float maxHealth = 100;
    public float MaxHealth { set { maxHealth = value; } get { return maxHealth; } }


    [SerializeField] float currentHealth = 100;
    public float CurrentHealth { set { currentHealth = value; } get { return currentHealth; } }


    [SerializeField] float attack = 22;
    public float Attack { set { attack = value; } get { return attack; } }


    [SerializeField] float defense = 5;
    public float Defense { set { defense = value; } get { return defense; } }


    [SerializeField] float criticalAttackChance = 20f;
    public float CriticalAttackChance { set { criticalAttackChance = value; } get { return criticalAttackChance; } }


    int fireLevel = 0;
    float firePercentageDamage = 0f;
    [SerializeField] float fireDamageIncrement = 10f;
    float fireChance = 0f;
    [SerializeField] float fireChanceIncrement = 10f;
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

        this.fireLevel = permanentStatsSO.fireLevel;
        this.firePercentageDamage = permanentStatsSO.firePercentageDamage;
        this.fireDamageIncrement = permanentStatsSO.fireDamageIncrement;
        this.fireChance = permanentStatsSO.fireChance;
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
