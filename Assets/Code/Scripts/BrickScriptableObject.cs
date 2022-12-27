using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "NewBrick")]
public class BrickScriptableObject : ScriptableObject
{
    [SerializeField] VisualTreeAsset brickUIElement;
    [SerializeField] BrickTypes bricktype;
    [SerializeField] float brickChance;

    public VisualTreeAsset GetBrickUIElement()
    {
        return brickUIElement;
    }

    public Brick GetBrickType()
    {
        if(bricktype == BrickTypes.YellowBrick)
        {

        }

        return null;
    }

    public float GetBrickSpawnChance()
    {
        return brickChance;
    }
}

public enum BrickTypes
{
    YellowBrick,
    GreenBrick,
    RedBrick
}