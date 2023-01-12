using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "New Level")]
public class LevelSO : ScriptableObject
{
    [SerializeField] BrickTypesSO brickTypes;

    [SerializeField] List<Enemy> enemies = new List<Enemy>();

    public List<Enemy> Enemies { get { return enemies; } }
}

[System.Serializable]
public class Enemy
{
    [SerializeField] int health;
    public int Health { get { return health; }}
    [SerializeField] int attack;
    public int Attack { get { return attack; }}
    [SerializeField] List <BrickProbability> enemyBricks = new List<BrickProbability>();
    public List<BrickProbability> EnemyBricks { get { return enemyBricks; } }

    public BrickTypeEnum GetRandomBrick()
    {
        return BrickTypeEnum.YellowBrick;
    }

    // Hacer el swicht aquí y hacer una funcion que devuelva un Brick y se fabrique aquí
    
}

[System.Serializable]
public struct BrickProbability
{
    [SerializeField] BrickTypeEnum brickType;
    public BrickTypeEnum BrickType { get { return brickType; }}
    [SerializeField] float probability;
    public float Probability { get { return probability; }}
}