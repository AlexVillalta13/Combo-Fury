using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;
using UnityEngine.UIElements;
using System;

public class CombatBarPresenter : MonoBehaviour
{
    [SerializeField] BrickTypesSO brickTypesSO;

    CombatBarUI combatBarUI;

    LevelSO levelSO;
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

    private void GetRandomBrickFromSO()
    {
        SetupEnemyBrickProbabilityStats();

        BrickTypeEnum brickTypeToSpawn = ChoosePlayerOrEnemyBrick();

        VisualElement visualElement = brickTypesSO.GetBrick(brickTypeToSpawn).BrickUIAsset.Instantiate();

        InitializeBrick(brickTypesSO.GetPool(brickTypeToSpawn).Pool.Get(), visualElement);
    }
    private void SetupEnemyBrickProbabilityStats()
    {
        chanceOfPlayerBrick = levelSO.Enemies[EnemyStats.currentEnemy].ChanceOfPlayerBrick;
        chanceOfEnemyBrick = levelSO.Enemies[EnemyStats.currentEnemy].ChanceOfEnemyBrick;

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
            foreach (BrickProbability brickProbability in levelSO.Enemies[EnemyStats.currentEnemy].EnemyBricks)
            {
                maxRange += brickProbability.Probability;
            }

            randomNumber = UnityEngine.Random.Range(0f, maxRange);
            rangeNumberToSpawn = 0f;
            foreach (BrickProbability brickProbability in levelSO.Enemies[EnemyStats.currentEnemy].EnemyBricks)
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


    private void InitializeBrick(Brick brickScriptToSpawn, VisualElement brickUIElement)
    {
        brickScriptToSpawn.SetupBrick(this, brickUIElement, playerBrickElementHolder, enemyBricksElementHolder);
        bricksInBarDict.Add(brickUIElement, brickScriptToSpawn);
    }

    private void SetupLevel(LevelSO level)
    {
        this.levelSO = level;
    }
}
