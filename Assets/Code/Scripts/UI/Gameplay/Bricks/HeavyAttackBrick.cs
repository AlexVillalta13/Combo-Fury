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

    [SerializeField] GameEvent heavyAttackEvent;

    public override void EffectWithTouch()
    {
        base.EffectWithTouch();

        hitsToDestroyBrick--;
        if (hitsToDestroyBrick == 1)
        {
            brickElement.RemoveFromClassList(yellowBrickUSSClassName);
            brickElement.AddToClassList(greenBrickUSSClassName);
            ScaleDownALittleUI();
        }
        else if (hitsToDestroyBrick <= 0)
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
}
