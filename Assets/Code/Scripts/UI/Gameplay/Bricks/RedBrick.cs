using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        hitsToDestroyBrick--;
        if (hitsToDestroyBrick < 1)
        {
            brickEventsHolder.GetPlayerBlockEvent().Raise();
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
        if(gameObject.activeSelf == true )
        {
            bricksPool.RedBrickPool.Release(this);
        }
    }
}
