using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;
using UnityEngine.UIElements;
using System;

public class CombatBarPresenter : MonoBehaviour
{
    bool inCombat = false;

    float timeToSpawnBrick = 5f;
    float timerSpawn = 0f;

    [SerializeField] BrickTypesSO brickTypesSO;

    CombatBarUI combatBarUI;

    ILevelData levelSO;
    [SerializeField] EnemyStats EnemyStats;

    [SerializeField] PlayerStatsSO inCombatPlayerStatsSo;

    //EnemyBrickStats
    float chanceOfPlayerBrick = 60f;
    float chanceOfEnemyBrick = 40f;

    float maxRange;
    float randomNumber;
    float rangeNumberToSpawn;

    private void Awake()
    {
        combatBarUI = FindAnyObjectByType<CombatBarUI>();
    }

    private void OnEnable()
    {
        LevelSelectorUI.onSelectedLevelToPlay += SetupLevel;
    }

    private void OnDisable()
    {
        LevelSelectorUI.onSelectedLevelToPlay -= SetupLevel;
    }

    void Update()
    {
        if (inCombat == true)
        {
            combatBarUI.MovePointer();

            timerSpawn += Time.deltaTime;
            if (timerSpawn > timeToSpawnBrick)
            {
                timerSpawn = 0f;
                CreateRandomTimeToSpawnBrick();

                GetRandomBrickFromSO();
            }
        }
    }

    private void GetRandomBrickFromSO()
    {
        SetupEnemyBrickProbabilityStats();

        BrickTypeEnum brickTypeToSpawn = ChoosePlayerOrEnemyBrick();

        combatBarUI.InitializeBrick(brickTypesSO.GetPool(brickTypeToSpawn).Pool.Get());
    }
    private void SetupEnemyBrickProbabilityStats()
    {
        chanceOfPlayerBrick = levelSO.GetEnemy(EnemyStats.currentEnemy).ChanceOfPlayerBrick;
        chanceOfEnemyBrick = levelSO.GetEnemy(EnemyStats.currentEnemy).ChanceOfEnemyBrick;

        maxRange = chanceOfPlayerBrick + chanceOfEnemyBrick;
        randomNumber = UnityEngine.Random.Range(0f, maxRange);
        rangeNumberToSpawn = 0f;
    }

    public BrickTypeEnum ChoosePlayerOrEnemyBrick()
    {
        if (rangeNumberToSpawn < randomNumber && (rangeNumberToSpawn + chanceOfPlayerBrick) > randomNumber)
        {
            //Player Brick
            return inCombatPlayerStatsSo.GetRandomPlayerBrick();
        }
        else
        {
            //Enemy Brick
            maxRange = 0f;
            foreach (BrickProbability brickProbability in levelSO.GetEnemy(EnemyStats.currentEnemy).EnemyBricks)
            {
                maxRange += brickProbability.Probability;
            }

            randomNumber = UnityEngine.Random.Range(0f, maxRange);
            rangeNumberToSpawn = 0f;
            foreach (BrickProbability brickProbability in levelSO.GetEnemy(EnemyStats.currentEnemy).EnemyBricks)
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

    private void SetupLevel(ILevelData level)
    {
        this.levelSO = level;
    }

    public void InCombat()
    {
        inCombat = true;
        CreateRandomTimeToSpawnBrick();
    }

    public void OutOfCombat()
    {
        inCombat = false;
    }

    private void CreateRandomTimeToSpawnBrick()
    {
        timeToSpawnBrick = UnityEngine.Random.Range(levelSO.GetEnemy(EnemyStats.currentEnemy).MinTimeToSpawnBrick, levelSO.GetEnemy(EnemyStats.currentEnemy).MaxTimeToSpawnBrick);
    }
}
