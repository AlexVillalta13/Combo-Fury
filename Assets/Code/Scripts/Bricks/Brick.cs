using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Brick
{
    protected TouchBrickEventsSO brickEventsHolder;
    protected VisualElement brickElementAttached;

    public Brick(VisualElement UIElement, TouchBrickEventsSO brickEventsHolder)
    {
        this.brickElementAttached = UIElement;
        this.brickEventsHolder = brickEventsHolder;
    }

    public virtual void EffectWithTouch()
    {

    }

    public VisualElement GetBrickElementAttached()
    {
        return brickElementAttached;
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
        brickElementAttached.RemoveFromHierarchy();
    }
}
public class GreenBrick : Brick
{
    public GreenBrick(VisualElement UIElement, TouchBrickEventsSO brickEventsHolder) : base(UIElement, brickEventsHolder)
    {

    }

    public override void EffectWithTouch()
    {
        base.EffectWithTouch();

        brickEventsHolder.GetPlayerCriticalAttackEvent().Raise();
        brickElementAttached.RemoveFromHierarchy();
    }
}
public class YellowBrick : Brick
{
    public YellowBrick(VisualElement UIElement, TouchBrickEventsSO brickEventsHolder) : base(UIElement, brickEventsHolder)
    {

    }

    public override void EffectWithTouch()
    {
        base.EffectWithTouch();

        brickEventsHolder.GetPlayerAttackEvent().Raise();
        brickElementAttached.RemoveFromHierarchy();
    }
}