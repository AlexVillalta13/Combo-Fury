using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Brick
{
    protected TouchBrickEventsSO brickEventsHolder;
    protected VisualElement brickElementAttached;

    protected float timeToAutoDelete = 0f;

    public Brick(VisualElement UIElement, TouchBrickEventsSO brickEventsHolder)
    {
        this.brickElementAttached = UIElement;
        this.brickEventsHolder = brickEventsHolder;
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
    public RedBrick(VisualElement UIElement, TouchBrickEventsSO brickEventsHolder) : base(UIElement, brickEventsHolder)
    {
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
    public GreenBrick(VisualElement UIElement, TouchBrickEventsSO brickEventsHolder) : base(UIElement, brickEventsHolder)
    {
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
    public YellowBrick(VisualElement UIElement, TouchBrickEventsSO brickEventsHolder) : base(UIElement, brickEventsHolder)
    {
        timeToAutoDelete = 15f;
    }

    public override void EffectWithTouch()
    {
        base.EffectWithTouch();

        brickEventsHolder.GetPlayerAttackEvent().Raise();
        RemoveBrickElement();
    }
}