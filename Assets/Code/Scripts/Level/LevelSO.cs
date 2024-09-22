using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "New Level")]
public class LevelSO : ScriptableObject, ILevelData
{
    [SerializeField] PlayerStatsSO inCombatStatsSO;

    [SerializeField] Sprite levelIcon;
    public Sprite LevelIcon => levelIcon;

    [SerializeField] SceneEnum environment = SceneEnum.AlpineWoods;
    // public SceneEnum Environment => environment;
    public SceneEnum GetEnvironment()
    {
        return environment;
    }

    [SerializeField] int initialPosition = 0;

    [SerializeField] BrickTypesSO brickTypes;

    [SerializeField] int minibossEncounterAt = 10;
    public int MinibossEncounterAt => minibossEncounterAt;

    [SerializeField] List<EnemyData> enemies = new List<EnemyData>();

    public List<EnemyData> Enemies { get => enemies; set => enemies = value; }

    public EnemyData GetEnemy(int enemyNumber)
    {
        return enemies[enemyNumber];
    }

    public int GetTotalEnemiesCount()
    {
        return enemies.Count;
    }

    private void OnEnable()
    {
        foreach(EnemyData enemy in Enemies)
        {
            if(enemy != null)
            {
                enemy.InCombatStatsSO = inCombatStatsSO;
            }
        }
    }
}