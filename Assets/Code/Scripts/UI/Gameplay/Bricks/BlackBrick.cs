using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UIElements;

public class BlackBrick : Brick
{
    Tween tween;
    Vector3 vec3Shake;
    float blackBrickPositionInBar;
    Vector3 shakeStrength = new Vector3(50f, 0f, 0f);
    bool isShaking = false;

    [SerializeField] private float timeToEnableTouch = 0.5f;

    public BlackBrick() : base()
    {
        brickHolder = BrickHolder.EnemyBrick;
        brickType = BrickTypeEnum.BlackBrick;
    }
    
    protected override void SetVisualElementParent(VisualElement playerElementParent, VisualElement enemyElementParent, VisualElement trapElementParent)
    {
        this.m_elementParent = trapElementParent;
        brickRootElementAttached.AddToClassList(enemyUSSClassName);
    }

    protected override void OnBrickPositioned()
    {
        base.OnBrickPositioned();
        blackBrickPositionInBar = brickRootElementAttached.style.left.value.value;
        vec3Shake.x = blackBrickPositionInBar;
        brickRootElementAttached.AddToClassList(ignoreBrickWithTouchUSSClassName);
        StartCoroutine(TouchBrickEnabled());
    }

    private IEnumerator TouchBrickEnabled()
    {
        yield return new WaitForSeconds(timeToEnableTouch);
        brickRootElementAttached.RemoveFromClassList(ignoreBrickWithTouchUSSClassName);
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
            brickEventsHolder.GetPlayerIsHitEvent().Raise(gameObject);
            ShakeBrick();
        }
    }


    private void ShakeBrick()
    {
        tween = DOTween.Shake(() => vec3Shake, x => vec3Shake = x, 0.25f, shakeStrength, 100);
        tween.onComplete += RemoveBrickElement;
        tween.onComplete += RemoveVisualElementFromDict;
        isShaking = true;
    }

    public override void RemoveBrickElement()
    {
        tween.Kill();
        base.RemoveBrickElement();
    }
}
