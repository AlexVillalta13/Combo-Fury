using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "Procedural Level")]
public class ProceduralLevelSO : ScriptableObject, ILevelData
{
    [SerializeField] private SceneEnum environment = SceneEnum.AlpineWoods;
    public SceneEnum GetEnvironment()
    {
        return environment;
    }

    [SerializeField] private int initialHealth = 40;

    [SerializeField] private int minAttack = 3;
    [SerializeField] private int maxAttack = 7;

    [SerializeField] private List<BrickProbability> bricksProbabilities = new List<BrickProbability>();
    
    public EnemyData enemy;
    public int currentEnemy = -1;
    
    private void OnEnable()
    {
        currentEnemy = -1;
    }

    public EnemyData GetEnemy(int enemyNumber)
    {
        if (enemyNumber + 1 == currentEnemy)
        {
            return enemy;
        }
        
        return UpdateEnemyData(enemyNumber);
    }

    private EnemyData UpdateEnemyData(int enemyNumber)
    {
        currentEnemy = enemyNumber + 1;
        SetEnemyHealth();
        SetEnemyAttack();
        SetEnemyBricksProbabilities();
        return enemy;
    }

    private void SetEnemyHealth()
    {
        int maxHealthToIncrease = initialHealth;
        
        for(int i = 1; i <= currentEnemy; i++)
        {
            if (1 < i && i <= 10)
            {
                maxHealthToIncrease += 16;
            }
            else if (i % 10 == 1 && i > 20)
            {
                maxHealthToIncrease += 109;
            }
            else if (i >= 11)
            {
                maxHealthToIncrease += 41;
            }
        }
        enemy.Health = maxHealthToIncrease;
    }

    private void SetEnemyAttack()
    {
        int increment = 2; 
        int totalAttack = minAttack;
        
        for (int i = 2; i <= currentEnemy; i++) 
        {
            if (i > 10)
            {
                increment = 4 + (i - 11) / 10;
            }
            totalAttack += increment;
        }

        enemy.minAttack = totalAttack;
        enemy.maxAttack = enemy.minAttack * 2f + 1f;
    }

    private void SetEnemyBricksProbabilities()
    {
        foreach (BrickProbability brickProbabilityToCompare in bricksProbabilities)
        {
            foreach (BrickProbability enemyCurrentBrickProbability in enemy.EnemyBricks)
            {
                if(enemyCurrentBrickProbability.BrickType == brickProbabilityToCompare.BrickType)
                {
                    if (currentEnemy >= brickProbabilityToCompare.LevelToUnlock)
                    {
                        enemyCurrentBrickProbability.Probability = brickProbabilityToCompare.Probability;
                    }
                    else
                    {
                        enemyCurrentBrickProbability.Probability = 0f;
                    }
                    break;
                }
            }
        }
    }
    
    public int GetTotalEnemiesCount()
    {
        return 0;
    }
}
