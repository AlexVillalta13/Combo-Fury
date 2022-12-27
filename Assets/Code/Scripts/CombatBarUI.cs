using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CombatBarUI : MonoBehaviour
{
    VisualElement pointerCombatBar;
    UIDocument combatBarDocument;
    VisualElement enemyBricksElement;
    VisualElement playerBrickElement;

    [SerializeField] VisualTreeAsset yellowBrick;
    [SerializeField] VisualTreeAsset greenBrick;
    [SerializeField] VisualTreeAsset redBrick;
    Dictionary<VisualElement, Brick> bricksInBarDict = new Dictionary<VisualElement, Brick>();

    [SerializeField] TouchBrickEventsSO touchBrickEventsHolder;

    float pointerPercentPosition = 0f;
    [SerializeField] float pointerVelocity = 80f;

    [SerializeField] float minTimeToSpawnBrick = 3f;
    [SerializeField] float maxTimeSpawnBrick = 6f;
    float timeToSpawnBrick = 5f;
    float timerSpawn = 0f;

    private void Awake()
    {
        combatBarDocument = GetComponent<UIDocument>();
        pointerCombatBar = combatBarDocument.rootVisualElement.Query<VisualElement>("PointerCombatBar");
        enemyBricksElement = combatBarDocument.rootVisualElement.Query<VisualElement>("EnemyAttacks");
        playerBrickElement = combatBarDocument.rootVisualElement.Query<VisualElement>("PlayerAttacks");

        timeToSpawnBrick = UnityEngine.Random.Range(minTimeToSpawnBrick, maxTimeSpawnBrick);
    }

    void Update()
    {
        MovePointer();

        timerSpawn += Time.deltaTime;
        if(timerSpawn > timeToSpawnBrick)
        {
            timerSpawn = 0f;
            timeToSpawnBrick = UnityEngine.Random.Range(minTimeToSpawnBrick, maxTimeSpawnBrick);

            int randomBrickNumber = UnityEngine.Random.Range(0, 2);
            if(randomBrickNumber == 0)
            {
                SpawnBrick();
            }
            else if(randomBrickNumber == 1)
            {

            }
            else if(randomBrickNumber == 2)
            {

            }
        }
    }

    private void SpawnBrick()
    {
        TemplateContainer brickToAdd = redBrick.Instantiate();
        brickToAdd.style.visibility = Visibility.Hidden;
        enemyBricksElement.Add(brickToAdd);
        brickToAdd.AddToClassList("enemyBrick");
        brickToAdd.style.position = Position.Absolute;

        StartCoroutine(SpawnBrickCoroutine(brickToAdd));
    }

    private IEnumerator SpawnBrickCoroutine(TemplateContainer brickToAdd)
    {
        yield return new WaitForEndOfFrame();

        brickToAdd.style.visibility = Visibility.Visible;

        brickToAdd.style.left = UnityEngine.Random.Range(enemyBricksElement.resolvedStyle.left, enemyBricksElement.resolvedStyle.left + enemyBricksElement.resolvedStyle.width - brickToAdd.resolvedStyle.width);

        bricksInBarDict.Add(brickToAdd, new RedBrick(brickToAdd, touchBrickEventsHolder));
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
        float pointerPos = pointerCombatBar.resolvedStyle.left + pointerCombatBar.resolvedStyle.width/2f;
        List<VisualElement> enemyBricksList = new List<VisualElement>();
        enemyBricksList = enemyBricksElement.Query<VisualElement>(className: "enemyBrick").ToList();
        List<VisualElement> playerBricksList = new List<VisualElement>();
        playerBricksList = playerBrickElement.Query<VisualElement>(className: "playerBrick").ToList();
        List<VisualElement> bricksInPosition = new List<VisualElement>();
        foreach (VisualElement element in enemyBricksList)
        {
            if (pointerPos > element.resolvedStyle.left && pointerPos < element.resolvedStyle.left + element.resolvedStyle.width)
            {
                bricksInPosition.Add(element);
            }
        }
        if(bricksInPosition.Count == 0)
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

    public void RedBrickTestEvents()
    {
        Debug.Log("Red Brick: Event send");
    }
    public void YellowBrickTestEvents()
    {
        Debug.Log("Yellow Brick: Event send");
    }
    public void GreenBrickTestEvents()
    {
        Debug.Log("Green Brick: Event send");
    }
}
