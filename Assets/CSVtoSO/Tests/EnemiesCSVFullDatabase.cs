using CSVtoSO.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CSVReader/New Enemies Database", fileName = "New Enemies Database")]
public class EnemiesCSVFullDatabase : FullDatabaseBase
{
    public CSVToEnemiesDatabase[] enemies;

    public CSVToEnemiesDatabase[] GetEnemiesDatabase()
    {
        return enemies;
    }
}
