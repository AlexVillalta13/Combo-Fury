using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackBrick : Brick
{
    public BlackBrick() : base()
    {
        brickHolder = BrickHolder.EnemyBrick;
        brickType = BrickTypeEnum.BlackBrick;
    }

    public override void EffectWithTouch()
    {
        base.EffectWithTouch();

        brickEventsHolder.GetPlayerIsHitEvent().Raise();
        RemoveBrickElement();
    }

    public override void RemoveBrickElement()
    {
        base.RemoveBrickElement();
        if (gameObject.activeSelf == true)
        {
            bricksPool.BlackBrickPool.Release(this);
        }
    }
}
