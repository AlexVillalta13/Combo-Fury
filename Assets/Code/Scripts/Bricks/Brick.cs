using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public enum BrickTypeEnum
{
    Redbrick,
    YellowBrick,
    Greenbrick
}

public enum BrickHolder
{
    PlayerBrick,
    EnemyBrick
}

public class Brick
{
    protected BrickHolder brickHolder = BrickHolder.EnemyBrick;

    protected TouchBrickEventsSO brickEventsHolder;
    protected VisualElement brickElementAttached;
    protected VisualElement m_elementParent;

    protected float timeToAutoDelete = 0f;

    public Brick()
    {

    }

    public void SetupBrick(VisualElement brickElementAttached, VisualElement playerElementParent, string playerClassName, VisualElement enemyElementParent, string enemyClassName, TouchBrickEventsSO brickEventsHolder)
    {
        this.brickElementAttached = brickElementAttached;
        this.brickEventsHolder = brickEventsHolder;

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
    }

    public void PositionBrick()
    {
        brickElementAttached.style.visibility = Visibility.Visible;

        brickElementAttached.style.left = UnityEngine.Random.Range(m_elementParent.resolvedStyle.left, m_elementParent.resolvedStyle.left + m_elementParent.resolvedStyle.width - brickElementAttached.resolvedStyle.width);
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

    public void RemoveBrickElement()
    {
        brickElementAttached.RemoveFromHierarchy();
    }

    public IEnumerator AutoDestroyBrickElement()
    {
        yield return new WaitForSeconds(timeToAutoDelete);
        if(timeToAutoDelete > 0f)
        {
            RemoveBrickElement();
        }
    }
}
public class RedBrick : Brick
{
    public RedBrick() : base()
    {
        brickHolder = BrickHolder.EnemyBrick;
    }

    public override void EffectWithTouch()
    {
        base.EffectWithTouch();

        brickEventsHolder.GetPlayerBlockEvent().Raise();
        RemoveBrickElement();
    }
}
public class GreenBrick : Brick
{
    public GreenBrick() : base()
    {
        brickHolder = BrickHolder.PlayerBrick;
        timeToAutoDelete = 10f;
    }

    public override void EffectWithTouch()
    {
        base.EffectWithTouch();

        brickEventsHolder.GetPlayerCriticalAttackEvent().Raise();
        RemoveBrickElement();
    }
}
public class YellowBrick : Brick
{
    public YellowBrick() : base()
    {
        brickHolder = BrickHolder.PlayerBrick;
        timeToAutoDelete = 15f;
    }

    public override void EffectWithTouch()
    {
        base.EffectWithTouch();

        brickEventsHolder.GetPlayerAttackEvent().Raise();
        RemoveBrickElement();
    }
}