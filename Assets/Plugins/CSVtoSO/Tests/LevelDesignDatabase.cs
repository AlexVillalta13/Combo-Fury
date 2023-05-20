using CSVtoSO.Attributes;
using CSVtoSO.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
[TableIdentifier("Levels design")]
public class LevelDesignDatabase : TableObject
{
    [TableColumnMapper("Max Health")] public int maxHealth;
    [TableColumnMapper("Attack")] public int attack;
    [TableColumnMapper("Player Chance")] public int playerChance;

}
