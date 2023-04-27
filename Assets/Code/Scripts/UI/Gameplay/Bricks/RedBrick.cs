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

        brickEventsHolder.GetPlayerBlockEvent().Raise();
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
