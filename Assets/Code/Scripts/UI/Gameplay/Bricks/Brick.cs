using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public enum BrickTypeEnum
{
    Redbrick,
    YellowBrick,
    Greenbrick,
    BlackBrick,
    SpeedBrick,
    ShieldBrick,
    FireBrick,
    HeavyAttackBrick
}

public enum BrickHolder
{
    PlayerBrick,
    EnemyBrick
}

public class Brick: MonoBehaviour
{
    protected BrickHolder brickHolder = BrickHolder.EnemyBrick;
    [SerializeField] protected BrickTypeEnum brickType = BrickTypeEnum.Redbrick;

    [SerializeField] protected BrickTypesSO brickTypesSO;
    [SerializeField] protected TouchBrickEventsSO brickEventsHolder;
    [SerializeField] protected VisualTreeAsset brickUIElement;
    protected BricksPool bricksPool;
    const string brickUSSClass = "brick";
    protected const string brickFlashClass = "brickFlash";
    const string scaleDownClass = "scaledDown";
    const string scaleDownALittleClass = "scaleDownALittle";
    protected const string scaleUpClass = "scaledUp";
    protected const string ignoreBrickWithTouchUSSClassName = "ignoreBrickWithTouch";
    protected const string yellowBrickUSSClassName = "yellowBrick";
    protected const string greenBrickUSSClassName = "greenBrick";
    const string enemyUSSClassName = "enemyBrick";
    const string playerUSSClassName = "playerBrick";

    protected CombatBarUI combatBarUI;

    protected VisualElement m_elementParent;
    protected VisualElement brickRootElementAttached;
    protected VisualElement brickElement;
    protected VisualElement iconElement;

    [SerializeField] protected float timeToAutoDelete = 0f;
    [SerializeField] protected float minWidth;
    [SerializeField] protected float maxWidth;
    [SerializeField] protected int hitsToDestroyBrick = 1;
    protected int currenHitsToDestroyBrick = 1;


    public Brick()
    {

    }

    public void SetupBrick(CombatBarUI combatBarUI, VisualElement playerElementParent, VisualElement enemyElementParent)
    {
        currenHitsToDestroyBrick = hitsToDestroyBrick;
        
        brickRootElementAttached.style.display = DisplayStyle.Flex;

        iconElement = brickRootElementAttached.Query<VisualElement>(name: "Icon").First();
        this.combatBarUI = combatBarUI;
        this.brickElement = brickRootElementAttached.Q(className: brickUSSClass);

        if (brickHolder == BrickHolder.PlayerBrick)
        {
            this.m_elementParent = playerElementParent;
            brickRootElementAttached.AddToClassList(playerUSSClassName);
        }
        else if(brickHolder == BrickHolder.EnemyBrick)
        {
            this.m_elementParent = enemyElementParent;
            brickRootElementAttached.AddToClassList(enemyUSSClassName);
        }

        brickRootElementAttached.style.visibility = Visibility.Hidden;
        m_elementParent.Add(brickRootElementAttached);
        brickRootElementAttached.style.position = Position.Absolute;

        float randomWidht = Random.Range(minWidth, maxWidth);
        brickRootElementAttached.style.width = randomWidht;

        brickElement.RegisterCallback<TransitionEndEvent>(OnChangeScaleEndEvent);

        StartCoroutine(WaitFrameAndPosition());
    }

    private IEnumerator WaitFrameAndPosition()
    {
        yield return new WaitForEndOfFrame();

        PositionBrick();
    }


    public virtual void PositionBrick()
    {
        brickRootElementAttached.style.visibility = Visibility.Visible;

        brickRootElementAttached.style.left = UnityEngine.Random.Range(m_elementParent.resolvedStyle.left, m_elementParent.resolvedStyle.left + m_elementParent.resolvedStyle.width - brickRootElementAttached.resolvedStyle.width);

        if(timeToAutoDelete > 0f)
        {
            StartCoroutine(AutoDestroyBrickElement());
        }

        ScaleUpUI();
        OnBrickPositioned();
    }

    protected virtual void OnBrickPositioned() { }

    public void SetPool(BricksPool brickPool)
    {
        this.bricksPool = brickPool;

        brickRootElementAttached = brickUIElement.Instantiate();
    }

    public float GetTimeToAutoDelete()
    {
        return timeToAutoDelete;
    }

    public virtual void EffectWithTouch() { }

    public VisualElement GetBrickElementAttached()
    {
        return brickRootElementAttached;
    }

    public virtual void RemoveBrickElement()
    {
        brickElement.UnregisterCallback<TransitionEndEvent>(OnChangeScaleEndEvent);
        brickRootElementAttached.style.display = DisplayStyle.None;

        brickRootElementAttached.RemoveFromClassList(ignoreBrickWithTouchUSSClassName);
        brickElement.RemoveFromClassList(brickFlashClass);

        combatBarUI.RemoveBrickFromDict(brickRootElementAttached);

        if (gameObject.activeSelf == true)
        {
            bricksPool.Pool.Release(this);
        }
    }

    public IEnumerator AutoDestroyBrickElement()
    {
        yield return new WaitForSeconds(timeToAutoDelete);
        RemoveBrickElement();
    }

    protected void ScaleUpUI()
    {
        brickElement.RemoveFromClassList(scaleDownClass);
        brickElement.RemoveFromClassList(scaleDownALittleClass);
        brickElement.AddToClassList(scaleUpClass);
    }

    protected void ScaleDownUI()
    {
        brickElement.RemoveFromClassList(scaleDownALittleClass);
        brickElement.RemoveFromClassList(scaleUpClass);
        brickElement.AddToClassList(scaleDownClass);
        brickRootElementAttached.AddToClassList(ignoreBrickWithTouchUSSClassName);
    }

    protected void ScaleDownALittleUI()
    {
        brickElement.RemoveFromClassList(scaleUpClass);
        brickElement.AddToClassList(scaleDownALittleClass);
    }

    protected void OnChangeScaleEndEvent(TransitionEndEvent evt)
    {
        foreach(StylePropertyName transitionName in evt.stylePropertyNames)
        {
            if(transitionName == "scale")
            {
                VisualElement element = evt.currentTarget as VisualElement;
                if (element.ClassListContains(scaleUpClass))
                {
                    OnScaledUp();
                }
                else if (element.ClassListContains(scaleDownClass))
                {
                    OnScaledDown();
                }
                else if(element.ClassListContains(scaleDownALittleClass))
                {
                    OnScaleDownALittle();
                }
            }
        }
    }

    protected virtual void OnScaledDown() { }

    protected virtual void OnScaledUp() { }

    protected void OnScaleDownALittle()
    {
        ScaleUpUI();
    }
}