using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingLevelUI : UIComponent
{
    [SerializeField] GameEvent onLevelLoadedevent;
    [SerializeField] float timeToLoad = 1f;
    public void StartDisableLoadingUICoroutine()
    {
        StartCoroutine(DisableLoadingLevelUICoroutine());
    }
    private IEnumerator DisableLoadingLevelUICoroutine() 
    {
        yield return new WaitForSeconds(timeToLoad);
        HideGameplayElement();
        onLevelLoadedevent.Raise(gameObject);
    }
}
