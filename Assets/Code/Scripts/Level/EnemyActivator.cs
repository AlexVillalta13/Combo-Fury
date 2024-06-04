using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActivator : MonoBehaviour
{
    [SerializeField] List<EnemySpawn> spawnList = new List<EnemySpawn>();
    int enemiesCount;

    [SerializeField] int currentEnemy = -1;

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
        if(gameObject.activeInHierarchy == false)
        {
            return;
        }
        currentEnemy++;
        spawnList[currentEnemy].ActivateEnemy(true);
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
