using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "Brick Types")]
public class BrickTypesSO : ScriptableObject
{
    [SerializeField] List<BrickTypes> brickTypes = new List<BrickTypes>();
    public List<BrickTypes> BrickTypesList { get { return brickTypes; } }

    public BricksPool GetPool(BrickTypeEnum brickTypeEnum)
    {
        foreach (BrickTypes brick in brickTypes)
        {
            if (brick.BrickType == brickTypeEnum)
            {
                return brick.Pool;
            }
        }

        Debug.LogError("GetPool() - You are triyin to get a pool that doesn't exist");
        return null;
    }

    public BrickTypes GetBrick(BrickTypeEnum brickTypeEnum)
    {
        foreach (BrickTypes brick in brickTypes)
        {
            if (brick.BrickType == brickTypeEnum)
            {
                return brick;
            }
        }

        Debug.LogError("GetPool() - You are triyin to get a brick that doesn't exist");
        return null;
    }
}
[System.Serializable]
public class BrickTypes
{
    [SerializeField] BrickTypeEnum brickType;
    public BrickTypeEnum BrickType { get { return brickType; } }


    [SerializeField] VisualTreeAsset brickUIAsset;
    public VisualTreeAsset BrickUIAsset { get { return brickUIAsset; } }


    [SerializeField] Brick brickPrefab;
    public Brick BrickPrefab { get { return brickPrefab; } }

    [System.NonSerialized]
    public BricksPool Pool;
}