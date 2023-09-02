using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneElementsHolder : MonoBehaviour
{
    EnemyActivator enemyActivator;
    private void Awake()
    {
        enemyActivator = GetComponentInChildren<EnemyActivator>();
    }

    public void SetupLevel(int enemiesCount)
    {
        enemyActivator.Setup(enemiesCount);
    }
}
