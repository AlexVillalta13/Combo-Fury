using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefabSpawn : MonoBehaviour
{
    [SerializeField] GameObject PlayerPrefab;
    GameObject instantiatedPlayerGameObject;

    private void OnEnable()
    {
        instantiatedPlayerGameObject = Instantiate(PlayerPrefab, transform.position, transform.rotation);
    }

    private void OnDisable()
    {
        Destroy(instantiatedPlayerGameObject);
    }
}
