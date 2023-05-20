using CSVtoSO.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CSVReader/Test/Level Design", fileName = "New Test Database")]
public class LevelDesignFullDatabase : FullDatabaseBase
{
    [NonReorderable] public LevelDesignDatabase[] level;
}
