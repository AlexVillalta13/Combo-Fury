using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] List<GameObject> enemyPrefabsList = new List<GameObject>();

    GameObject instantiateEnemy;

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
            instantiateEnemy = Instantiate(SelectRandomEnemy(), transform.position, transform.rotation);
        }
        else if(state == false)
        {
            instantiateEnemy.SetActive(state);
        }
    }
}
