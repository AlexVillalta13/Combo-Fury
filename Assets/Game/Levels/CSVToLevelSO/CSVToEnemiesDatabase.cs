using CSVtoSO.Attributes;
using CSVtoSO.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[TableIdentifier("EnemyCSVData")]
public class CSVToEnemiesDatabase : TableObject
{
    [TableColumnMapper("Max Health")] public int enemyHealth;
    [TableColumnMapper("Attack")] public int enemyAttack;
    [TableColumnMapper("Min Brick Spawn")] public int enemyMinTimeToSpawnBrick;
    [TableColumnMapper("Max Brick Spawn")] public int enemyMaxTimeToSpawnBrick;
    [TableColumnMapper("Player Chance")] public int playerBrickSpawnChance;
    [TableColumnMapper("Red Chance")] public int enemyRedBrickSpawnChance;
    [TableColumnMapper("Moving Brick")] public int enemyMovingBrickSpawnChance;
    [TableColumnMapper("Shield Brick")] public int enemyShieldBrickSpawnChance;
    [TableColumnMapper("Trap Brick")] public int enemyTrapBrickSpawnChance;
}
