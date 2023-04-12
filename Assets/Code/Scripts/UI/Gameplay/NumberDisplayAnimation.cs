using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class NumberDisplayAnimation : MonoBehaviour
{
    NumberDisplayPool pool;
    TextMeshPro _textMeshPro;

    private void Awake()
    {
        _textMeshPro = GetComponent<TextMeshPro>();
    }

    public void SetPool(NumberDisplayPool pool)
    {
        this.pool = pool;
    }

    private void OnEnable()
    {
        StartCoroutine(DOMoveCoroutine());
    }

    private IEnumerator DOMoveCoroutine()
    {
        //transform.DOShakeScale(0.5f);
        transform.DOPunchScale(new Vector3(2f, 2f, 2f), 0.3f, 10);
        Tween moveTween = transform.DOMoveY(1f, 1f);
        yield return moveTween.WaitForCompletion();
        pool.NumberMeshPool.Release(_textMeshPro);
    }
}
