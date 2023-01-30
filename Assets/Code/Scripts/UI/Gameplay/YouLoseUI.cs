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
        returnButton.clicked += returnToMainMenuEvent.Raise;
    }

    private void OnDisable()
    {
        returnButton.clicked -= returnToMainMenuEvent.Raise;
    }
}
