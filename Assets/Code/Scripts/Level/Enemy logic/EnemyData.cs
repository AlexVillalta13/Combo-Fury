using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyData
{
    public PlayerStatsSO InCombatStatsSO { get; set; }

    [SerializeField] float health = 100;
    public float Health { get { return health; } }

    [SerializeField] float attack = 5;
    public float Attack { get { return attack; } }

    [SerializeField] float minTimeToSpawnBrick = 1f;
    public float MinTimeToSpawnBrick { get { return minTimeToSpawnBrick; } }

    [SerializeField] float maxTimeToSpawnBrick = 3f;
    public float MaxTimeToSpawnBrick { get { return maxTimeToSpawnBrick; } }

    [SerializeField] float chanceOfPlayerBrick = 60f;
    public float ChanceOfPlayerBrick => chanceOfPlayerBrick;

    [SerializeField] float chanceOfEnemyBrick = 40f;
    public float ChanceOfEnemyBrick => chanceOfEnemyBrick;

    [SerializeField] List<CurrencyReward> currencyRewardList = new List<CurrencyReward>();
    public List<CurrencyReward> CurrencyRewardsList => currencyRewardList;

    [SerializeField] List<BrickProbability> enemyBricks = new List<BrickProbability>();
    public List<BrickProbability> EnemyBricks { get { return enemyBricks; } }

    public BrickTypeEnum GetRandomBrick()
    {
        float maxRange = chanceOfPlayerBrick + chanceOfEnemyBrick;
        float randomNumber = UnityEngine.Random.Range(0f, maxRange);
        float rangeNumberToSpawn = 0f;
        if (rangeNumberToSpawn < randomNumber && (rangeNumberToSpawn + chanceOfPlayerBrick) > randomNumber)
        {
            //Player Brick
            return InCombatStatsSO.GetRandomPlayerBrick();
        }
        else
        {
            //Enemy Brick
            maxRange = 0f;
            foreach (BrickProbability brickProbability in enemyBricks)
            {
                maxRange += brickProbability.Probability;
            }

            randomNumber = UnityEngine.Random.Range(0f, maxRange);
            rangeNumberToSpawn = 0f;
            foreach (BrickProbability brickProbability in enemyBricks)
            {
                if (rangeNumberToSpawn < randomNumber && (rangeNumberToSpawn + brickProbability.Probability) > randomNumber)
                {
                    return brickProbability.BrickType;
                }
                rangeNumberToSpawn += brickProbability.Probability;
            }
            Debug.LogError("LevelSo: No random Enemy brick selected");
        }

        Debug.LogError("LevelSo: No random brick selected");
        return BrickTypeEnum.Redbrick;
    }

    public void SetEnemyData(float maxHealth, float attack, float minTimeToSpawnBrick, float maxTimeToSpawnBrick, float playerChance, float redBrickChance, float movingBrickChance, float shieldBrickChance, float trapChance)
    {
        this.health = maxHealth;
        this.attack = attack;
        this.minTimeToSpawnBrick = minTimeToSpawnBrick;
        this.maxTimeToSpawnBrick = maxTimeToSpawnBrick;
        this.chanceOfPlayerBrick = playerChance;
        this.chanceOfEnemyBrick = 100f - chanceOfPlayerBrick;

        enemyBricks.Clear();

        CreateNewBrickProbability(BrickTypeEnum.Redbrick, redBrickChance);
        if (movingBrickChance > 0f)
        {
            CreateNewBrickProbability(BrickTypeEnum.SpeedBrick, movingBrickChance);
        }
        if (shieldBrickChance > 0f)
        {
            CreateNewBrickProbability(BrickTypeEnum.ShieldBrick, shieldBrickChance);
        }
        if (trapChance > 0f)
        {
            CreateNewBrickProbability(BrickTypeEnum.BlackBrick, trapChance);
        }
    }

    private void CreateNewBrickProbability(BrickTypeEnum brickTypeEnum, float probabilityToSpawn)
    {
        BrickProbability newgBrickProbability = new BrickProbability(brickTypeEnum, probabilityToSpawn);
        enemyBricks.Add(newgBrickProbability);
    }
}
