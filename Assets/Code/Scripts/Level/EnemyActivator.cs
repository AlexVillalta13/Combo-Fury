using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActivator : MonoBehaviour
{
    List<EnemySpawn> spawnList = new List<EnemySpawn>();
    int enemiesCount;

    int currentEnemy = -1;

    private void Awake()
    {
        foreach(Transform child in transform)
        {
            spawnList.Add(child.GetComponent<EnemySpawn>());
        }
    }

    public void Setup(int enemiesCount)
    {
        this.enemiesCount = enemiesCount;
        for (int i = 0; i < enemiesCount; i++)
        {
            if(i == enemiesCount - 1)
            {
                spawnList[i].Setup(i, true);
            }
            else
            {
                spawnList[i].Setup(i, false); ;
            }
        }
    }

    private void OnDisable()
    {
        currentEnemy = -1;
        foreach(EnemySpawn enemySpawn in spawnList)
        {
            enemySpawn.DeleteEnemyGameObject();
        }
    }

    public void ActivateNextEnemy()
    {
        currentEnemy++;
        spawnList[currentEnemy].ActivateEnemy(true);
    }

    public void DeactivatePreviousEnemy()
    {
        if (currentEnemy - 1 >= 0)
        {
            spawnList[currentEnemy - 1].ActivateEnemy(false);
        }
    }
}
