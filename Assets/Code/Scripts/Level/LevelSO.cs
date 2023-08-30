using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "New Level")]
public class LevelSO : ScriptableObject
{
    [SerializeField] PlayerStatsSO inCombatStatsSO;
    [SerializeField] SceneEnum environment = SceneEnum.AlpineWoods;
    [SerializeField] int initialPosition = 0;

    [SerializeField] BrickTypesSO brickTypes;

    [SerializeField] int minibossEncounterAt = 10;
    public int MinibossEncounterAt { get { return minibossEncounterAt; } }

    [SerializeField] List<Enemy> enemies = new List<Enemy>();

    public List<Enemy> Enemies { get { return enemies; } }

    private void OnEnable()
    {
        foreach(Enemy enemy in Enemies)
        {
            if(enemy != null)
            {
                enemy.InCombatStatsSO = inCombatStatsSO;
            }
        }
    }
}

[System.Serializable]
public class Enemy
{
    public PlayerStatsSO InCombatStatsSO { get; set; }

    [SerializeField] float health = 100;
    public float Health { get { return health; }}
    [SerializeField] float attack = 5;
    public float Attack { get { return attack; }}
    [SerializeField] float minTimeToSpawnBrick = 1f;
    public float MinTimeToSpawnBrick { get { return minTimeToSpawnBrick; }}
    [SerializeField] float maxTimeToSpawnBrick = 3f;
    public float MaxTimeToSpawnBrick { get { return maxTimeToSpawnBrick; }}
    [SerializeField] float chanceOfPlayerBrick = 60f;
    [SerializeField] float chanceOfEnemyBrick = 40f;
    [SerializeField] List <BrickProbability> enemyBricks = new List<BrickProbability>();
    public List<BrickProbability> EnemyBricks { get { return enemyBricks; } }

    public BrickTypeEnum GetRandomBrick()
    {
        float maxRange = chanceOfPlayerBrick + chanceOfEnemyBrick;
        float randomNumber = UnityEngine.Random.Range(0f, maxRange);
        float rangeNumberToSpawn = 0f;
        if (rangeNumberToSpawn < randomNumber && (rangeNumberToSpawn + chanceOfPlayerBrick) > randomNumber)
        {
            //Player Brick
            randomNumber = Random.Range(0f, 100f);
            if (randomNumber < InCombatStatsSO.CriticalAttackChance)
            {
                return BrickTypeEnum.Greenbrick;
            }
            else
            {
                return BrickTypeEnum.YellowBrick;
            }
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
}

[System.Serializable]
public class BrickProbability
{
    [SerializeField] BrickTypeEnum brickType;
    public BrickTypeEnum BrickType { get { return brickType; }}
    [SerializeField] float probability;
    public float Probability { get { return probability; }}
}