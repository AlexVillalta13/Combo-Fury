using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Brick: MonoBehaviour
{
    protected TouchBrickEventsSO brickEventsHolder;
    protected VisualElement brickElementAttached;

    protected float timeToAutoDelete = 0f;

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

    public void RemoveBrickElement()
    {
        brickElementAttached.RemoveFromHierarchy();
    }

    public void AutoDestroyBrickElement()
    {
        RemoveBrickElement();
    }
}
public class RedBrick : Brick
{
    public RedBrick(VisualElement UIElement, TouchBrickEventsSO brickEventsHolder) : base(UIElement, brickEventsHolder)
    {

        timeToAutoDelete = 20f;
        //Invoke(nameof(AutoDestroyBrickElement), timeToAutoDelete);
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
        //Invoke(nameof(AutoDestroyBrickElement), timeToAutoDelete);

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
        //Invoke(nameof(AutoDestroyBrickElement), timeToAutoDelete);

    }

    public override void EffectWithTouch()
    {
        base.EffectWithTouch();

        brickEventsHolder.GetPlayerAttackEvent().Raise();
        RemoveBrickElement();
    }

}