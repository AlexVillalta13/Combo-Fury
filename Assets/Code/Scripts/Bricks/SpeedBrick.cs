using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SpeedBrick : Brick
{
    public SpeedBrick() : base()
    {
        brickHolder = BrickHolder.EnemyBrick;
        brickType = BrickTypeEnum.SpeedBrick;
    }

    public override void PositionBrick()
    {
        brickElementAttached.style.visibility = Visibility.Visible;

        brickElementAttached.style.left = m_elementParent.resolvedStyle.left + m_elementParent.resolvedStyle.width - brickElementAttached.resolvedStyle.width;
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
        bricksPool.SpeedBrickPool.Release(this);
    }
}