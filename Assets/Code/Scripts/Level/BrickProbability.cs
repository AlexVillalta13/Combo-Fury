using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BrickProbability
{
    public BrickProbability(BrickTypeEnum brickTypeEnum)
    {
        this.brickType = brickTypeEnum;
    }
    [SerializeField] private BrickTypeEnum brickType;
    public BrickTypeEnum BrickType => brickType;

    public float Probability = 0f;

    [SerializeField] private int levelToUnlock = 0;
    public int LevelToUnlock => levelToUnlock;
}