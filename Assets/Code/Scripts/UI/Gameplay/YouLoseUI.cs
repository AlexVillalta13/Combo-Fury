using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class YouLoseUI : UIComponent
{
    Button returnButton;

    [SerializeField] GameEvent returnToMainMenuEvent;

    public override void SetElementsReferences()
    {
        base.SetElementsReferences();

        returnButton = m_Root.Query<Button>("ReturnButton");
    }

    private void OnEnable()
    {
        returnButton.RegisterCallback<ClickEvent>(RaiseReturnToMainMenuEvent);
    }

    private void OnDisable()
    {
        returnButton.UnregisterCallback<ClickEvent>(RaiseReturnToMainMenuEvent);
    }

    private void RaiseReturnToMainMenuEvent(ClickEvent evt)
    {
        returnToMainMenuEvent.Raise(gameObject);
    }
}
