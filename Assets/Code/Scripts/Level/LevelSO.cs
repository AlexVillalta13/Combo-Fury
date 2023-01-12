using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level")]
public class LevelSO : ScriptableObject
{
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

    // Hacer el swicht aqu� y hacer una funcion que devuelva un Brick y se fabrique aqu�
    
}

[System.Serializable]
public struct BrickProbability
{
    [SerializeField] BrickType brickType;
    public BrickType BrickType { get { return brickType; }}
    [SerializeField] float probability;
    public float Probability { get { return probability; }}
}