using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "New Level")]
public class LevelSO : ScriptableObject
{
    [SerializeField] SceneEnum environment = SceneEnum.AlpineWoods;
    [SerializeField] int initialPosition = 0;

    [SerializeField] BrickTypesSO brickTypes;

    [SerializeField] List<Enemy> enemies = new List<Enemy>();

    public List<Enemy> Enemies { get { return enemies; } }
}

[System.Serializable]
public class Enemy
{
    [SerializeField] int health;
    public int Health { get { return health; }}
    [SerializeField] int attack;
    public int Attack { get { return attack; }}
    [SerializeField] float minTimeToSpawnBrick;
    public float MinTimeToSpawnBrick { get { return minTimeToSpawnBrick; }}
    [SerializeField] float maxTimeToSpawnBrick;
    public float MaxTimeToSpawnBrick { get { return maxTimeToSpawnBrick; }}
    [SerializeField] List <BrickProbability> enemyBricks = new List<BrickProbability>();
    public List<BrickProbability> EnemyBricks { get { return enemyBricks; } }

    public BrickTypeEnum GetRandomBrick()
    {
        float maxRange = 0f;
        foreach(BrickProbability brickProbability in enemyBricks)
        {
            maxRange += brickProbability.Probability;
        }

        float randomNumber = UnityEngine.Random.Range(0f, maxRange);
        float rangeNumberToSpawn = 0f;
        foreach(BrickProbability brickProbability in enemyBricks)
        {
            if(rangeNumberToSpawn < randomNumber && (rangeNumberToSpawn + brickProbability.Probability) > randomNumber)
            {
                return brickProbability.BrickType;
            }
            rangeNumberToSpawn += brickProbability.Probability;
        }

        Debug.LogError("LevelSo: No random brick selected");
        return BrickTypeEnum.Redbrick;
    }
}

[System.Serializable]
public struct BrickProbability
{
    [SerializeField] BrickTypeEnum brickType;
    public BrickTypeEnum BrickType { get { return brickType; }}
    [SerializeField] float probability;
    public float Probability { get { return probability; }}
}