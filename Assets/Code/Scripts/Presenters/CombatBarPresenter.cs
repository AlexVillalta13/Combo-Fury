using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatBarPresenter : MonoBehaviour
{
    private bool inCombat = false;

    private float minTimeToSpawnEnemyBrick = 0f;
    private float maxTimeToSpawnEnemyBrick = 0f;
    private float timeToSpawnEnemyBrick = 0f;
    private float spawnEnemyBrickTimer = 0f;
    
    private float minTimeToSpawnPlayerBrick = 0f;
    private float maxTimeToSpawnPlayerBrick = 0f;
    private int maxSimultaneousPlayerBricks = 0;
    private float timeToSpawnPlayerBrick = 0f;
    private float spawnEnemyPlayerTimer = 0f;

    [SerializeField] private BrickTypesSO brickTypesSO;

    private CombatBarUI combatBarUI;

    private ILevelData levelSO;
    [SerializeField] private EnemyStats EnemyStats;

    [SerializeField] private PlayerStatsSO inCombatPlayerStatsSo;

    //EnemyBrickStats
    private float maxRange;
    private float randomNumber;
    private float rangeNumberToSpawn;

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
            
            SpawnEnemyBrick();

            SpawnPlayerBrick();
        }
    }
    
    private void SpawnEnemyBrick()
    {
        spawnEnemyBrickTimer += Time.deltaTime;
        if (spawnEnemyBrickTimer >= timeToSpawnEnemyBrick)
        {
            BrickTypeEnum brickTypeToSpawn = levelSO.GetEnemy(EnemyStats.currentEnemy).GetRandomEnemyBrick();
            combatBarUI.InitializeBrick(brickTypesSO.GetPool(brickTypeToSpawn).Pool.Get());
            timeToSpawnEnemyBrick = Random.Range(minTimeToSpawnEnemyBrick, maxTimeToSpawnEnemyBrick);
            spawnEnemyBrickTimer = 0f;
        }
    }
    
    private void SpawnPlayerBrick()
    {
        spawnEnemyPlayerTimer += Time.deltaTime;
        if (spawnEnemyPlayerTimer >= timeToSpawnPlayerBrick && combatBarUI.GetPlayerBricksInBar() < maxSimultaneousPlayerBricks)
        {
            BrickTypeEnum brickTypeToSpawn = inCombatPlayerStatsSo.GetRandomPlayerBrick();
            combatBarUI.InitializeBrick(brickTypesSO.GetPool(brickTypeToSpawn).Pool.Get());
            timeToSpawnPlayerBrick = Random.Range(minTimeToSpawnPlayerBrick, maxTimeToSpawnPlayerBrick);
            spawnEnemyPlayerTimer = 0f;
        }
    }
    
    private void SetupLevel(ILevelData level)
    {
        this.levelSO = level;
        CreateRandomTimeToSpawnBrick();
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
        minTimeToSpawnEnemyBrick = levelSO.GetEnemy(EnemyStats.currentEnemy).MinTimeToSpawnBrick;
        maxTimeToSpawnEnemyBrick = levelSO.GetEnemy(EnemyStats.currentEnemy).MaxTimeToSpawnBrick;

        minTimeToSpawnPlayerBrick = inCombatPlayerStatsSo.MinTimeToSpawnPlayerBrick;
        maxTimeToSpawnPlayerBrick = inCombatPlayerStatsSo.MaxTimeToSpawnPlayerBrick;
        maxSimultaneousPlayerBricks = inCombatPlayerStatsSo.MaxSimultaneousPlayerBricks;
        
        timeToSpawnEnemyBrick = Random.Range(minTimeToSpawnEnemyBrick, maxTimeToSpawnEnemyBrick);
        spawnEnemyBrickTimer = 0f;
        timeToSpawnPlayerBrick = Random.Range(minTimeToSpawnPlayerBrick, maxTimeToSpawnPlayerBrick);
        spawnEnemyPlayerTimer = 0f;
    }
}
