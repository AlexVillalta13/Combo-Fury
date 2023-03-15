using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActivator : MonoBehaviour
{
    List<EnemySpawn> spawnList = new List<EnemySpawn>();

    int currentEnemy = -1;

    private void Awake()
    {
        foreach(Transform child in transform)
        {
            spawnList.Add(child.GetComponent<EnemySpawn>());
        }
    }

    private void OnDisable()
    {
        currentEnemy = -1;
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
