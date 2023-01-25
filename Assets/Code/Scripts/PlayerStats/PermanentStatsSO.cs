using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerPemanentStatsSO", fileName = "New Permanent Stats SO")]
public class PermanentStatsSO : ScriptableObject
{
    [SerializeField] int maxHealth = 100;
    public int MaxHealth { get { return maxHealth; } }

    [SerializeField] int attack = 22;
    public int Attack { get { return attack; } }

    [SerializeField] int defense = 5;
    public int Defense { get { return defense; } }
}
