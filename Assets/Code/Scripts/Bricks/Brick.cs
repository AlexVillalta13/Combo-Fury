using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public enum BrickTypeEnum
{
    Redbrick,
    YellowBrick,
    Greenbrick,
    BlackBrick
}

public enum BrickHolder
{
    PlayerBrick,
    EnemyBrick
}

public class Brick
{
    protected BrickHolder brickHolder = BrickHolder.EnemyBrick;
    protected BrickTypeEnum brickType = BrickTypeEnum.Redbrick;

    protected BrickTypesSO brickTypesSO;
    protected TouchBrickEventsSO brickEventsHolder;
    protected VisualElement brickElementAttached;
    protected VisualElement m_elementParent;

    protected float timeToAutoDelete = 0f;
    protected float minWidth;
    protected float maxWidth;

    public Brick()
    {

    }

    public void SetupBrick(VisualElement brickElementAttached, VisualElement playerElementParent, string playerClassName, VisualElement enemyElementParent, string enemyClassName, TouchBrickEventsSO brickEventsHolder, BrickTypesSO brickTypesSO)
    {
        this.brickElementAttached = brickElementAttached;
        this.brickEventsHolder = brickEventsHolder;
        this.brickTypesSO = brickTypesSO;

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

        foreach(BrickTypes element in brickTypesSO.BrickTypes)
        {
            if(element.BrickType == brickType)
            {
                this.timeToAutoDelete = element.TimeToAutoDelete;
                this.minWidth = element.MinWidth;
                this.maxWidth = element.MaxWidth;
                float randomWidht = Random.Range(minWidth, maxWidth);
                brickElementAttached.style.width = randomWidht;
                break;
            }
        }
    }

    public void PositionBrick()
    {
        //float randomWidht = Random.Range(minWidth, maxWidth);
        //brickElementAttached.style.width = randomWidht;

        brickElementAttached.style.visibility = Visibility.Visible;
        //float randomWidht = Random.Range(minWidth, maxWidth);
        //brickElementAttached.style.width = randomWidht;
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
        brickType = BrickTypeEnum.Redbrick;
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
        brickType = BrickTypeEnum.Greenbrick;

        //timeToAutoDelete = 10f;
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
        brickType = BrickTypeEnum.YellowBrick;

        //timeToAutoDelete = 15f;
    }

    public override void EffectWithTouch()
    {
        base.EffectWithTouch();

        brickEventsHolder.GetPlayerAttackEvent().Raise();
        RemoveBrickElement();
    }
}

public class BlackBrick : Brick
{
    public BlackBrick() : base()
    {
        brickHolder = BrickHolder.EnemyBrick;
        brickType = BrickTypeEnum.BlackBrick;

        //timeToAutoDelete = 10f;
    }

    public override void EffectWithTouch()
    {
        base.EffectWithTouch();

        brickEventsHolder.GetPlayerIsHitEvent().Raise();
        RemoveBrickElement();
    }
}