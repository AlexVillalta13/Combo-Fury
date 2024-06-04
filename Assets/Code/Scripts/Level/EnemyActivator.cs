using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActivator : MonoBehaviour
{
    [SerializeField] List<EnemySpawn> spawnList = new List<EnemySpawn>();

    [SerializeField] int currentEnemy = -1;

    private void Awake()
    {
        SetListOfEnemySpawns();
    }

    private void SetListOfEnemySpawns()
    {
        foreach (Transform child in transform)
        {
            spawnList.Add(child.GetComponent<EnemySpawn>());
        }
    }

    public void SetIfEnemyIsBoss(int enemiesCount)
    {
        for (int i = 0; i < enemiesCount; i++)
        {
            if(i == enemiesCount - 1)
            {
                spawnList[i].SetIfEnemyIsBoss(true);
            }
            else
            {
                spawnList[i].SetIfEnemyIsBoss(false); ;
            }
        }
    }

    private void OnDisable()
    {
        DeleteInstantiatedEnemies();
    }

    private void DeleteInstantiatedEnemies()
    {
        currentEnemy = -1;
        foreach (EnemySpawn enemySpawn in spawnList)
        {
            enemySpawn.DeleteEnemyGameObject();
        }
    }

    public void ActivateNextEnemy()
    {
        if(gameObject.activeInHierarchy == false)
        {
            return;
        }
        currentEnemy++;
        spawnList[currentEnemy].ActivateEnemy();
    }

    public void DeactivatePreviousEnemy()
    {
        if (gameObject.activeInHierarchy == false)
        {
            return;
        }
        if (currentEnemy - 1 >= 0)
        {
            spawnList[currentEnemy - 1].DeactivateEnemy();
        }
    }
}
