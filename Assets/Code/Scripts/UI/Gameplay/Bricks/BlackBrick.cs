using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BlackBrick : Brick
{
    Tween tween;
    Vector3 vec3Shake;
    float blackBrickPositionInBar;
    Vector3 shakeStrength = new Vector3(50f, 0f, 0f);
    bool isShaking = false;

    public BlackBrick() : base()
    {
        brickHolder = BrickHolder.EnemyBrick;
        brickType = BrickTypeEnum.BlackBrick;
    }

    protected override void OnBrickPositioned()
    {
        base.OnBrickPositioned();
        blackBrickPositionInBar = brickRootElementAttached.style.left.value.value;
        vec3Shake.x = blackBrickPositionInBar;
    }

    private void Update()
    {
        if (isShaking == true)
        {
            blackBrickPositionInBar = vec3Shake.x;
            brickRootElementAttached.style.left = blackBrickPositionInBar;
        }
    }

    public override void EffectWithTouch()
    {
        base.EffectWithTouch();

        hitsToDestroyBrick--;
        if (hitsToDestroyBrick < 1)
        {
            brickRootElementAttached.AddToClassList(ignoreBrickWithTouchUSSClassName);
            brickEventsHolder.GetPlayerIsHitEvent().Raise();
            ShakeBrick();
        }
    }


    private void ShakeBrick()
    {
        tween = DOTween.Shake(() => vec3Shake, x => vec3Shake = x, 0.25f, shakeStrength, 100);
        tween.onComplete += RemoveBrickElement;
        isShaking = true;
    }

    public override void RemoveBrickElement()
    {
        tween.Kill();
        base.RemoveBrickElement();
        if (gameObject.activeSelf == true)
        {
            bricksPool.BlackBrickPool.Release(this);
        }
    }
}
