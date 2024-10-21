using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EndLevelUI : UIComponent
{
    [SerializeField] GameEvent returnToMainMenuEvent;
    [SerializeField] float timeToShowUIAfterDeath = 1.5f;

    public override void SetElementsReferences()
    {
        base.SetElementsReferences();

    }

    public void StartCoroutineEnableTapToReturn()
    {
        StartCoroutine(EnableTapToReturnToMainMenuCoroutine());
    }

    private IEnumerator EnableTapToReturnToMainMenuCoroutine()
    {
        yield return new WaitForSeconds(timeToShowUIAfterDeath);
        SetDisplayElementFlex();
        EnableTapToReturnToMainMenu();
    }

    private void EnableTapToReturnToMainMenu()
    {
        m_UIElement.RegisterCallback<ClickEvent>(RaiseReturnToMainMenuEvent);
    }

    private void DisableTapToReturnToMainMenu()
    {
        m_UIElement.UnregisterCallback<ClickEvent>(RaiseReturnToMainMenuEvent);
    }

    private void RaiseReturnToMainMenuEvent(ClickEvent evt)
    {
        DisableTapToReturnToMainMenu();
        returnToMainMenuEvent.Raise(this);
    }
}
