using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboCounter : MonoBehaviour
{
    [SerializeField] PlayerStatsSO inCombatPlayerStatsSO;

    [SerializeField] GameEvent onUpdateComboNumber;

    public void ResetComboCount()
    {
        inCombatPlayerStatsSO.comboCount = 0;
        onUpdateComboNumber.Raise(this);
    }

    public void IncreaseComboCount()
    {
        inCombatPlayerStatsSO.comboCount++;
        onUpdateComboNumber.Raise(this);
    }
}
