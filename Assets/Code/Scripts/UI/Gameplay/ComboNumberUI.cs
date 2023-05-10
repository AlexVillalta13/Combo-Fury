using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ComboNumberUI : UIComponent
{
    Label comboNumber;

    const string comboNumberReference = "ComboNumber";

    public override void SetElementsReferences()
    {
        base.SetElementsReferences();

        comboNumber = m_UIElement.Query<Label>(name: comboNumberReference);
    }

    private void OnEnable()
    {
        CombatController.onChangeComboNumber += UpdateComboNumber;
    }

    private void OnDisable()
    {
        CombatController.onChangeComboNumber -= UpdateComboNumber;
    }

    private void UpdateComboNumber(int number)
    {
        comboNumber.text = number.ToString();
    }
}
