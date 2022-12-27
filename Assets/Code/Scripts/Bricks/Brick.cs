using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Brick
{
    TouchBrickEventsSO brickEventsHolder;
    VisualElement brickElementAttached;

    public Brick(VisualElement UIElement, TouchBrickEventsSO brickEventsHolder)
    {
        this.brickElementAttached = UIElement;
        this.brickEventsHolder = brickEventsHolder;
    }

    public void EffectWithTouch()
    {
        brickEventsHolder.GetPlayerAttackEvent().Raise();
        brickElementAttached.RemoveFromHierarchy();
    }
}
public class RedBrick : Brick
{
    public RedBrick(VisualElement UIElement, TouchBrickEventsSO brickEventsHolder) : base(UIElement, brickEventsHolder)
    {

    }

    //public void EffectWithTouch()
    //{
    //    base.EffectWithTouch();
    //}
}
