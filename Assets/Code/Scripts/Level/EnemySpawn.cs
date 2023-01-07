using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] List<GameObject> enemyPrefabsList = new List<GameObject>();

    GameObject instantiateEnemy;

    private void OnEnable()
    {
        instantiateEnemy = Instantiate(SelectRandomEnemy(), transform.position, transform.rotation);
    }

    private void OnDisable()
    {
        Destroy(instantiateEnemy);
    }

    private GameObject SelectRandomEnemy()
    {
        return enemyPrefabsList[Random.Range(0, enemyPrefabsList.Count)];
    }
}
