using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CombatBarUI : UIComponent
{
    // string references
    const string pointerCombarBarReferfence = "PointerCombatBar";
    const string enemyBricksElementReference = "EnemyAttacks";
    const string playerBrickElementReference = "PlayerAttacks";
    const string enemyUSSClassName = "enemyBrick";
    const string playerUSSClassName = "playerBrick";

    // visual elements references
    VisualElement pointerCombatBar;
    VisualElement enemyBricksElementHolder;
    VisualElement playerBrickElementHolder;

    Dictionary<VisualElement, Brick> bricksInBarDict = new Dictionary<VisualElement, Brick>();


    // states
    [SerializeField] bool inCombat = false;



    [Header("Combat Bar Stats")]
    [SerializeField] float pointerVelocity = 60f;

    [SerializeField] float minTimeToSpawnBrick = 3f;
    [SerializeField] float maxTimeSpawnBrick = 6f;
    [SerializeField] float timeToSpawnBrick = 5f;
    [SerializeField] float timerSpawn = 0f;

    float pointerPercentPosition = 0f;

    [Header("Touch Event")]
    [SerializeField] TouchBrickEventsSO touchBrickEventsHolder;

    [Header("SO Data")]
    [SerializeField] LevelSO levelSO;
    [SerializeField] BrickTypesSO brickTypesSO;
    int currentEnemy = 0;

    private void OnEnable()
    {
        LevelSelectorUI.loadLevel += SetupLevel;
    }
    private void OnDisable()
    {
        LevelSelectorUI.loadLevel -= SetupLevel;
    }

    public override void Awake()
    {
        base.Awake();
    }

    public override void SetElementsReferences()
    {
        base.SetElementsReferences();
        pointerCombatBar = m_UIElement.Query<VisualElement>(pointerCombarBarReferfence);
        enemyBricksElementHolder = m_UIElement.Query<VisualElement>(enemyBricksElementReference);
        playerBrickElementHolder = m_UIElement.Query<VisualElement>(playerBrickElementReference);

        pointerCombatBar.style.left = Length.Percent(pointerPercentPosition);
    }

    void Update()
    {
        if(inCombat == true)
        {
            MovePointer();

            timerSpawn += Time.deltaTime;
            if (timerSpawn > timeToSpawnBrick)
            {
                timerSpawn = 0f;
                CreateRandomTimeToSpawnBrick();

                GetRandomBrickFromSO();
            }
        }
    }

    private void GetRandomBrickFromSO()
    {
        BrickTypeEnum brickTypeToSpawn = levelSO.Enemies[currentEnemy].GetRandomBrick();

        VisualElement visualElement = null;
        foreach (BrickTypes brickTypeInSO in brickTypesSO.BrickTypes)
        {
            if (brickTypeInSO.BrickType == brickTypeToSpawn)
            {
                visualElement = brickTypeInSO.BrickUIAsset.Instantiate();
                break;
            }
        }
        if (visualElement == null)
        {
            Debug.LogError("CombatBarUI class: brick visual element to instantiate is null");
        }

        switch (brickTypeToSpawn)
        {
            case BrickTypeEnum.Redbrick:
                SpawnBrick(new RedBrick(), visualElement);
                break;
            case BrickTypeEnum.YellowBrick:
                SpawnBrick(new YellowBrick(), visualElement);
                break;
            case BrickTypeEnum.Greenbrick:
                SpawnBrick(new GreenBrick(), visualElement);
                break;
            case BrickTypeEnum.BlackBrick:
                SpawnBrick(new BlackBrick(), visualElement);
                break;
            case BrickTypeEnum.SpeedBrick:
                SpawnBrick(new SpeedBrick(), visualElement);
                break;

        }
    }

    private void SpawnBrick(Brick brickScriptToSpawn, VisualElement brickUIElement)
    {
        StartCoroutine(SpawnBrickCoroutine(brickScriptToSpawn, brickUIElement));
    }

    private IEnumerator SpawnBrickCoroutine(Brick brickScriptToSpawn, VisualElement brickUIElement)
    {
        brickScriptToSpawn.SetupBrick(brickUIElement, playerBrickElementHolder, playerUSSClassName, enemyBricksElementHolder, enemyUSSClassName, touchBrickEventsHolder, brickTypesSO);

        yield return new WaitForEndOfFrame();

        brickScriptToSpawn.PositionBrick();
        bricksInBarDict.Add(brickUIElement, brickScriptToSpawn);

        if(brickScriptToSpawn.GetTimeToAutoDelete() > 0f)
        {
            StartCoroutine(brickScriptToSpawn.AutoDestroyBrickElement());
        }
    }

    private void MovePointer()
    {
        pointerPercentPosition += Time.deltaTime * pointerVelocity;
        if (pointerPercentPosition > 100f)
        {
            pointerPercentPosition = 100f;
            pointerVelocity *= -1f;
        }
        else if (pointerPercentPosition < 0f)
        {
            pointerPercentPosition = 0f;
            pointerVelocity *= -1f;
        }

        pointerCombatBar.style.left = Length.Percent(pointerPercentPosition);
    }

    public void Touch()
    {
        if(inCombat == true)
        {
            float pointerPos = pointerCombatBar.resolvedStyle.left + pointerCombatBar.resolvedStyle.width / 2f;

            List<VisualElement> enemyBricksList = enemyBricksElementHolder.Query<VisualElement>(className: enemyUSSClassName).ToList();
            List<VisualElement> playerBricksList = playerBrickElementHolder.Query<VisualElement>(className: playerUSSClassName).ToList();
            List<VisualElement> bricksInPosition = new List<VisualElement>();

            foreach (VisualElement element in enemyBricksList)
            {
                if (pointerPos > element.resolvedStyle.left && pointerPos < element.resolvedStyle.left + element.resolvedStyle.width)
                {
                    bricksInPosition.Add(element);
                }
            }
            if (bricksInPosition.Count == 0)
            {
                foreach (VisualElement element in playerBricksList)
                {
                    if (pointerPos > element.resolvedStyle.left && pointerPos < element.resolvedStyle.left + element.resolvedStyle.width)
                    {
                        bricksInPosition.Add(element);
                    }
                }
            }

            if (bricksInPosition.Count == 0)
            {
                touchBrickEventsHolder.GetPlayerIsHitEvent().Raise();
                return;
            }

            VisualElement brickToBreack = bricksInPosition[0];
            foreach (VisualElement element in bricksInPosition)
            {
                if (element.parent.hierarchy.IndexOf(element) > element.parent.hierarchy.IndexOf(brickToBreack))
                {
                    brickToBreack = element;
                }
            }
            bricksInBarDict[brickToBreack].EffectWithTouch();
        }
    }

    public void InCombat()
    {
        inCombat = true;
        CreateRandomTimeToSpawnBrick();
    }

    public void FinishCombat()
    {
        inCombat = false;
        currentEnemy++;

        foreach(Brick brick in bricksInBarDict.Values)
        {
            brick.RemoveBrickElement();
        }
        bricksInBarDict.Clear();
    }

    public void StartLevel()
    {
        currentEnemy = 0;
        CreateRandomTimeToSpawnBrick();
    }

    private void CreateRandomTimeToSpawnBrick()
    {
        minTimeToSpawnBrick = levelSO.Enemies[currentEnemy].MinTimeToSpawnBrick;
        maxTimeSpawnBrick = levelSO.Enemies[currentEnemy].MaxTimeToSpawnBrick;
        timeToSpawnBrick = UnityEngine.Random.Range(levelSO.Enemies[currentEnemy].MinTimeToSpawnBrick, levelSO.Enemies[currentEnemy].MaxTimeToSpawnBrick);
    }

    private void SetupLevel(LevelSO level)
    {
        this.levelSO = level;
    }
}
