using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Pool;

public class BricksPool : MonoBehaviour
{
    [SerializeField] BrickTypeEnum brickTypeEnum;
    [SerializeField] BrickTypesSO BrickTypesSO;
    private ObjectPool<Brick> pool;
    [SerializeField] Brick brickPrefab;
    public ObjectPool<Brick> Pool
    {
        get
        {
            return pool;
        }
    }
    private void Awake()
    {
        if (pool == null)
        {
            pool = new ObjectPool<Brick>(CreateBrickItem, OnTakeItemFromPool, OnReturnObjectToPool, defaultCapacity: 10);
        }

        BrickTypesSO.SetBrickPool(brickTypeEnum, this);
    }

    private Brick CreateBrickItem()
    {
        Brick brickObject = Instantiate(brickPrefab);
        brickObject.SetPool(this);
        return brickObject;
    }

    private void OnTakeItemFromPool(Brick brickObject)
    {
        brickObject.gameObject.SetActive(true);
    }

    private void OnReturnObjectToPool(Brick brickObject)
    {
        brickObject.gameObject.SetActive(false);
    }
}
