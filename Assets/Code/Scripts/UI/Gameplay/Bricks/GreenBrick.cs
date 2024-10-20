using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GreenBrick : Brick
{
    
    protected override void SetVisualElementParent(VisualElement playerElementParent, VisualElement enemyElementParent, VisualElement trapElementParent)
    {
        this.m_elementParent = playerElementParent;
        brickRootElementAttached.AddToClassList(playerUSSClassName);
    }

    public override void EffectWithTouch()
    {
        base.EffectWithTouch();

        hitsToDestroyBrick--;
        if (hitsToDestroyBrick < 1)
        {
            brickEventsHolder.GetPlayerCriticalAttackEvent().Raise(gameObject);
            brickElement.AddToClassList(brickFlashClass);

            ScaleDownUI();
        }
    }

    protected override void OnScaledDown()
    {
        base.OnScaledDown();
        RemoveBrickElement();
        combatBarUI.RemoveBrickFromDict(brickRootElementAttached);
    }
}
