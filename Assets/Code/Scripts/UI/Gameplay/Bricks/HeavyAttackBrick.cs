using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyAttackBrick : Brick
{
    public HeavyAttackBrick() : base()
    {
        brickHolder = BrickHolder.PlayerBrick;
        brickType = BrickTypeEnum.HeavyAttackBrick;
    }

    [SerializeField] GameEvent fireAttackEvent;

    public override void EffectWithTouch()
    {
        base.EffectWithTouch();

        hitsToDestroyBrick--;
        if (hitsToDestroyBrick < 1)
        {
            // Effect when touched
        }
    }

    protected override void OnScaledDown()
    {
        base.OnScaledDown();
        RemoveBrickElement();
    }
}
