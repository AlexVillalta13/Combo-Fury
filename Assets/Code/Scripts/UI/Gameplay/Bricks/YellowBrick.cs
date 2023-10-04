using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowBrick : Brick
{
    public YellowBrick() : base()
    {
        brickHolder = BrickHolder.PlayerBrick;
        brickType = BrickTypeEnum.YellowBrick;
    }

    public override void EffectWithTouch()
    {
        base.EffectWithTouch();

        hitsToDestroyBrick--;
        if(hitsToDestroyBrick < 1)
        {
            brickEventsHolder.GetPlayerAttackEvent().Raise(gameObject);
            ScaleDownUI();
            brickElement.AddToClassList(brickFlashClass);
        }
    }

    protected override void OnScaledDown()
    {
        base.OnScaledDown();
        RemoveBrickElement();
    }
}