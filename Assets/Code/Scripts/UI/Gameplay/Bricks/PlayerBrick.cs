using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerBrick : Brick
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
        if(hitsToDestroyBrick < 1)
        {
            TriggerEventBasedOnType();
            brickElement.AddToClassList(brickFlashClass);
            
            ScaleDownUI();
        }
    }

    private void TriggerEventBasedOnType()
    {
        switch (brickType)
        {
            case BrickTypeEnum.YellowBrick:
                brickEventsHolder.GetPlayerAttackEvent().Raise(this);
                break;

            case BrickTypeEnum.Greenbrick:
                brickEventsHolder.GetPlayerCriticalAttackEvent().Raise(this);
                break;
        }
    }

    protected override void OnScaledDown()
    {
        base.OnScaledDown();
        RemoveBrickElement();
        combatBarUI.RemoveBrickFromDict(brickRootElementAttached);
    }
}