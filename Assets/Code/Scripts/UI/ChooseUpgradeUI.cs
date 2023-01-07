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
        continueButton.clicked += continueWalkingEvent.Raise;
    }

    private void OnDisable()
    {
        continueButton.clicked -= continueWalkingEvent.Raise;
    }
}
