using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Pool;

public class BricksPool : MonoBehaviour
{
    [SerializeField] int initialNumbersToLoad = 3;

    private ObjectPool<Brick> redBrickPool;
    [SerializeField] Brick redBrickPrefab;
    public ObjectPool<Brick> RedBrickPool
    {
        get
        {
            return redBrickPool;
        }
    }

    private ObjectPool<Brick> yellowBrickPool;
    [SerializeField] Brick yellowBrickPrefab;
    public ObjectPool<Brick> YellowBrickPool
    {
        get
        {
            return yellowBrickPool;
        }
    }

    private ObjectPool<Brick> greenBrickPool;
    [SerializeField] Brick greenBrickPrefab;
    public ObjectPool<Brick> GreenBrickPool
    {
        get
        {
            return greenBrickPool;
        }
    }

    private ObjectPool<Brick> blackBrickPool;
    [SerializeField] Brick blackBrickPrefab;
    public ObjectPool<Brick> BlackBrickPool
    {
        get
        {
            return blackBrickPool;
        }
    }

    private ObjectPool<Brick> speedBrickPool;
    [SerializeField] Brick speedBrickPrefab;
    public ObjectPool<Brick> SpeedBrickPool
    {
        get
        {
            return speedBrickPool;
        }
    }
    private void Awake()
    {
        if (redBrickPool == null)
        {
            redBrickPool = new ObjectPool<Brick>(CreateRedBrickItem, OnTakeItemFromPool, OnReturnObjectToPool);
        }
        if(yellowBrickPool == null)
        {
            yellowBrickPool = new ObjectPool<Brick>(CreateYellowBrickItem, OnTakeItemFromPool, OnReturnObjectToPool);
        }
        if (greenBrickPool == null)
        {
            greenBrickPool = new ObjectPool<Brick>(CreateGreenBrickItem, OnTakeItemFromPool, OnReturnObjectToPool);
        }
        if (blackBrickPool == null)
        {
            blackBrickPool = new ObjectPool<Brick>(CreateBlackBrickItem, OnTakeItemFromPool, OnReturnObjectToPool);
        }
        if (speedBrickPool == null)
        {
            speedBrickPool = new ObjectPool<Brick>(CreateSpeedBrickItem, OnTakeItemFromPool, OnReturnObjectToPool);
        }
    }

    private Brick CreateRedBrickItem()
    {
        Brick brickObject = Instantiate(redBrickPrefab);
        brickObject.SetPool(this);
        return brickObject;
    }

    private Brick CreateYellowBrickItem()
    {
        Brick brickObject = Instantiate(yellowBrickPrefab);
        brickObject.SetPool(this);
        return brickObject;
    }

    private Brick CreateGreenBrickItem()
    {
        Brick brickObject = Instantiate(greenBrickPrefab);
        brickObject.SetPool(this);
        return brickObject;
    }

    private Brick CreateBlackBrickItem()
    {
        Brick brickObject = Instantiate(blackBrickPrefab);
        brickObject.SetPool(this);
        return brickObject;
    }

    private Brick CreateSpeedBrickItem()
    {
        Brick brickObject = Instantiate(speedBrickPrefab);
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
