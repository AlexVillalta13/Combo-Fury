using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SpeedBrick : Brick
{
    Tween tween;

    float combatBarPosition;
    [SerializeField] float minVelocity = 1.5f;
    [SerializeField] float maxVelocity = 1.75f;

    [SerializeField] Ease easeCurve = Ease.Linear;
    public SpeedBrick() : base()
    {
        brickHolder = BrickHolder.EnemyBrick;
        brickType = BrickTypeEnum.SpeedBrick;
    }

    public override IEnumerator PositionBrick()
    {
        yield return new WaitForEndOfFrame();

        brickRootElementAttached.style.visibility = Visibility.Visible;

        combatBarPosition = m_elementParent.resolvedStyle.left + m_elementParent.resolvedStyle.width - brickRootElementAttached.resolvedStyle.width / 2f;
        brickRootElementAttached.style.left = combatBarPosition;

        float velocity = Random.Range(minVelocity, maxVelocity);
        tween = DOTween.To(() => combatBarPosition, x => combatBarPosition = x, 0f - (brickRootElementAttached.resolvedStyle.width / 2f) -10f, velocity).SetEase(easeCurve);
    }

    private void Update()
    {
        TranslateBrick();
    }

    private void TranslateBrick()
    {
        brickRootElementAttached.style.left = combatBarPosition;
        if(combatBarPosition <= 0f - (brickRootElementAttached.resolvedStyle.width / 2f))
        {
            tween.Kill();
            brickEventsHolder.GetPlayerIsHitEvent().Raise();
            ScaleDownUI();
        }
    }

    public override void EffectWithTouch()
    {
        base.EffectWithTouch();

        hitsToDestroyBrick--;
        if (hitsToDestroyBrick < 1)
        {
            tween.Kill();
            brickEventsHolder.GetPlayerBlockEvent().Raise();
            RemoveBrickElement();
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
            bricksPool.SpeedBrickPool.Release(this);
        }
    }
}