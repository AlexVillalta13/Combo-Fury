using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ShieldBrick : Brick
{
    [SerializeField] int hits = 3;
    [SerializeField] Sprite icon2WithHits;
    //[SerializeField] Sprite icon1WithHit;
    public ShieldBrick() : base()
    {
        brickHolder = BrickHolder.EnemyBrick;
        brickType = BrickTypeEnum.ShieldBrick;
        hits = 3;
    }

    public override void EffectWithTouch()
    {
        base.EffectWithTouch();

        hits--;
        if(hits == 2)
        {
            brickEventsHolder.GetPlayerBlockEvent().Raise();
            brickElementAttached.Query<VisualElement>(name: "Icon").First().style.backgroundImage = new StyleBackground(icon2WithHits);
        }
        else if(hits == 1)
        {
            brickEventsHolder.GetPlayerBlockEvent().Raise();
            brickElementAttached.Query<VisualElement>(name: "Icon").First().style.backgroundImage = null;
        }
        else if (hits <= 0)
        {
            brickEventsHolder.GetPlayerBlockEvent().Raise();
            RemoveBrickElement();
        }
    }

    public override void RemoveBrickElement()
    {
        base.RemoveBrickElement();
        if (gameObject.activeSelf == true)
        {
            bricksPool.ShieldBrickPool.Release(this);
        }
    }
}
