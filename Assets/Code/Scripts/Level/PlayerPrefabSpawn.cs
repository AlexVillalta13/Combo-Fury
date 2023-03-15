using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefabSpawn : MonoBehaviour
{
    [SerializeField] GameObject PlayerPrefab;
    [SerializeField] GameEvent beginWalkEvent;
    GameObject instantiatedPlayerGameObject;

    private void OnEnable()
    {
        instantiatedPlayerGameObject = Instantiate(PlayerPrefab, transform.position, transform.rotation);
        beginWalkEvent.Raise();
    }

    private void OnDisable()
    {
        Destroy(instantiatedPlayerGameObject);
    }
}
