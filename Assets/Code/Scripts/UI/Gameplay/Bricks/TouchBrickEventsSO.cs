using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newBrickEventsHolder")]
public class TouchBrickEventsSO : ScriptableObject
{
    [SerializeField] GameEvent playerAttackEvent;
    [SerializeField] GameEvent playerCriticalAttackEvent;
    [SerializeField] GameEvent playerBlockEvent;
    [SerializeField] GameEvent playerIsHitEvent;

    public GameEvent GetPlayerAttackEvent()
    {
        return playerAttackEvent;
    }

    public GameEvent GetPlayerCriticalAttackEvent()
    {
        return playerCriticalAttackEvent;
    }

    public GameEvent GetPlayerBlockEvent()
    {
        return playerBlockEvent;
    }
    public GameEvent GetPlayerIsHitEvent()
    {
        return playerIsHitEvent;
    }
}
