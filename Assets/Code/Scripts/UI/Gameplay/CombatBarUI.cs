using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CombatBarUI : UIComponent
{
    // string references
    private const string pointerCombarBarReferfence = "PointerCombatBar";
    private const string enemyBricksElementReference = "EnemyAttacks";
    private const string trapBricksElementReference = "TrapAttacks";
    private const string playerBrickElementReference = "PlayerAttacks";
    private const string enemyUSSClassName = "enemyBrick";
    private const string playerUSSClassName = "playerBrick";
    private const string ignoreBrickWithTouchUSSClassName = "ignoreBrickWithTouch";

    // visual elements references
    private VisualElement pointerCombatBar;
    private VisualElement enemyBricksElementHolder;
    private VisualElement trapBricksElementHolder;
    private VisualElement playerBrickElementHolder;

    [ShowInInspector]
    private Dictionary<VisualElement, Brick> bricksInBarDict = new Dictionary<VisualElement, Brick>();
    private List<VisualElement> enemyBricksList = new List<VisualElement>();
    private List<VisualElement> trapBricksList = new List<VisualElement>();
    private List<VisualElement> playerBricksList = new List<VisualElement>();
    private List<VisualElement> bricksInPosition = new List<VisualElement>();

    // states
    [SerializeField] private bool inCombat = false;



    [Header("Combat Bar Stats")]
    [SerializeField] private float pointerVelocity = 60f;
    private float pointerPercentPosition = 0f;


    [Header("Touch Event")]
    [SerializeField] private TouchBrickEventsSO touchBrickEventsHolder;

    [Header("SO Data")]
    [SerializeField] private BrickTypesSO brickTypesSO;
    
    public override void SetElementsReferences()
    {
        base.SetElementsReferences();
        pointerCombatBar = m_UIElement.Query<VisualElement>(pointerCombarBarReferfence);
        enemyBricksElementHolder = m_UIElement.Query<VisualElement>(enemyBricksElementReference);
        trapBricksElementHolder = m_UIElement.Query<VisualElement>(trapBricksElementReference);
        playerBrickElementHolder = m_UIElement.Query<VisualElement>(playerBrickElementReference);

        pointerCombatBar.style.left = Length.Percent(pointerPercentPosition);
    }

    public void InitializeBrick(Brick brickScriptToSpawn)
    {
        brickScriptToSpawn.SetupBrick(this, playerBrickElementHolder, enemyBricksElementHolder, trapBricksElementHolder);
        bricksInBarDict.Add(brickScriptToSpawn.GetBrickElementAttached(), brickScriptToSpawn);
        playerBricksList = playerBrickElementHolder.Query<VisualElement>(className: playerUSSClassName).ToList();
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

            enemyBricksList = enemyBricksElementHolder.Query<VisualElement>(className: enemyUSSClassName).ToList();
            trapBricksList  = trapBricksElementHolder.Query<VisualElement>(className: enemyUSSClassName).ToList();
            playerBricksList = playerBrickElementHolder.Query<VisualElement>(className: playerUSSClassName).ToList();
            bricksInPosition.Clear();

            foreach (VisualElement element in enemyBricksList)
            {
                if (pointerPos > element.resolvedStyle.left && pointerPos < element.resolvedStyle.left + element.resolvedStyle.width)
                {
                    bricksInPosition.Add(element);
                }
            }
            
            if (bricksInPosition.Count == 0)
            {
                foreach (VisualElement element in trapBricksList)
                {
                    if (pointerPos > element.resolvedStyle.left && pointerPos < element.resolvedStyle.left + element.resolvedStyle.width)
                    {
                        bricksInPosition.Add(element);
                    }
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
                touchBrickEventsHolder.GetPlayerIsHitEvent().Raise(this);
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
        playerBricksList = playerBrickElementHolder.Query<VisualElement>(className: playerUSSClassName).ToList();
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
        foreach (KeyValuePair<VisualElement, Brick> element in bricksInBarDict)
        {
            element.Value.RemoveBrickElement();
        }
        bricksInBarDict.Clear();
    }

    public int GetPlayerBricksInBar()
    {
        return playerBricksList.Count;
    }

    // private void SetupLevel(ILevelEnemyData level)
    // {
    //     this.levelSO = level;
    // }
}
