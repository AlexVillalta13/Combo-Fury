using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ChooseUpgradeUI : UIComponent
{
    [SerializeField] GameEvent continueWalkingEvent;

    Button continueButton;

    public override void SetElementsReferences()
    {
        base.SetElementsReferences();

        continueButton = m_UIElement.Query<Button>();
    }

    private void OnEnable()
    {
        continueButton.clicked += ContinueButtonPressed;
    }

    private void OnDisable()
    {
        continueButton.clicked -= ContinueButtonPressed;
    }

    public void ContinueButtonPressed()
    {
        continueWalkingEvent.Raise();
        HideGameplayElement();
    }
}
