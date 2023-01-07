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
    VisualElement enemyBricksElement;
    VisualElement playerBrickElement;

    // states
    bool inCombat = false;

    [Header("Brick UI Assets")]
    [SerializeField] VisualTreeAsset yellowBrick;
    [SerializeField] VisualTreeAsset greenBrick;
    [SerializeField] VisualTreeAsset redBrick;
    Dictionary<VisualElement, Brick> bricksInBarDict = new Dictionary<VisualElement, Brick>();

    float pointerPercentPosition = 0f;
    [Header("Combat Bar Stats")]
    [SerializeField] float pointerVelocity = 80f;

    [SerializeField] float minTimeToSpawnBrick = 3f;
    [SerializeField] float maxTimeSpawnBrick = 6f;
    [SerializeField] float timeToSpawnBrick = 5f;
    [SerializeField] float timerSpawn = 0f;
    [Header("Touch Event")]
    [SerializeField] TouchBrickEventsSO touchBrickEventsHolder;


    public override void Awake()
    {
        base.Awake();

        timeToSpawnBrick = UnityEngine.Random.Range(minTimeToSpawnBrick, maxTimeSpawnBrick);
    }

    public override void SetElementsReferences()
    {
        base.SetElementsReferences();
        pointerCombatBar = m_UIElement.Query<VisualElement>(pointerCombarBarReferfence);
        enemyBricksElement = m_UIElement.Query<VisualElement>(enemyBricksElementReference);
        playerBrickElement = m_UIElement.Query<VisualElement>(playerBrickElementReference);
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
                timeToSpawnBrick = UnityEngine.Random.Range(minTimeToSpawnBrick, maxTimeSpawnBrick);

                // TO DO: RANDOM SPAWN SYSTEM

                int randomBrickNumber = UnityEngine.Random.Range(0, 3);
                if (randomBrickNumber == 0)
                {
                    SpawnBrick(new RedBrick(redBrick.Instantiate(), touchBrickEventsHolder), enemyBricksElement, enemyUSSClassName);
                }
                else if (randomBrickNumber == 1)
                {
                    SpawnBrick(new GreenBrick(greenBrick.Instantiate(), touchBrickEventsHolder), playerBrickElement, playerUSSClassName);
                }
                else if (randomBrickNumber == 2)
                {
                    SpawnBrick(new YellowBrick(yellowBrick.Instantiate(), touchBrickEventsHolder), playerBrickElement, playerUSSClassName);
                }
            }
        }
    }

    private void SpawnBrick(Brick brickScriptToSpawn, VisualElement visualElementToParentWith, string className)
    {
        StartCoroutine(SpawnBrickCoroutine(brickScriptToSpawn, brickScriptToSpawn.GetBrickElementAttached(), visualElementToParentWith, className));
    }

    private IEnumerator SpawnBrickCoroutine(Brick brickScriptToSpawn, VisualElement brickVisualElementToAdd, VisualElement visualElementToParentWith, string className)
    {
        brickVisualElementToAdd.style.visibility = Visibility.Hidden;
        visualElementToParentWith.Add(brickVisualElementToAdd);
        brickVisualElementToAdd.AddToClassList(className);
        brickVisualElementToAdd.style.position = Position.Absolute;

        yield return new WaitForEndOfFrame();

        brickVisualElementToAdd.style.visibility = Visibility.Visible;

        brickVisualElementToAdd.style.left = UnityEngine.Random.Range(visualElementToParentWith.resolvedStyle.left, visualElementToParentWith.resolvedStyle.left + visualElementToParentWith.resolvedStyle.width - brickVisualElementToAdd.resolvedStyle.width);

        bricksInBarDict.Add(brickVisualElementToAdd, brickScriptToSpawn);

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
            List<VisualElement> enemyBricksList = new List<VisualElement>();
            enemyBricksList = enemyBricksElement.Query<VisualElement>(className: enemyUSSClassName).ToList();
            List<VisualElement> playerBricksList = new List<VisualElement>();
            playerBricksList = playerBrickElement.Query<VisualElement>(className: playerUSSClassName).ToList();
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
    }

    public void FinishCombat()
    {
        inCombat = false;

        pointerPercentPosition = 0f;

        foreach(Brick brick in bricksInBarDict.Values)
        {
            brick.RemoveBrickElement();
        }
        bricksInBarDict.Clear();
    }
}
