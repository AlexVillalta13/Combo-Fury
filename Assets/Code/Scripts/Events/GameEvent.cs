using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Game event")]
public class GameEvent : ScriptableObject
{
    [ShowInInspector] private readonly List<GameEventListener> gameEventsList = new List<GameEventListener>();

    [ShowInInspector] private readonly List<GameObject> gameObjectAttached = new List<GameObject>();

    [Button(ButtonSizes.Medium)]
    public void Raise (GameObject gameObjectAttached)
    {
        this.gameObjectAttached.Add(gameObjectAttached);
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
