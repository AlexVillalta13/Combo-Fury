using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefabSpawn : MonoBehaviour
{
    [SerializeField] GameObject PlayerPrefab;
    [SerializeField] GameEvent beginWalkEvent;
    GameObject instantiatedPlayerGameObject;
    GameplayCamera gameplayCamera;

    private void Awake()
    {
        gameplayCamera = FindObjectOfType<GameplayCamera>();
    }

    private void OnEnable()
    {
        instantiatedPlayerGameObject = Instantiate(PlayerPrefab, transform.position, transform.rotation);
        gameplayCamera.SetTarget(instantiatedPlayerGameObject.transform);
        beginWalkEvent.Raise();
    }

    private void OnDisable()
    {
        Destroy(instantiatedPlayerGameObject);
    }
}
