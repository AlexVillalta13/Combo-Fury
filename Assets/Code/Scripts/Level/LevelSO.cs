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
    public int health;
    public int attack;
}