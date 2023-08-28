using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ShieldBrick : Brick
{
    [SerializeField] Sprite icon2WithHits;
    //[SerializeField] Sprite icon1WithHit;
    public ShieldBrick() : base()
    {
        brickHolder = BrickHolder.EnemyBrick;
        brickType = BrickTypeEnum.ShieldBrick;
    }

    private void OnEnable()
    {
        hitsToDestroyBrick = 3;
    }

    public override void EffectWithTouch()
    {
        base.EffectWithTouch();

        hitsToDestroyBrick--;
        if(hitsToDestroyBrick == 2)
        {
            brickEventsHolder.GetPlayerBlockEvent().Raise();
            brickRootElementAttached.Query<VisualElement>(name: "Icon").First().style.backgroundImage = new StyleBackground(icon2WithHits);
        }
        else if(hitsToDestroyBrick == 1)
        {
            brickEventsHolder.GetPlayerBlockEvent().Raise();
            brickRootElementAttached.Query<VisualElement>(name: "Icon").First().style.backgroundImage = null;
        }
        else if (hitsToDestroyBrick <= 0)
        {
            brickEventsHolder.GetPlayerBlockEvent().Raise();
            ScaleDownUI();
            brickElement.AddToClassList(brickFlashClass);
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
            bricksPool.ShieldBrickPool.Release(this);
        }
    }
}
