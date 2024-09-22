using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "ProceduralPlayerStatsSO", fileName = "New Procedural Player Stats SO")]
public class ProceduralPlayerStatsSO : ScriptableObject
{
    [FormerlySerializedAs("MaxHealth")] [Title("Player Base Stats")]
    public float maxHealth = 100f;
    public float currentHealth = 100f;
    public bool PlayerIsAlive()
    {
        return currentHealth > 0;
    }
    public float minAttack = 10f;
    public float maxAttack = 10f;
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

    public void StartGame(ProceduralPlayerStatsSO permanentStatsSO)
    {
        this.maxHealth = permanentStatsSO.maxHealth;
        this.minAttack = permanentStatsSO.minAttack;
        this.maxAttack = permanentStatsSO.maxAttack;
        this.currentHealth = this.maxHealth;

        this.healPercentage = permanentStatsSO.HealPercentage;
        this.littleAttackIncreasePercentage = permanentStatsSO.littleAttackIncreasePercentage;
        this.mediumAttackIncreasePercentage = permanentStatsSO.mediumAttackIncreasePercentage;
        this.maxHealthIncreasePercentage = permanentStatsSO.maxHealthIncreasePercentage;
        this.criticalChanceIncrease = permanentStatsSO.criticalChanceIncrease;

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
}
