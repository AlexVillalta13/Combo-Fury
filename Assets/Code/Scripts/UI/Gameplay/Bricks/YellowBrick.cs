using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public override void RemoveBrickElement()
    {
        base.RemoveBrickElement();
        bricksPool.YellowBrickPool.Release(this);
        //Destroy(gameObject);
    }
}