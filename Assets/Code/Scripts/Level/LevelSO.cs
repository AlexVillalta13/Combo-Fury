using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "New Level")]
public class LevelSO : ScriptableObject
{
    [SerializeField] PlayerStatsSO inCombatStatsSO;

    [SerializeField] Sprite levelIcon;
    public Sprite LevelIcon { get { return levelIcon; } }

    [SerializeField] SceneEnum environment = SceneEnum.AlpineWoods;
    public SceneEnum Environment { get { return environment; } }

    [SerializeField] int initialPosition = 0;

    [SerializeField] BrickTypesSO brickTypes;

    [SerializeField] int minibossEncounterAt = 10;
    public int MinibossEncounterAt { get { return minibossEncounterAt; } }

    [SerializeField] List<EnemyData> enemies = new List<EnemyData>();

    public List<EnemyData> Enemies { get { return enemies; } set { enemies = value; } }

    private void OnEnable()
    {
        foreach(EnemyData enemy in Enemies)
        {
            if(enemy != null)
            {
                enemy.InCombatStatsSO = inCombatStatsSO;
            }
        }
    }
}



[System.Serializable]
public class BrickProbability
{
    public BrickProbability(BrickTypeEnum brickTypeEnum, float probability)
    {
        this.brickType = brickTypeEnum;
        this.probability = probability;
    }
    [SerializeField] BrickTypeEnum brickType;
    public BrickTypeEnum BrickType { get { return brickType; }}
    [SerializeField] float probability;
    public float Probability { get { return probability; } set { probability = value; } }
}