using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] List<GameObject> enemyPrefabsList = new List<GameObject>();

    [SerializeField] int enemyPositionInList;
    bool enemyIsBoss = false;
    GameObject instantiatedEnemy;

    public void Setup(int enemyPositionInList, bool enemyIsBoss)
    {
        this.enemyPositionInList = enemyPositionInList;
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

    public void ActivateEnemy(bool state)
    {
        //if(state == true)
        //{
            //InstantiateEnemyPrefab();
        //}
        //else if(state == false)
        //{
        //    instantiateEnemy.SetActive(state);
        //}
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

    private void InstantiateEnemyPrefab()
    {
        instantiatedEnemy = Instantiate(SelectRandomEnemy(), transform.position, transform.rotation);
        if (enemyIsBoss == true)
        {
            instantiatedEnemy.transform.localScale = 2f * Vector3.one;
        }
    }
}
