using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using DG.Tweening;
using UnityEngine.Serialization;


public class RedBrick : Brick
{
    [SerializeField] Sprite shieldSprite;
    [SerializeField] Sprite shieldBrokenSprite;
    
    Tween tween;

    float MovingBrickPositionInBar;
    [SerializeField] float minTimeToCompletePath = 3f;
    [SerializeField] float maxTimeToCompletePath = 5f;

    [SerializeField] Ease easeCurve = Ease.Linear;

    protected override void SetVisualElementParent(VisualElement playerElementParent, VisualElement enemyElementParent, VisualElement trapElementParent)
    {
        this.m_elementParent = enemyElementParent;
        brickRootElementAttached.AddToClassList(enemyUSSClassName);
    }

    public override void PositionBrick()
    {
        if (hitsToDestroyBrick == 3)
        {
            iconElement.style.backgroundImage = new StyleBackground(shieldSprite);
        }
        
        brickRootElementAttached.style.visibility = Visibility.Visible;

        MovingBrickPositionInBar = m_elementParent.resolvedStyle.left + m_elementParent.resolvedStyle.width - brickRootElementAttached.resolvedStyle.width / 2f;
        brickRootElementAttached.style.left = MovingBrickPositionInBar;

        float velocity = Random.Range(minTimeToCompletePath, maxTimeToCompletePath);
        tween = DOTween.To(() => MovingBrickPositionInBar, x=> MovingBrickPositionInBar = x, 0f - (brickRootElementAttached.resolvedStyle.width / 2f) -10f, velocity).SetEase(easeCurve);
        
        ScaleUpUI();
    }
    
    private void Update()
    {
        if (brickElement.ClassListContains(scaleUpClass))
        {
            TranslateBrick();
        }
    }

    private void TranslateBrick()
    {
        brickRootElementAttached.style.left = MovingBrickPositionInBar;
        if(MovingBrickPositionInBar <= 0f - (brickRootElementAttached.resolvedStyle.width / 2f))
        {
            tween.Kill();
            brickEventsHolder.GetPlayerIsHitEvent().Raise(gameObject);
            ScaleDownUI();
        }
    }

    public override void EffectWithTouch()
    {
        base.EffectWithTouch();

        currenHitsToDestroyBrick--;
        if(currenHitsToDestroyBrick == 2)
        {
            brickEventsHolder.GetPlayerBlockEvent().Raise(gameObject);
            iconElement.style.backgroundImage = new StyleBackground(shieldBrokenSprite);
            ScaleDownALittleUI();
        }
        else if(currenHitsToDestroyBrick == 1)
        {
            brickEventsHolder.GetPlayerBlockEvent().Raise(gameObject);
            iconElement.style.backgroundImage = null;
            ScaleDownALittleUI();
        }
        else if (currenHitsToDestroyBrick <= 0)
        {
            tween.Kill();

            brickEventsHolder.GetPlayerBlockEvent().Raise(gameObject);
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
