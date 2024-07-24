using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "Currency")]
public class CurrencySO : ScriptableObject
{
    public Sprite icon;
    [ShowInInspector] int count;

    public void Add(int quantityToAdd)
    {
        count += quantityToAdd;
    }

    public bool Substract(int quantityToSubstract)
    {
        if(count - quantityToSubstract < 0)
        {
            return false;
        }
        count -= quantityToSubstract;
        return true;
    }
}
