using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBrick : Brick
{
    public FireBrick() : base()
    {
        brickHolder = BrickHolder.PlayerBrick;
        brickType = BrickTypeEnum.FireBrick;
    }

    [SerializeField] GameEvent fireAttackEvent;

    public override void EffectWithTouch()
    {
        base.EffectWithTouch();

        hitsToDestroyBrick--;
        if (hitsToDestroyBrick < 1)
        {
            fireAttackEvent.Raise(gameObject);
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
