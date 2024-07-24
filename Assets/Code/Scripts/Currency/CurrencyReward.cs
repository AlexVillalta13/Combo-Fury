using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CurrencyReward
{
    [SerializeField] CurrencySO currency;
    public CurrencySO Currency => currency;

    [SerializeField] int amount = 0;
    public int Amount => amount;
}
