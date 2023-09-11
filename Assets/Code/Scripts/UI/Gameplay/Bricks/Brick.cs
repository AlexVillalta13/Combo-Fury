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
    FireBrick
}

public enum BrickHolder
{
    PlayerBrick,
    EnemyBrick
}

public class Brick: MonoBehaviour
{
    protected BrickHolder brickHolder = BrickHolder.EnemyBrick;
    protected BrickTypeEnum brickType = BrickTypeEnum.Redbrick;

    [SerializeField] protected BrickTypesSO brickTypesSO;
    [SerializeField] protected TouchBrickEventsSO brickEventsHolder;
    [SerializeField] protected VisualTreeAsset brickUIElement;
    [SerializeField] protected BricksPool bricksPool;
    const string brickUSSClass = "brick";
    protected const string brickFlashClass = "brickFlash";
    const string scaleDownClass = "scaledDown";
    const string scaleDownALittleClass = "scaleDownALittle";
    const string scaleUpClass = "scaledUp";
    protected const string ignoreBrickWithTouchUSSClassName = "ignoreBrickWithTouch";

    protected CombatBarUI combatBarUI;

    protected VisualElement m_elementParent;
    protected VisualElement brickRootElementAttached;
    protected VisualElement brickElement;

    [SerializeField] protected float timeToAutoDelete = 0f;
    [SerializeField] protected float minWidth;
    [SerializeField] protected float maxWidth;
    [SerializeField] protected int hitsToDestroyBrick = 1;

    public Brick()
    {

    }

    public void SetupBrick(CombatBarUI combatBarUI, VisualElement brickElementAttached, VisualElement playerElementParent, string playerClassName, VisualElement enemyElementParent, string enemyClassName)
    {
        this.combatBarUI = combatBarUI;
        this.brickRootElementAttached = brickElementAttached;
        this.brickElement = brickRootElementAttached.Q(className: brickUSSClass);

        if (brickHolder == BrickHolder.PlayerBrick)
        {
            this.m_elementParent = playerElementParent;
            brickElementAttached.AddToClassList(playerClassName);
        }
        else if(brickHolder == BrickHolder.EnemyBrick)
        {
            this.m_elementParent = enemyElementParent;
            brickElementAttached.AddToClassList(enemyClassName);
        }

        brickElementAttached.style.visibility = Visibility.Hidden;
        m_elementParent.Add(brickElementAttached);
        brickElementAttached.style.position = Position.Absolute;

        float randomWidht = Random.Range(minWidth, maxWidth);
        brickElementAttached.style.width = randomWidht;

        brickElement.RegisterCallback<TransitionEndEvent>(OnChangeScaleEndEvent);

        StartCoroutine(PositionBrick());
    }

    public virtual IEnumerator PositionBrick()
    {
        yield return new WaitForEndOfFrame();

        brickRootElementAttached.style.visibility = Visibility.Visible;

        brickRootElementAttached.style.left = UnityEngine.Random.Range(m_elementParent.resolvedStyle.left, m_elementParent.resolvedStyle.left + m_elementParent.resolvedStyle.width - brickRootElementAttached.resolvedStyle.width);

        if(timeToAutoDelete > 0f)
        {
            StartCoroutine(AutoDestroyBrickElement());
        }

        ScaleUpUI();
        OnBrickPositioned();
    }

    protected virtual void OnBrickPositioned()
    {

    }

    public void SetPool(BricksPool brickPool)
    {
        this.bricksPool = brickPool;
    }

    public float GetTimeToAutoDelete()
    {
        return timeToAutoDelete;
    }

    public virtual void EffectWithTouch()
    {

    }

    public VisualElement GetBrickElementAttached()
    {
        return brickRootElementAttached;
    }

    public virtual void RemoveBrickElement()
    {
        brickElement.UnregisterCallback<TransitionEndEvent>(OnChangeScaleEndEvent);
        brickRootElementAttached.RemoveFromHierarchy();

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

    protected virtual void OnScaledDown()
    {
    }

    protected virtual void OnScaledUp()
    {
    }

    protected void OnScaleDownALittle()
    {
        ScaleUpUI();
    }
}