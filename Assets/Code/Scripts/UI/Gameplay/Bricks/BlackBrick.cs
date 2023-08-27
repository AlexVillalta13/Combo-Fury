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

        hitsToDestroyBrick--;
        if (hitsToDestroyBrick < 1)
        {
            brickEventsHolder.GetPlayerIsHitEvent().Raise();
            ScaleDownUI();
        }
    }

    protected override void OnScaledDown()
    {
        base.OnScaledDown();
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
