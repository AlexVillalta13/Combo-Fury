using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefabSpawn : MonoBehaviour
{
    [SerializeField] GameObject PlayerPrefab;
    [SerializeField] GameEvent beginWalkEvent;
    GameObject instantiatedPlayerGameObject;
    GameplayCamera gameplayCamera;
    bool canDoFirstWalk = false;

    private void Awake()
    {
        gameplayCamera = FindObjectOfType<GameplayCamera>();
    }

    private void OnEnable()
    {
        instantiatedPlayerGameObject = Instantiate(PlayerPrefab, transform.position, transform.rotation);
        gameplayCamera.SetTarget(instantiatedPlayerGameObject.transform);
    }

    private void OnDisable()
    {
        Destroy(instantiatedPlayerGameObject);
    }

    public void CanWalk()
    {
        canDoFirstWalk = true;
    }

    public void BeginWalk()
    {
        if(canDoFirstWalk)
        {
            beginWalkEvent.Raise();
            canDoFirstWalk = false;
        }
    }
}
