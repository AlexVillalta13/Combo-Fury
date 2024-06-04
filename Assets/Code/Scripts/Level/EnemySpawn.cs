using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] List<GameObject> enemyPrefabsList = new List<GameObject>();

    bool enemyIsBoss = false;
    GameObject instantiatedEnemy;

    public void SetIfEnemyIsBoss(bool enemyIsBoss)
    {
        this.enemyIsBoss = enemyIsBoss;
    }

    public void DeleteEnemyGameObject()
    {
        Destroy(instantiatedEnemy);
    }

    private GameObject SelectRandomEnemy()
    {
        return enemyPrefabsList[Random.Range(0, enemyPrefabsList.Count)];
    }

    public void ActivateEnemy()
    {
        instantiatedEnemy = Instantiate(SelectRandomEnemy(), transform.position, transform.rotation);
        if (enemyIsBoss == true)
        {
            instantiatedEnemy.transform.localScale = 2f * Vector3.one;
        }
    }

    public void DeactivateEnemy()
    {
        instantiatedEnemy.SetActive(false);
    }
}
