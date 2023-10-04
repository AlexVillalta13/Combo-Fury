using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] List<GameObject> enemyPrefabsList = new List<GameObject>();

    [SerializeField] int enemyPositionInList;
    bool enemyIsBoss = false;
    GameObject instantiateEnemy;

    public void Setup(int enemyPositionInList, bool enemyIsBoss)
    {
        this.enemyPositionInList = enemyPositionInList;
        this.enemyIsBoss = enemyIsBoss;
    }

    public void DeleteEnemyGameObject()
    {
        Destroy(instantiateEnemy);
    }

    private GameObject SelectRandomEnemy()
    {
        return enemyPrefabsList[Random.Range(0, enemyPrefabsList.Count)];
    }

    public void ActivateEnemy(bool state)
    {
        if(state == true)
        {
            InstantiateEnemyPrefab();
        }
        else if(state == false)
        {
            instantiateEnemy.SetActive(state);
        }
    }

    private void InstantiateEnemyPrefab()
    {
        instantiateEnemy = Instantiate(SelectRandomEnemy(), transform.position, transform.rotation);
        if (enemyIsBoss == true)
        {
            instantiateEnemy.transform.localScale = 2f * Vector3.one;
        }
    }
}
