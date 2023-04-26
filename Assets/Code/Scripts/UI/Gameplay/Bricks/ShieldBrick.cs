using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBrick : Brick
{
    [SerializeField] int hits = 3;
    [SerializeField] Sprite icon2WithHits;
    [SerializeField] Sprite icon1WithHit;
    public ShieldBrick() : base()
    {
        brickHolder = BrickHolder.EnemyBrick;
        brickType = BrickTypeEnum.ShieldBrick;
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
        bricksPool.ShieldBrickPool.Release(this);
    }
}
