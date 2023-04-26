using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public enum BrickTypeEnum
{
    Redbrick,
    YellowBrick,
    Greenbrick,
    BlackBrick,
    SpeedBrick,
    ShieldBrick
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

    protected VisualElement brickElementAttached;
    protected VisualElement m_elementParent;

    [SerializeField] protected float timeToAutoDelete = 0f;
    [SerializeField] protected float minWidth;
    [SerializeField] protected float maxWidth;

    public Brick()
    {

    }

    public void SetupBrick(VisualElement brickElementAttached, VisualElement playerElementParent, string playerClassName, VisualElement enemyElementParent, string enemyClassName)
    {
        this.brickElementAttached = brickElementAttached;

        if(brickHolder == BrickHolder.PlayerBrick)
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

        StartCoroutine(PositionBrick());
    }

    public virtual IEnumerator PositionBrick()
    {
        yield return new WaitForEndOfFrame();

        brickElementAttached.style.visibility = Visibility.Visible;

        brickElementAttached.style.left = UnityEngine.Random.Range(m_elementParent.resolvedStyle.left, m_elementParent.resolvedStyle.left + m_elementParent.resolvedStyle.width - brickElementAttached.resolvedStyle.width);

        if(timeToAutoDelete > 0f)
        {
            StartCoroutine(AutoDestroyBrickElement());
        }
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
        return brickElementAttached;
    }

    public virtual void RemoveBrickElement()
    {
        brickElementAttached.RemoveFromHierarchy();
    }

    public IEnumerator AutoDestroyBrickElement()
    {
        yield return new WaitForSeconds(timeToAutoDelete);
        RemoveBrickElement();
    }
}