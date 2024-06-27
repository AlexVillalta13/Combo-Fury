using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Pool;

public class BricksPool : MonoBehaviour
{
    [SerializeField] BrickTypeEnum brickTypeEnum;
    [SerializeField] BrickTypesSO BrickTypesSO;
    [SerializeField] Brick brickPrefab;

    private ObjectPool<Brick> pool;
    public ObjectPool<Brick> Pool => pool;

    private void Awake()
    {
        if (pool == null)
        {
            pool = new ObjectPool<Brick>(CreateBrickItem, OnTakeItemFromPool, OnReturnObjectToPool, defaultCapacity: 10);
        }

        PassThisReferenceToBrickTypesSO();
    }
    public void PassThisReferenceToBrickTypesSO()
    {
        foreach (BrickTypes brickType in BrickTypesSO.BrickTypesList)
        {
            if (brickType.BrickType == brickTypeEnum)
            {
                brickType.Pool = this;
                break;
            }
        }
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
