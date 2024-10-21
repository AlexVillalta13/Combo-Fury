using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class WinLevelUI : UIComponent
{
    Button goToMainMenuButton;
    [SerializeField] GameEvent returnToMainMenuEvent;

    public override void SetElementsReferences()
    {
        base.SetElementsReferences();

        goToMainMenuButton = m_Root.Query<Button>("ReturnToMainMenuButton");
    }

    private void OnEnable()
    {
        goToMainMenuButton.clicked += ReturnToMainMenu;
    }

    private void OnDisable()
    {
        goToMainMenuButton.clicked -= ReturnToMainMenu;
    }

    private void ReturnToMainMenu()
    {
        SetDisplayElementNone();
        returnToMainMenuEvent.Raise(this);
    }
}
