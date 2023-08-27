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

        hitsToDestroyBrick--;
        if(hitsToDestroyBrick < 1)
        {
            brickEventsHolder.GetPlayerAttackEvent().Raise();
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
            bricksPool.YellowBrickPool.Release(this);
        }
    }
}