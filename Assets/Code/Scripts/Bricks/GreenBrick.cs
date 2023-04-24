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

        brickEventsHolder.GetPlayerCriticalAttackEvent().Raise();
        RemoveBrickElement();
    }

    public override void RemoveBrickElement()
    {
        base.RemoveBrickElement();
        bricksPool.GreenBrickPool.Release(this);
    }
}
