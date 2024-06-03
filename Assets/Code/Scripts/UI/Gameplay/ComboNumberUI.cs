using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ComboNumberUI : UIComponent
{
    [SerializeField] PlayerStatsSO inCombatPlayerStatsSO;

    Label comboNumber;

    const string comboNumberReference = "ComboNumber";

    public override void SetElementsReferences()
    {
        base.SetElementsReferences();

        comboNumber = m_UIElement.Query<Label>(name: comboNumberReference);
    }

    public void UpdateComboNumber()
    {
        comboNumber.text = "Combo " + inCombatPlayerStatsSO.comboCount.ToString();
    }
}
