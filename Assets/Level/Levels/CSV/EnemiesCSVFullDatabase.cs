using CSVtoSO.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "CSVReader/New Enemies Database", fileName = "New Enemies Database")]
public class EnemiesCSVFullDatabase : FullDatabaseBase
{
    public CSVToEnemiesDatabase[] enemiesData;
    [SerializeField] LevelSO levelToExportData;

#if UNITY_EDITOR
    public override void ExportData()
    {
        if (levelToExportData == null)
        {
            Debug.LogError("levelToExportData is null");
            return;
        }

        List<Enemy> enemiesList = new List<Enemy>();

        foreach(CSVToEnemiesDatabase enemyData in enemiesData)
        {
            Enemy enemy = new Enemy();

            enemy.SetupEnemy(
                enemyData.enemyHealth,
                enemyData.enemyAttack,
                enemyData.enemyMinTimeToSpawnBrick,
                enemyData.enemyMaxTimeToSpawnBrick,
                enemyData.playerBrickSpawnChance,
                enemyData.enemyRedBrickSpawnChance,
                enemyData.enemyMovingBrickSpawnChance,
                enemyData.enemyShieldBrickSpawnChance,
                enemyData.enemyTrapBrickSpawnChance);

            enemiesList.Add(enemy);
        }

        levelToExportData.Enemies = enemiesList;
        EditorUtility.SetDirty(levelToExportData);
        AssetDatabase.SaveAssets();
    }
#endif
}
