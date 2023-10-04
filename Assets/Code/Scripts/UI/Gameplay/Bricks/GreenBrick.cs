using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        hitsToDestroyBrick--;
        if (hitsToDestroyBrick < 1)
        {
            brickEventsHolder.GetPlayerCriticalAttackEvent().Raise(gameObject);
            ScaleDownUI();
            brickElement.AddToClassList(brickFlashClass);
        }
    }

    protected override void OnScaledDown()
    {
        base.OnScaledDown();
        RemoveBrickElement();
    }

    //public override void RemoveBrickElement()
    //{
    //    base.RemoveBrickElement();
    //    if (gameObject.activeSelf == true)
    //    {
    //        bricksPool.GreenBrickPool.Release(this);
    //    }
    //}
}
