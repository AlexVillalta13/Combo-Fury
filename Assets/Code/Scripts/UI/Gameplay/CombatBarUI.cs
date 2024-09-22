using Sirenix.OdinInspector;
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
    const string ignoreBrickWithTouchUSSClassName = "ignoreBrickWithTouch";

    // visual elements references
    VisualElement pointerCombatBar;
    VisualElement enemyBricksElementHolder;
    VisualElement playerBrickElementHolder;

    Dictionary<VisualElement, Brick> bricksInBarDict = new Dictionary<VisualElement, Brick>();

    // states
    [SerializeField] bool inCombat = false;



    [Header("Combat Bar Stats")]
    [SerializeField] float pointerVelocity = 60f;
    float pointerPercentPosition = 0f;


    [Header("Touch Event")]
    [SerializeField] TouchBrickEventsSO touchBrickEventsHolder;

    [Header("SO Data")]
    // private ILevelEnemyData levelSO;
    [SerializeField] BrickTypesSO brickTypesSO;

    // private void OnEnable()
    // {
    //     LevelSelectorUI.onSelectedLevelToPlay += SetupLevel;
    // }
    // private void OnDisable()
    // {
    //     LevelSelectorUI.onSelectedLevelToPlay -= SetupLevel;
    // }

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

    public void InitializeBrick(Brick brickScriptToSpawn)
    {
        brickScriptToSpawn.SetupBrick(this, playerBrickElementHolder, enemyBricksElementHolder);
        bricksInBarDict.Add(brickScriptToSpawn.GetBrickElementAttached(), brickScriptToSpawn);
    }

    public void MovePointer()
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
                touchBrickEventsHolder.GetPlayerIsHitEvent().Raise(gameObject);
                return;
            }

            VisualElement brickToBreack = null;
            foreach (VisualElement element in bricksInPosition)
            {
                if(element.ClassListContains(ignoreBrickWithTouchUSSClassName))
                {
                    continue;
                }
                if (element.parent.hierarchy.IndexOf(element) > element.parent.hierarchy.IndexOf(brickToBreack))
                {
                    brickToBreack = element;
                }
            }

            if(brickToBreack != null) 
            {
                bricksInBarDict[brickToBreack].EffectWithTouch();
            }
        }
    }

    public void RemoveBrickFromDict(VisualElement brick)
    {
        bricksInBarDict.Remove(brick);
    }

    public void InCombat()
    {
        inCombat = true;
        //CreateRandomTimeToSpawnBrick();
    }

    public void OutOfCombat()
    {
        inCombat = false;
        //currentEnemy++;
    }

    public void DeleteAllBricks()
    {
        bricksInBarDict.Clear();
    }

    // private void SetupLevel(ILevelEnemyData level)
    // {
    //     this.levelSO = level;
    // }
}
