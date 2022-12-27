using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Game event")]
public class GameEvent : ScriptableObject
{
    private readonly List<GameEventListener> gameEventsList = new List<GameEventListener>();

    public void Raise ()
    {
        for(int i = gameEventsList.Count - 1; i >= 0; i--)
        {
            gameEventsList[i].OnEventRaised();
        }
    }

    public void RegisterEventListener(GameEventListener listener)
    {
        if(gameEventsList.Contains(listener) == false)
        {
            gameEventsList.Add(listener);
        }
    }

    public void UnregisterEventListener (GameEventListener listener)
    {
        if(gameEventsList.Contains (listener) == false)
        {
            gameEventsList.Remove(listener);
        }
    }
}
